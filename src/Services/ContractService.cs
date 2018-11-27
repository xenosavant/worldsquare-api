using stellar_dotnet_sdk;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
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

        public ContractService(IHorizonService horizonService)
        {
            _horizonService = horizonService;
            // get worldSuareAccount from somewhere (keyvault or similar)
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

        public async Task<Contract> SetupContractAsync()
        {
            var escrow = _horizonService.CreateAccount();

            var sequenceNumber = await _horizonService.GetSequenceNumber(escrow.PublicKey);

            var contract = new Contract
            {
                EscrowAccountId = escrow.PublicKey,
                BaseSequenceNumber = sequenceNumber,
                CurrentSequenceNumber = sequenceNumber,
                ContractStateId = (int)ContractState.Initial,
                ContractTypeId = 0
            };

            var phaseZero = new ContractPhase
            {
                Completed = true,
                SequenceNumber = sequenceNumber
            };

            contract.Phases.Add(phaseZero);
            return contract;
        }

        public async Task<Contract> FundContractAsync(Contract contract, ContractParameterModel contractParameterModel)
        {
            var phaseOne = new ContractPhase();
            var operations = new List<Operation>();

            contract.DestAccountId = contractParameterModel.DestinationAccount;
            contract.SourceAccountId = contractParameterModel.SourceAccount;
            //for phase 1, its base sequence number +1
            phaseOne.SequenceNumber = contract.BaseSequenceNumber + 1;

            var weight = new HorizonAccountWeightModel();
            var destinationAccount = new HorizonAccountSignerModel();
            var worldSquareAccount = new HorizonAccountSignerModel();
            weight.Signers = new List<HorizonAccountSignerModel>();

            //Transfer funds tp Escrow
            //TBD: consider other assets too
            //TBD: transfer 1 % to WorldSquare 
            var paymentOperation = _horizonService.CreatePaymentOperation(contract.SourceAccountId, contract.EscrowAccountId, contractParameterModel.Asset.Amount);
            operations.Add(paymentOperation);

            //Escrow threshold weights are 4
            weight.LowThreshold = 5;
            weight.MediumThreshold = 5;
            weight.HighThreshold = 6;

            //escrow master weight (1) + dest weight (1) + WorldSquare (4)
            //dest account has weight 1
            destinationAccount.Signer = contract.DestAccountId;
            destinationAccount.Weight = 1;
            weight.Signers.Add(destinationAccount);
            worldSquareAccount.Signer = _worldSquareAccount.PublicKey;
            worldSquareAccount.Weight = 4;
            weight.Signers.Add(worldSquareAccount);
            //Let the SignerSecret be null
            weight.SignerSecret = null;

            var setOptionsOperation = _horizonService.SetOptionsOperation(contract.EscrowAccountId, weight);
            operations.Add(setOptionsOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseOne.SequenceNumber - 1);
            
            /*  var response = await _horizon.SubmitTxn(_horizon.SignTxn(contract.EscrowAccountId, xdrTransaction));
                if(response.IsSuccess() == false)
                return null;
            */

            var preTransaction = new PreTransaction
            {
                XdrString = xdrTransaction
            };

            var publicKeys = new[]
            {
                contract.DestAccountId, contract.EscrowAccountId
            };

            var signatureList = CreateSignatureList(preTransaction, publicKeys);

            phaseOne.Transactions.Add(signatureList);

            contract.Phases.Add(phaseOne);
            return contract;
        }

        private async Task<Contract> ConstructPhaseTwoAsync(Contract contract)
        {
            var phaseTwo = new ContractPhase();
            var operations = new List<Operation>();

            //for phase 1, its base sequence number +1
            phaseTwo.SequenceNumber = contract.BaseSequenceNumber + 2;

            //success txn, bump to next
            var bumpOperation = _horizonService.BumpSequenceOperation(contract.EscrowAccountId, contract.BaseSequenceNumber + (1 + 1));
            operations.Add(bumpOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseTwo.SequenceNumber - 1);

            var preTransaction = new PreTransaction
            {
                XdrString = xdrTransaction
            };

            var publicKeys = new[]
            {
                contract.DestAccountId, contract.EscrowAccountId
            };

            var signatureList = CreateSignatureList(preTransaction, publicKeys);

            phaseTwo.Transactions.Add(signatureList);

            //failure txn, bump
            //ToDo: replace bump sequence with transfer fund to source account
            bumpOperation = _horizonService.BumpSequenceOperation(contract.EscrowAccountId, contract.BaseSequenceNumber + (1 + 3));

            operations.Add(bumpOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseTwo.SequenceNumber - 1);

            var preTransactionRide = new PreTransaction
            {
                XdrString = xdrTransaction
            };

            //use the same publicKeys as above
            signatureList = CreateSignatureList(preTransactionRide, publicKeys);

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
                contract.DestAccountId, contract.EscrowAccountId
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
                contract.DestAccountId, contract.EscrowAccountId
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

            var weight = new HorizonAccountWeightModel();

            var destinationAccount = new HorizonAccountSignerModel();

            var worldSquareAccount = new HorizonAccountSignerModel();

            weight.Signers = new List<HorizonAccountSignerModel>();

            weight.LowThreshold = 5;
            weight.MediumThreshold = 5;
            weight.HighThreshold = 6;

            destinationAccount.Signer = contract.DestAccountId;
            destinationAccount.Weight = 1;
            weight.Signers.Add(destinationAccount);
            worldSquareAccount.Signer = _worldSquareAccount.PublicKey;
            worldSquareAccount.Weight = 4;
            weight.Signers.Add(worldSquareAccount);
            //Let the SignerSecret be null
            weight.SignerSecret = null;

            var setOptionsOperation = _horizonService.SetOptionsOperation(contract.EscrowAccountId, weight);
            operations.Add(setOptionsOperation);

            var xdrTransaction =
                await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, null, phaseFourDispute.SequenceNumber - 1);

            var preTransaction = new PreTransaction
            {
                XdrString = xdrTransaction
            };

            var publicKeys = new[]
            {
                contract.DestAccountId, contract.EscrowAccountId
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
                contract.DestAccountId, contract.EscrowAccountId
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
            contract = await ConstructPhaseTwoAsync(contract);

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
        public void UpdateContract(Contract contract)
        {
            var enumerator = contract.Phases.GetEnumerator();

            var move = contract.CurrentSequenceNumber - contract.BaseSequenceNumber;

            for (long i = 0; i <= move; i++) enumerator.MoveNext();

            var phase = enumerator.Current;
            if (phase.Completed)
            {
                Console.WriteLine("Error: Sequence number and phase.completed not matching\n");
                return;
            }

            foreach (var preTransaction in phase.Transactions) {
                var flag = preTransaction.Signatures.All(sign => sign.Signed);

                //all signatures obtained , verify if we can submit
                if(flag && VerifyTimeBound(preTransaction.MinimumTime, preTransaction.MaximumTime)) {
                     _horizonService.SubmitTransaction(preTransaction.XdrString);

                     preTransaction.Submitted = true;

                     phase.Completed = true;
                     /* todo update sequence number*/
                     //contract.CurrentSequenceNumber = _horizonService.GetSequenceNumber(contract.EscrowAccountId);
                     break;
                }
            }
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

        public string ExecuteContract()
        {
            const string hash = "";
            return hash;
        }
    }
}