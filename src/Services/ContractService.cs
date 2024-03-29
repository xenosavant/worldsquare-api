﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using stellar_dotnet_sdk;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contracts;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Services
{
    public class ContractService : IContractService
    {
        private readonly IHorizonService _horizonService;
        //ToDo: add minimumFund in config
        private readonly string _minimumFund;
        private readonly HorizonKeyPairModel _worldSquareAccount;

        public ContractService(IHorizonService horizonService, IOptions<SignatureSettings> settings)
        {
            _horizonService = horizonService;
            _worldSquareAccount = new HorizonKeyPairModel {PublicKey = settings.Value.MasterPublicKey, SecretKey = settings.Value.MasterSecrectKey};
        }

        public async Task<Contract> SetupContractAsync(ContractParameterModel contractParameterModel)
        {
            var escrow = _horizonService.CreateAccount();

            /* Create the escrow account with minimum fund, this is important
            * to register the account on network so that we obtain escrow sequence
            */
            var operations = new List<Operation>();

            var createAccountOperation = _horizonService.CreateAccountOperation(_worldSquareAccount.PublicKey, escrow.PublicKey, _minimumFund);
            operations.Add(createAccountOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(_worldSquareAccount.PublicKey, operations, time: null, sequence: 0);
            await _horizonService.SubmitTransaction(_horizonService.SignTransaction(_worldSquareAccount.SecretKey, xdrTransaction));

            // Add buyer and WS as signer and assign escrow master weight to 0
            operations.Clear();
            var weight = new HorizonAccountWeightModel
                         {
                             Signers = new List<HorizonAccountSignerModel>
                                       {
                                           new HorizonAccountSignerModel {Signer = contractParameterModel.SourceAccountId, Weight = 1},
                                           new HorizonAccountSignerModel {Signer = _worldSquareAccount.PublicKey, Weight = 4}
                                       },
                             MasterWeight = 0,
                             LowThreshold = 5,
                             MediumThreshold = 5,
                             HighThreshold = 5
                         };

            var setOptionsWeightOperation = _horizonService.SetOptionsWeightOperation(escrow.PublicKey, weight);
            operations.Add(setOptionsWeightOperation);

            xdrTransaction = await _horizonService.CreateTransaction(escrow.PublicKey, operations, time: null, sequence: 0);

            await _horizonService.SubmitTransaction(_horizonService.SignTransaction(escrow.SecretKey, xdrTransaction));

            //create contract
            var sequenceNumber = await _horizonService.GetSequenceNumberAsync(escrow.PublicKey);
            //de-couple phase and sequence numbering
            var PhaseAdd = 0;
            var SequenceAdd = 0;

            var contract = new Contract
                           {
                               EscrowAccountId = escrow.PublicKey,
                               DestAccountId = contractParameterModel.DestinationAccountId,
                               SourceAccountId = contractParameterModel.SourceAccountId,
                               BaseSequenceNumber = sequenceNumber,
                               CurrentSequenceNumber = sequenceNumber,
                               CurrentPhaseNumber = 0,

                               // ToDo: contract state yet to be added
                               //ContractStateId =0;
                           };

            // NO CONTRACT TYPES NOW, everything is defined by parent objects and contracts are agnostic 


            //switch (contractParameterModel.ContractTypeId)
            //{
            //    case (int) ContractTypes.OnlineSaleInternalShippingValidation:

                    //if we are here, that means the current phase is success
                    var phase = new ContractPhase {Completed = true, SequenceNumber = sequenceNumber, Contested = false,
                                    PhaseNumber = ++PhaseAdd};
                    contract.Phases.Add(phase);
                    contract.CurrentPhaseNumber++;

                    //FundContractAsync sequence number is same like before since no change to escrow account
                    //Phase will be true when funding in complete in FundContractAsync
                    phase = new ContractPhase {Completed = false, SequenceNumber = sequenceNumber, Contested = false,
                                    PhaseNumber = ++PhaseAdd};
                    contract.Phases.Add(phase);

                    //create Phase Ship regular and time over ride transactions and buyer signs it
                    contract = await ConstructPhaseShipAsync(contract, ++PhaseAdd, ++SequenceAdd);

                    var secret = contractParameterModel.SourceAccountSecret;

                    // ToDo: add pre transaction to secret
                    SignContract(secret);

                    //buyer signing not required, but we will create all pre txn here itself
                    contract = await ConstructPhaseDeliveryAsync(contract, ++PhaseAdd, ++SequenceAdd);

                    contract = await ConstructPhaseReceiptAsync(contract, ++PhaseAdd, ++SequenceAdd);

                    contract = await ConstructPhaseDisputeAsync(contract, ++PhaseAdd, ++SequenceAdd);

                    contract = await ConstructPhaseResolutionAsync(contract, ++PhaseAdd, ++SequenceAdd);

                //    break;

                //default:
                //    Console.WriteLine(value: "contract id undefined\n");

                //    return null;
            //}

            return contract;
        }

        public async Task<Contract> FundContractAsync(Contract contract, ContractSignatureModel fundingAccount, string amount)
        {
            var operations = new List<Operation>();

            var asset = new HorizonAssetModel
                        {
                            AssetType = "native", Amount = amount, SourceAccountPublicKey = contract.SourceAccountId, DestinationAccountPublicKey = contract.EscrowAccountId
                        };
            var paymentOperation = await _horizonService.CreatePaymentOperationAsync(asset);
            operations.Add(paymentOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: 0);

            var response = await _horizonService.SubmitTransaction(_horizonService.SignTransaction(fundingAccount.Secret, xdrTransaction));

            if (response.IsSuccess() == false)
            {
                return null;
            }

            // set phase to be true here, no need to update sequence number
            var phase = GetNextPhase(contract);
            phase.Completed = true;
            //update contract
            contract.CurrentPhaseNumber++;

            return contract;
        }

        public async Task<Contract> CreateContractAsync(Contract contract)
        {
            // redundant function ?
            await Task.CompletedTask;
            return contract;
        }

        public async Task<bool> UpdateContractAsync(Contract contract)
        {
            var phase = GetNextPhase(contract);

            foreach (var preTransaction in phase.Transactions)
            {
                var flag = preTransaction.Signatures.All(predicate: sign => sign.Signed);

                //all signatures obtained , verify if we can submit
                if (flag && VerifyTimeBound(preTransaction.MinimumTime, preTransaction.MaximumTime))
                {
                    await _horizonService.SubmitTransaction(preTransaction.XdrString);

                    //update preTransaction
                    preTransaction.Submitted = true;

                    //update phase
                    phase.Completed = true;

                    //update contract
                    contract.CurrentSequenceNumber = await _horizonService.GetSequenceNumberAsync(contract.EscrowAccountId);
                    contract.CurrentPhaseNumber++;
                    break;
                }
            }

            return true;
        }

        public bool SignContract(ContractSignatureModel signatureModel)
        {
            // call horizon and if successful, then update signature and return true
            // otherwise return false
            var signature = signatureModel.Signature;
            var preTransaction = signature.Transaction;

            if (!VerifyTimeBound(preTransaction.MinimumTime, preTransaction.MaximumTime))
            {
                Console.WriteLine(value: "time bound delay verification failed");
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
                    Console.WriteLine(value: "secret key and public key did not match");
                    return false;
                }

                var hash = SignPreTransaction(preTransaction.XdrString, secretKey);

                if (hash != null)
                {
                    signature.SignatureHash = hash;
                    signature.Signed = true;
                    signature.SignedOn = DateTime.UtcNow;

                    return true;
                }

                //signature list did not increment
                Console.WriteLine(value: "secret key and public key did not match");
                return false;
            }

            if (signature.PublicKey == null)
            {
                return true;
            }

            //dead end
            return false;
        }

        private async Task<Contract> ConstructPhaseShipAsync(Contract contract, long PhaseAdd, long SequenceAdd)
        {
            var operations = new List<Operation>();

            //for phase x, its base sequence number +x
            var phase = new ContractPhase {Completed = false, SequenceNumber = contract.BaseSequenceNumber + SequenceAdd,
                                            Contested = false, PhaseNumber = PhaseAdd};

            //success txn, add seller as single txn signer
            //Add seller as escrow signer along with buyer and WS
            var weight = new HorizonAccountWeightModel
                         {
                             Signers = new List<HorizonAccountSignerModel> {new HorizonAccountSignerModel {Signer = contract.DestAccountId, Weight = 1}},
                             LowThreshold = 5,
                             MediumThreshold = 5,
                             HighThreshold = 6
                         };

            var setOptionsWeightOperation = _horizonService.SetOptionsWeightOperation(contract.EscrowAccountId, weight);
            operations.Add(setOptionsWeightOperation);

            var setOptionsAddSeller = _horizonService.SetOptionsSingleSignerOperation(contract.DestAccountId);
            operations.Add(setOptionsAddSeller);

            var xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: phase.SequenceNumber - 1);

            var preTransaction = new PreTransaction {XdrString = xdrTransaction};

            var publicKeys = new[] {contract.SourceAccountId, contract.DestAccountId};

            var signatureList = CreateSignatureList(preTransaction, publicKeys);

            phase.Transactions.Add(signatureList);

            //failure txn, merge escrow to buyer
            var mergeOperation = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.SourceAccountId);

            operations.Add(mergeOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: phase.SequenceNumber - 1);

            var preTransactionRide = new PreTransaction {XdrString = xdrTransaction};

            var publicKeysOverRide = new[] {contract.SourceAccountId};

            signatureList = CreateSignatureList(preTransactionRide, publicKeysOverRide);

            phase.Transactions.Add(signatureList);

            contract.Phases.Add(phase);

            return contract;
        }

        private async Task<Contract> ConstructPhaseDeliveryAsync(Contract contract, long PhaseAdd, long SequenceAdd)
        {
            var operations = new List<Operation>();

            var phase = new ContractPhase {Completed = false, SequenceNumber = contract.BaseSequenceNumber + SequenceAdd,
                                            Contested = false, PhaseNumber = PhaseAdd};

            //success txn, bump to next
            var bumpOperation = _horizonService.BumpSequenceOperation(contract.EscrowAccountId, phase.SequenceNumber + 1);

            operations.Add(bumpOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: phase.SequenceNumber - 1);

            var preTransaction = new PreTransaction {XdrString = xdrTransaction};

            var publicKeys = new[] {contract.DestAccountId};

            var signatureList = CreateSignatureList(preTransaction, publicKeys);

            phase.Transactions.Add(signatureList);

            //failure txn, bump
            bumpOperation = _horizonService.BumpSequenceOperation(contract.EscrowAccountId, phase.SequenceNumber + 2);

            operations.Add(bumpOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: phase.SequenceNumber - 1);

            var preTransactionRide = new PreTransaction {XdrString = xdrTransaction};

            signatureList = CreateSignatureList(preTransactionRide, publicKeys);

            phase.Transactions.Add(signatureList);

            contract.Phases.Add(phase);

            return contract;
        }

        private async Task<Contract> ConstructPhaseReceiptAsync(Contract contract, long PhaseAdd, long SequenceAdd)
        {
            var operations = new List<Operation>();

            var phase = new ContractPhase {Completed = false, SequenceNumber = contract.BaseSequenceNumber + SequenceAdd,
                                            Contested = false, PhaseNumber = PhaseAdd};

            //dispute txn, bump to next
            var bumpOperation = _horizonService.BumpSequenceOperation(contract.EscrowAccountId, phase.SequenceNumber + 1);

            operations.Add(bumpOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: phase.SequenceNumber - 1);

            var preTransaction = new PreTransaction {XdrString = xdrTransaction};

            var publicKeys = new[] {contract.DestAccountId, contract.SourceAccountId};

            var signatureList = CreateSignatureList(preTransaction, publicKeys);

            phase.Transactions.Add(signatureList);

            //success txn, merge txn
            var mergeOperation = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.DestAccountId);

            operations.Add(mergeOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: phase.SequenceNumber - 1);

            var preTransactionMerge = new PreTransaction {XdrString = xdrTransaction};

            signatureList = CreateSignatureList(preTransactionMerge, publicKeys);

            phase.Transactions.Add(signatureList);

            //failure txn, bump
            bumpOperation = _horizonService.BumpSequenceOperation(contract.EscrowAccountId, phase.SequenceNumber + 1);
            operations.Add(bumpOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: phase.SequenceNumber - 1);

            var preTransactionRide = new PreTransaction {XdrString = xdrTransaction};

            signatureList = CreateSignatureList(preTransactionRide, publicKeys);

            phase.Transactions.Add(signatureList);

            contract.Phases.Add(phase);

            return contract;
        }

        private async Task<Contract> ConstructPhaseDisputeAsync(Contract contract, long PhaseAdd, long SequenceAdd)
        {
            var operations = new List<Operation>();

            var phase = new ContractPhase {Completed = false, SequenceNumber = contract.BaseSequenceNumber + SequenceAdd,
                                            Contested = false, PhaseNumber = PhaseAdd};

            var weight = new HorizonAccountWeightModel
                         {
                             Signers = new List<HorizonAccountSignerModel>
                                       {
                                           new HorizonAccountSignerModel {Signer = contract.DestAccountId, Weight = 0},
                                           new HorizonAccountSignerModel {Signer = _worldSquareAccount.PublicKey, Weight = 4}
                                       },
                             LowThreshold = 4,
                             MediumThreshold = 4,
                             HighThreshold = 4
                         };

            var setOptionsWeightOperation = _horizonService.SetOptionsWeightOperation(contract.EscrowAccountId, weight);
            operations.Add(setOptionsWeightOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: phase.SequenceNumber - 1);

            var preTransaction = new PreTransaction {XdrString = xdrTransaction};

            var publicKeys = new[] {contract.DestAccountId};

            var signatureList = CreateSignatureList(preTransaction, publicKeys);

            phase.Transactions.Add(signatureList);

            contract.Phases.Add(phase);

            return contract;
        }

        private async Task<Contract> ConstructPhaseResolutionAsync(Contract contract, long PhaseAdd, long SequenceAdd)
        {
            var operations = new List<Operation>();

            var phase = new ContractPhase {Completed = false, SequenceNumber = contract.BaseSequenceNumber + SequenceAdd,
                                            Contested = false, PhaseNumber = PhaseAdd};

            //release txn, merge txn
            var mergeOperation = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.DestAccountId);

            operations.Add(mergeOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: phase.SequenceNumber - 1);

            var preTransactionMerge = new PreTransaction {XdrString = xdrTransaction};

            var publicKeys = new[] {contract.DestAccountId};

            var signatureList = CreateSignatureList(preTransactionMerge, publicKeys);

            phase.Transactions.Add(signatureList);

            //refund txn, merge txn
            mergeOperation = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.SourceAccountId);

            operations.Add(mergeOperation);

            xdrTransaction = await _horizonService.CreateTransaction(contract.EscrowAccountId, operations, time: null, sequence: phase.SequenceNumber - 1);

            var preTransactionMergeRefund = new PreTransaction {XdrString = xdrTransaction};

            signatureList = CreateSignatureList(preTransactionMergeRefund, publicKeys);

            phase.Transactions.Add(signatureList);

            contract.Phases.Add(phase);

            return contract;
        }

        private static ContractPhase GetNextPhase(Contract contract)
        {
            var enumerator = contract.Phases.GetEnumerator();

            for (long i = 0; i <= contract.CurrentPhaseNumber; i++)
            {
                enumerator.MoveNext();
            }
            return enumerator.Current;

        }
        private static long GetCurrentTimeInSeconds()
        {
            return (long) (DateTime.UtcNow - new DateTime(year: 1970, month: 1, day: 1)).TotalSeconds;
        }

        private static long ConvertDateToSeconds(DateTime date)
        {
            return (long) (date.ToUniversalTime() - new DateTime(year: 1970, month: 1, day: 1)).TotalSeconds;
        }

        private static bool VerifyTimeBound(DateTime minimum, DateTime maximum)
        {
            var noDelay = true;

            var currentTime = GetCurrentTimeInSeconds();

            if (currentTime < ConvertDateToSeconds(minimum))
            {
                noDelay = false;
            }

            if (currentTime > ConvertDateToSeconds(maximum))
            {
                noDelay = false;
            }

            return noDelay;
        }
        private PreTransaction CreateSignatureList(PreTransaction preTransaction, IReadOnlyCollection<string> publicKeys)
        {
            foreach (var key in publicKeys)
            {
                var sign = new UserSignature {PublicKey = key, Signed = false, Transaction = preTransaction};

                preTransaction.Signatures.Add(sign);
            }

            //System Signature is common for all type of pre-transaction
            Signature systemSignature = new SystemSignature {PublicKey = _worldSquareAccount.PublicKey, Signed = false, Transaction = preTransaction};

            preTransaction.Signatures.Add(systemSignature);

            return preTransaction;
        }
        private string SignPreTransaction(string xdrTransaction, string secretKey)
        {
            int count, newcount = 0;

            count = _horizonService.GetSignatureCount(xdrTransaction);

            _horizonService.SignTransaction(secretKey, xdrTransaction);

            newcount = _horizonService.GetSignatureCount(xdrTransaction);

            if (count + 1 == newcount)
            {
                return _horizonService.SignatureHash(xdrTransaction, newcount - 1);
            }

            return null;
        }
    }
}