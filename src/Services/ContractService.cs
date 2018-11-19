using stellar_dotnet_sdk;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<Contract> SetupContract(ContractParameterModel contractParameterModel)
        {
            var escrow = _horizonService.CreateAccount();

            var sequenceNumber = await _horizonService.GetSequenceNumber(escrow.PublicKey);

            var contract = new Contract
            {
                EscrowAccountId = escrow.PublicKey,
                DestAccountId = contractParameterModel.DestinationAccount,
                SourceAccountId = contractParameterModel.SourceAccount,
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

        public async Task<Contract> FundContract(Contract contract, ContractParameterModel contractParameterModel)
        {
            var phaseOne = new ContractPhase();
            var operations = new List<Operation>();

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
            destinationAccount.Signer = contractParameterModel.DestinationAccount;
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

        private async Task<Contract> ConstructPhaseTwo(Contract contract)
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
            var pretxnride = new PreTransaction();
            pretxnride.XdrString = xdrTransaction;
            phaseTwo.Transactions.Add(
                CreateSignatureList(pretxnride, new[] {contract.DestAccountId, contract.EscrowAccountId}));

            contract.Phases.Add(phaseTwo);
            return contract;
        }

        private async Task<Contract> ConstructPhase3(Contract contract)
        {
            var Phase3 = new ContractPhase();
            var ops = new List<Operation>();

            Phase3.SequenceNumber = contract.BaseSequenceNumber + 3;

            //success txn, bump to next
            var BumpOp =
                _horizonService.BumpSequenceOperation(contract.EscrowAccountId, contract.BaseSequenceNumber + (2 + 1));
            ops.Add(BumpOp);
            var txnxdr =
                await _horizonService.CreateTransaction(contract.EscrowAccountId, ops, null, Phase3.SequenceNumber - 1);
            var pretxn = new PreTransaction();
            pretxn.XdrString = txnxdr;
            Phase3.Transactions.Add(CreateSignatureList(pretxn, new[] {contract.DestAccountId, contract.EscrowAccountId}));

            //failure txn, bump
            BumpOp = _horizonService.BumpSequenceOperation(contract.EscrowAccountId, contract.BaseSequenceNumber + (2 + 2));
            ops.Add(BumpOp);
            txnxdr = await _horizonService.CreateTransaction(contract.EscrowAccountId, ops, null, Phase3.SequenceNumber - 1);
            var pretxnride = new PreTransaction();
            pretxnride.XdrString = txnxdr;
            Phase3.Transactions.Add(
                CreateSignatureList(pretxnride, new[] {contract.DestAccountId, contract.EscrowAccountId}));

            contract.Phases.Add(Phase3);
            return contract;
        }

        private async Task<Contract> ConstructPhase4(Contract contract)
        {
            var Phase4 = new ContractPhase();
            var ops = new List<Operation>();

            Phase4.SequenceNumber = contract.BaseSequenceNumber + 4;

            //dispute txn, bump to next
            var BumpOp =
                _horizonService.BumpSequenceOperation(contract.EscrowAccountId, contract.BaseSequenceNumber + (3 + 1));
            ops.Add(BumpOp);
            var txnxdr =
                await _horizonService.CreateTransaction(contract.EscrowAccountId, ops, null, Phase4.SequenceNumber - 1);
            var pretxn = new PreTransaction();
            pretxn.XdrString = txnxdr;
            Phase4.Transactions.Add(CreateSignatureList(pretxn, new[] {contract.DestAccountId, contract.EscrowAccountId}));

            //success txn, merge txn
            var MergeOp = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.DestAccountId);
            ops.Add(MergeOp);
            txnxdr = await _horizonService.CreateTransaction(contract.EscrowAccountId, ops, null, Phase4.SequenceNumber - 1);
            var pretxnmerge = new PreTransaction();
            pretxnmerge.XdrString = txnxdr;
            Phase4.Transactions.Add(CreateSignatureList(pretxnmerge,
                new[] {contract.DestAccountId, contract.EscrowAccountId}));

            //failure txn, bump
            BumpOp = _horizonService.BumpSequenceOperation(contract.EscrowAccountId, contract.BaseSequenceNumber + (3 + 1));
            ops.Add(BumpOp);
            txnxdr = await _horizonService.CreateTransaction(contract.EscrowAccountId, ops, null, Phase4.SequenceNumber - 1);
            var pretxnride = new PreTransaction();
            pretxnride.XdrString = txnxdr;
            Phase4.Transactions.Add(
                CreateSignatureList(pretxnride, new[] {contract.DestAccountId, contract.EscrowAccountId}));

            contract.Phases.Add(Phase4);
            return contract;
        }

        private async Task<Contract> ConstructPhase4Dispute(Contract contract)
        {
            var Phase4D = new ContractPhase();
            var ops = new List<Operation>();

            Phase4D.SequenceNumber = contract.BaseSequenceNumber + 5;

            var weight = new HorizonAccountWeightModel();
            var dest_account = new HorizonAccountSignerModel();
            var ws_account = new HorizonAccountSignerModel();
            weight.Signers = new List<HorizonAccountSignerModel>();

            weight.LowThreshold = 5;
            weight.MediumThreshold = 5;
            weight.HighThreshold = 6;

            dest_account.Signer = contract.DestAccountId;
            dest_account.Weight = 1;
            weight.Signers.Add(dest_account);
            ws_account.Signer = _worldSquareAccount.PublicKey;
            ws_account.Weight = 4;
            weight.Signers.Add(ws_account);
            //Let the SignerSecret be null
            weight.SignerSecret = null;

            var SetOptionsOp = _horizonService.SetOptionsOperation(contract.EscrowAccountId, weight);
            ops.Add(SetOptionsOp);

            var txnxdr =
                await _horizonService.CreateTransaction(contract.EscrowAccountId, ops, null, Phase4D.SequenceNumber - 1);

            var pretxn = new PreTransaction();
            pretxn.XdrString = txnxdr;
            Phase4D.Transactions.Add(CreateSignatureList(pretxn, new[] {contract.DestAccountId, contract.EscrowAccountId}));

            contract.Phases.Add(Phase4D);
            return contract;
        }

        private async Task<Contract> ConstructPhase5(Contract contract)
        {
            var Phase5 = new ContractPhase();
            var ops = new List<Operation>();

            Phase5.SequenceNumber = contract.BaseSequenceNumber + 6;

            //release txn, merge txn
            var MergeOp = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.DestAccountId);
            ops.Add(MergeOp);
            var txnxdr =
                await _horizonService.CreateTransaction(contract.EscrowAccountId, ops, null, Phase5.SequenceNumber - 1);
            var pretxnmerge = new PreTransaction();
            pretxnmerge.XdrString = txnxdr;
            Phase5.Transactions.Add(CreateSignatureList(pretxnmerge,
                new[] {contract.DestAccountId, contract.EscrowAccountId}));

            //refund txn, merge txn
            MergeOp = _horizonService.CreateAccountMergeOperation(contract.EscrowAccountId, contract.SourceAccountId);
            ops.Add(MergeOp);
            txnxdr = await _horizonService.CreateTransaction(contract.EscrowAccountId, ops, null, Phase5.SequenceNumber - 1);
            var pretxnmerge2 = new PreTransaction();
            pretxnmerge2.XdrString = txnxdr;
            Phase5.Transactions.Add(CreateSignatureList(pretxnmerge2,
                new[] {contract.DestAccountId, contract.EscrowAccountId}));

            contract.Phases.Add(Phase5);
            return contract;
        }

        public async Task<Contract> CreateContract(Contract contract)
        {
            contract = await ConstructPhaseTwo(contract);
            contract = await ConstructPhase3(contract);
            contract = await ConstructPhase4(contract);
            contract = await ConstructPhase4Dispute(contract);
            contract = await ConstructPhase5(contract);
            return contract;
        }

        private long GetCurrentTimeinSeconds()
        {
            return (long) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private long ConvertDatetoSeconds(DateTime Date)
        {
            return (long) (Date.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private bool VerifyTimeBound(DateTime minimum, DateTime maximum)
        {
            bool NoDelay = true;
            long CurrentTime = GetCurrentTimeinSeconds();
            if(minimum != null && CurrentTime < ConvertDatetoSeconds(minimum)) {
                NoDelay = false;
            }
            if(maximum != null && CurrentTime > ConvertDatetoSeconds(maximum)) {
                NoDelay = false;
            }
            return NoDelay;
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

            foreach (var pretxn in phase.Transactions)
            foreach (var sign in pretxn.Signatures)
                if (sign.Signed)
                {
                    //all signatures obtained , verify if we can submit
                }
                else
                {
                    break;
                }
        }

        private string AddSign(string XdrString, string secretKey)
        {
            int count, newcount = 0;
            count = _horizonService.GetSignatureCount(XdrString);
            _horizonService.SignTransaction(null, secretKey, XdrString);
            newcount = _horizonService.GetSignatureCount(XdrString);
            if (count + 1 == newcount)
                return _horizonService.SignatureHash(XdrString, newcount - 1);
            return null;
        }

        public async Task<bool> SignContract(ContractSignatureModel SignatureModel)
        {
            // call horizon and if successful, then update signature and return true
            // otherwise return false
            var signature = SignatureModel.Signature;
            var Pretransaction = signature.Transaction;

            if(! VerifyTimeBound(Pretransaction.MinimumTime, Pretransaction.MaximumTime)) {
                Console.WriteLine("time bound delay verification failed");
                return false;
            }
            if (signature.PublicKey != null)
            {
                var secretKey = "";

                if (SignatureModel.Secret == null)
                {
                    //system signature
                    secretKey = string.Copy(_worldSquareAccount.SecretKey);
                }
                else if (_horizonService.GetPublicKey(SignatureModel.Secret) == signature.PublicKey)
                {
                    //user signature
                    secretKey = string.Copy(SignatureModel.Secret);
                }
                else
                {
                    //secret key and public key did not match
                    Console.WriteLine("secret key and public key did not match");
                    return false;
                }

                var hash = AddSign(Pretransaction.XdrString, secretKey);
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
            var hash = "";
            return hash;
        }
    }
}