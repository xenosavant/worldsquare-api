using Microsoft.Extensions.Options;
using stellar_dotnet_sdk;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Services
{
    public class ContractService : IContractService
    {
        private readonly IHorizonService _horizonService;
        private readonly HorizonKeyPairModel _worldSquareAccount;
        private readonly String _minimumFund;

        public ContractService(IHorizonService horizonService, IOptions<SignatureSettings> settings)
        {
            _horizonService = horizonService;
            _worldSquareAccount = new HorizonKeyPairModel()
            {
                PublicKey = settings.Value.MasterPublicKey,
                SecretKey = settings.Value.MasterSecrectKey
            };
        }

        private PreTransaction CreateSignatureList(PreTransaction preTransaction, IReadOnlyCollection<string> publicKeys)
        {
            foreach (var key in publicKeys)
            {
                var sign = new UserSignature
                {
                    PublicKey = key, Signed = false, Transaction = preTransaction
                };

                preTransaction.Signatures.Add(sign);
            }

            //System Signature is common for all type of pre-transaction
            Signature systemSignature = new SystemSignature
            {
                PublicKey = _worldSquareAccount.PublicKey,
                Signed = false,
                Transaction = preTransaction
            };

            preTransaction.Signatures.Add(systemSignature);
            return preTransaction;
        }

        public async Task<Contract> SetupContractAsync(ContractParameterModel contractParameterModel)
        {
            var escrow = _horizonService.CreateAccount();

            /* Create the escrow account with minimum fund, this is important
            * to register the account on network so that we obtain escrow sequence
            */
            var operations = new List<Operation>();

            var createAccountOperation = _horizonService.CreateAccountOperation(_worldSquareAccount.PublicKey,
                    escrow.PublicKey,  _minimumFund);
                operations.Add(createAccountOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(_worldSquareAccount.PublicKey, operations, null, 0);
            await _horizonService.SubmitTransaction(_horizonService.SignTransaction(_worldSquareAccount, null, xdrTransaction));

            // Add buyer and WS as signer and assign escrow master weight to 0
            operations.Clear();
            var weight = new HorizonAccountWeightModel() {
                Signers = new List<HorizonAccountSignerModel>(){
                    new HorizonAccountSignerModel {
                        Signer = contractParameterModel.SourceAccount,
                        Weight = 1
                    },
                    new HorizonAccountSignerModel {
                        Signer = _worldSquareAccount.PublicKey,
                        Weight = 4
                    },
                },
                MasterWeight = 0,
                LowThreshold = 5,
                MediumThreshold = 5,
                HighThreshold = 5,
            };

            var setOptionsWeightOperation = _horizonService.SetOptionsWeightOperation(escrow.PublicKey, weight);
            operations.Add(setOptionsWeightOperation);

            xdrTransaction = await _horizonService.CreateTransaction(escrow.PublicKey, operations, null, 0);

            await _horizonService.SubmitTransaction(_horizonService.SignTransaction(escrow, null, xdrTransaction));

           //create contract
            var sequenceNumber = await _horizonService.GetSequenceNumber(escrow.PublicKey);
            var contract = new Contract
            {
                EscrowAccountId = escrow.PublicKey,
                DestAccountId = contractParameterModel.DestinationAccount,
                SourceAccountId = contractParameterModel.SourceAccount,
                BaseSequenceNumber = sequenceNumber,
                CurrentSequenceNumber = sequenceNumber,
                ContractStateId = (int)ContractState.Initial
            };

            //create phase 2 regular and time over ride transactions and buyer signs it
            contract = await ConstructPhaseTwoAsync(contract);
            // ToDo: signing is pending

            //if we are here, that means phase 0 is success
            var phaseZero = new ContractPhase
            {
                Completed = true,
                SequenceNumber = sequenceNumber,
                Contested = false
            };

            contract.Phases.Add(phaseZero);
            return contract;
        }

        public async Task<Contract> FundContractAsync(Contract contract, ContractSignatureModel FundingAccount, string Amount)
        {
            var phaseOne = new ContractPhase();
            var operations = new List<Operation>();

            //nothing to be done for contract in phase 1; this affects the sequence number
            // ToDo: alter the sequence number

            //for phase 1, its base sequence number +1
            //phaseOne.SequenceNumber = contract.BaseSequenceNumber + 1;

            var asset = new HorizonAssetModel {
                IsNative = true,
                Amount = Amount
            };
            var paymentOperation = _horizonService.CreatePaymentOperation(contract.SourceAccountId, contract.EscrowAccountId, asset);
            operations.Add(paymentOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseOne.SequenceNumber - 1);
            
            var response = await _horizonService.SubmitTransaction(_horizonService.SignTransaction(null,
                    FundingAccount.Secret, xdrTransaction));

            if(response.IsSuccess() == false)
                    return null;

            return contract;
        }

        private async Task<Contract> ConstructPhaseTwoAsync(Contract contract)
        {
            var phaseTwo = new ContractPhase();
            var operations = new List<Operation>();

            //for phase 1, its base sequence number +1
            phaseTwo.SequenceNumber = contract.BaseSequenceNumber + 2;

            //success txn, add seller as single txn signer
            //Add seller as escrow signer along with buyer and WS
            var weight = new HorizonAccountWeightModel() {
                Signers = new List<HorizonAccountSignerModel>(){
                    new HorizonAccountSignerModel {
                        Signer = contract.DestAccountId,
                        Weight = 1
                    }
                },
                LowThreshold = 5,
                MediumThreshold = 5,
                HighThreshold = 6,
            };

            var setOptionsWeightOperation = _horizonService.SetOptionsWeightOperation(contract.EscrowAccountId, weight);
            operations.Add(setOptionsWeightOperation);

            var setOptionsAddSeller = _horizonService.SetOptionsSingleSignerOperation(contract.DestAccountId);
            operations.Add(setOptionsAddSeller);

            var xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseTwo.SequenceNumber - 1);

            var preTransaction = new PreTransaction
            {
                XdrString = xdrTransaction
            };

            var publicKeys = new[]
            {
                contract.SourceAccountId, contract.DestAccountId
            };

            var signatureList = CreateSignatureList(preTransaction, publicKeys);

            phaseTwo.Transactions.Add(signatureList);

            //failure txn, merge escrow to buyer
            var mergeOperation = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.SourceAccountId);

            operations.Add(mergeOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseTwo.SequenceNumber - 1);

            var preTransactionRide = new PreTransaction
            {
                XdrString = xdrTransaction
            };

            var publicKeysOverRide = new[]
            {
                contract.SourceAccountId
            };

            signatureList = CreateSignatureList(preTransactionRide, publicKeysOverRide);

            phaseTwo.Transactions.Add(signatureList);

            contract.Phases.Add(phaseTwo);

            return contract;
        }

        private async Task<Contract> ConstructPhaseThreeAsync(Contract contract)
        {
            var phaseThree = new ContractPhase();
            var operations = new List<Operation>();

            phaseThree.SequenceNumber = contract.BaseSequenceNumber + 3;

            //success txn, bump to next
            var bumpOperation =
                _horizonService.BumpSequenceOperation(contract.EscrowAccountId, contract.BaseSequenceNumber + (2 + 1));

            operations.Add(bumpOperation);

            var xdrTransaction =
                await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseThree.SequenceNumber - 1);

            var preTransaction = new PreTransaction
            {
                XdrString = xdrTransaction
            };
            
            var publicKeys = new[]
            {
                contract.DestAccountId
            };

            var signatureList = CreateSignatureList(preTransaction, publicKeys);

            phaseThree.Transactions.Add(signatureList);

            //failure txn, bump
            bumpOperation = _horizonService.BumpSequenceOperation(contract.EscrowAccountId, contract.BaseSequenceNumber + (2 + 2));

            operations.Add(bumpOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseThree.SequenceNumber - 1);

            var preTransactionRide = new PreTransaction
            {
                XdrString = xdrTransaction
            };
            
            signatureList = CreateSignatureList(preTransactionRide, publicKeys);

            phaseThree.Transactions.Add(signatureList);

            contract.Phases.Add(phaseThree);
            return contract;
        }

        private async Task<Contract> ConstructPhaseFourAsync(Contract contract)
        {
            var phaseFour = new ContractPhase();
            var operations = new List<Operation>();

            phaseFour.SequenceNumber = contract.BaseSequenceNumber + 4;

            //dispute txn, bump to next
            var bumpOperation =
                _horizonService.BumpSequenceOperation(contract.EscrowAccountId, contract.BaseSequenceNumber + (3 + 1));

            operations.Add(bumpOperation);

            var xdrTransaction =
                await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseFour.SequenceNumber - 1);

            var preTransaction = new PreTransaction
            {
                XdrString = xdrTransaction
            };
            
            var publicKeys = new[]
            {
                contract.DestAccountId
            };

            var signatureList = CreateSignatureList(preTransaction, publicKeys);

            phaseFour.Transactions.Add(signatureList);

            //success txn, merge txn
            var mergeOperation = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.DestAccountId);

            operations.Add(mergeOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseFour.SequenceNumber - 1);

            var preTransactionMerge = new PreTransaction
            {
                XdrString = xdrTransaction
            };
            
            signatureList = CreateSignatureList(preTransactionMerge, publicKeys);

            phaseFour.Transactions.Add(signatureList);

            //failure txn, bump
            bumpOperation = _horizonService.BumpSequenceOperation(contract.EscrowAccountId, contract.BaseSequenceNumber + (3 + 1));
            operations.Add(bumpOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseFour.SequenceNumber - 1);

            var preTransactionRide = new PreTransaction
            {
                XdrString = xdrTransaction
            };
            
            signatureList = CreateSignatureList(preTransactionRide, publicKeys);

            phaseFour.Transactions.Add(signatureList);

            contract.Phases.Add(phaseFour);
            return contract;
        }

        private async Task<Contract> ConstructPhaseFourDisputeAsync(Contract contract)
        {
            var phaseFourDispute = new ContractPhase();

            var operations = new List<Operation>();

            phaseFourDispute.SequenceNumber = contract.BaseSequenceNumber + 5;

            var weight = new HorizonAccountWeightModel() {
                Signers = new List<HorizonAccountSignerModel>(){
                    new HorizonAccountSignerModel {
                        Signer = contract.DestAccountId,
                        Weight = 0
                    },
                    new HorizonAccountSignerModel {
                        Signer = _worldSquareAccount.PublicKey,
                        Weight = 4
                    },
                },
                LowThreshold = 4,
                MediumThreshold = 4,
                HighThreshold = 4,
            };

            var setOptionsWeightOperation = _horizonService.SetOptionsWeightOperation(contract.EscrowAccountId, weight);
            operations.Add(setOptionsWeightOperation);

            var xdrTransaction =
                await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseFourDispute.SequenceNumber - 1);

            var preTransaction = new PreTransaction
            {
                XdrString = xdrTransaction
            };

            var publicKeys = new[]
            {
                contract.DestAccountId
            };

            var signatureList = CreateSignatureList(preTransaction, publicKeys);

            phaseFourDispute.Transactions.Add(signatureList);

            contract.Phases.Add(phaseFourDispute);
            return contract;
        }

        private async Task<Contract> ConstructPhaseFiveAsync(Contract contract)
        {
            var phaseFive = new ContractPhase();
            var operations = new List<Operation>();

            phaseFive.SequenceNumber = contract.BaseSequenceNumber + 6;

            //release txn, merge txn
            var mergeOperation = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.DestAccountId);

            operations.Add(mergeOperation);

            var xdrTransaction =
                await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseFive.SequenceNumber - 1);

            var preTransactionMerge = new PreTransaction
            {
                XdrString = xdrTransaction
            };
            
            var publicKeys = new[]
            {
                contract.DestAccountId
            };

            var signatureList = CreateSignatureList(preTransactionMerge, publicKeys);

            phaseFive.Transactions.Add(signatureList);

            //refund txn, merge txn
            mergeOperation = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.SourceAccountId);

            operations.Add(mergeOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseFive.SequenceNumber - 1);

            var preTransactionMergeRefund = new PreTransaction
            {
                XdrString = xdrTransaction
            };
            
            signatureList = CreateSignatureList(preTransactionMergeRefund, publicKeys);

            phaseFive.Transactions.Add(signatureList);

            contract.Phases.Add(phaseFive);
            return contract;
        }

        public async Task<Contract> CreateContractAsync(Contract contract)
        {
            //phase 2 pretxn is created in phase 0 itself
            //contract = await ConstructPhaseTwoAsync(contract);
            // ToDo: check if escrow is funded before proceeding

            contract = await ConstructPhaseThreeAsync(contract);

            contract = await ConstructPhaseFourAsync(contract);

            contract = await ConstructPhaseFourDisputeAsync(contract);

            contract = await ConstructPhaseFiveAsync(contract);
            return contract;
        }

        private static long GetCurrentTimeInSeconds()
        {
            return (long) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private static long ConvertDateToSeconds(DateTime date)
        {
            return (long) (date.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private static bool VerifyTimeBound(DateTime minimum, DateTime maximum)
        {
            var noDelay = true;

            var currentTime = GetCurrentTimeInSeconds();

            if(currentTime < ConvertDateToSeconds(minimum)) {
                noDelay = false;
            }
            if(currentTime > ConvertDateToSeconds(maximum)) {
                noDelay = false;
            }
            return noDelay;
        }
        public async Task<bool> UpdateContractAsync(Contract contract)
        {
            var enumerator = contract.Phases.GetEnumerator();

            var move = contract.CurrentSequenceNumber - contract.BaseSequenceNumber;

            for (long i = 0; i <= move; i++) enumerator.MoveNext();

            var phase = enumerator.Current;
            if (phase.Completed)
            {
                Console.WriteLine("Error: Sequence number and phase.completed not matching\n");
                return false;
            }

            foreach (var preTransaction in phase.Transactions) {
                var flag = preTransaction.Signatures.All(sign => sign.Signed);

                //all signatures obtained , verify if we can submit
                if(flag && VerifyTimeBound(preTransaction.MinimumTime, preTransaction.MaximumTime)) {
                     await _horizonService.SubmitTransaction(preTransaction.XdrString);
                     //update preTransaction
                     preTransaction.Submitted = true;
                     //update phase
                     phase.Completed = true;
                     //update contract
                     contract.CurrentSequenceNumber = await _horizonService.GetSequenceNumber(contract.EscrowAccountId);
                     break;
                }
            }
            return true;
        }

        private string AddSign(string xdrTransaction, string secretKey)
        {
            int count, newcount = 0;

            count = _horizonService.GetSignatureCount(xdrTransaction);

            _horizonService.SignTransaction(null, secretKey, xdrTransaction);

            newcount = _horizonService.GetSignatureCount(xdrTransaction);
            if (count + 1 == newcount)
                return _horizonService.SignatureHash(xdrTransaction, newcount - 1);

            return null;
        }

        public bool SignContract(ContractSignatureModel signatureModel)
        {
            // call horizon and if successful, then update signature and return true
            // otherwise return false
            var signature = signatureModel.Signature;
            var preTransaction = signature.Transaction;

            if(!VerifyTimeBound(preTransaction.MinimumTime, preTransaction.MaximumTime)) {
                Console.WriteLine("time bound delay verification failed");
                return false;
            }
            if (signature.PublicKey != null)
            {
                var secretKey = "";

                if (signatureModel.Secret == null)
                {
                    //system signature
                    secretKey = string.Copy(_worldSquareAccount.SecretKey);
                }
                else if (_horizonService.GetPublicKey(signatureModel.Secret) == signature.PublicKey)
                {
                    //user signature
                    secretKey = string.Copy(signatureModel.Secret);
                }
                else
                {
                    //secret key and public key did not match
                    Console.WriteLine("secret key and public key did not match");
                    return false;
                }

                var hash = AddSign(preTransaction.XdrString, secretKey);
                if (hash != null)
                {
                    signature.SignatureHash = hash;

                    signature.Signed = true;

                    signature.SignedOn = DateTime.UtcNow;
                    return true;
                }

                //signature list did not increment
                Console.WriteLine("secret key and public key did not match");
                return false;
            }

            if (signature.PublicKey == null) return true;
            //dead end
            return false;
        }
    }
}