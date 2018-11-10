using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Services
{
    public class ContractService : IContractService
    {
	private readonly IHorizonService _horizon;
	private  HorizonKeyPairModel WorldSquareAccount;
	public ContractService(IHorizonService horizon)
	{
		_horizon = horizon;
	}

	public async Task<Contract> SetupContract(ContractParamModel ContractParam)
	{
		var contract = new Contract();

		HorizonKeyPairModel escrow = _horizon.CreateAccount();
		contract.EscrowAccountId = escrow.PublicKey;
		contract.DestAccountId = ContractParam.DestAccount;
		contract.SourceAccountId = ContractParam.SourceAccount;
		contract.BaseSequenceNumber = contract.CurrentSequenceNumber =
							await _horizon.GetSequenceNumber(escrow.PublicKey);
		contract.ContractStateId = (int)ContractState.Initial;
		contract.ContractTypeId = 0;

		var Phase0 = new ContractPhase();
		Phase0.Completed=true;
		Phase0.SequenceNumber=contract.CurrentSequenceNumber;

		contract.Phases.Add(Phase0);
		return contract;
	}
	public async Task<Contract> FundContract(Contract contract, ContractParamModel ContractParam)
	{
		var Phase1 = new ContractPhase();
		var ops = new List<Operation>();

		//for phase 1, its base sequence number +1
		Phase1.SequenceNumber=contract.BaseSequenceNumber + 1;

		HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
		HorizonAccountSignerModel dest_account = new HorizonAccountSignerModel();
		HorizonAccountSignerModel ws_account = new HorizonAccountSignerModel();
		weight.Signers = new List<HorizonAccountSignerModel>();
		//Transfer funds tp Escrow
        //TBD: consider other assets too
		//TBD: transfer 1 % to WorldSquare 
		var PaymentOp = _horizon.CreatePaymentOps(contract.SourceAccountId, contract.EscrowAccountId,
                ContractParam.Asset.Amount);
		ops.Add(PaymentOp);

		//Escrow threshold weights are 4
		weight.LowThreshold = 5;
		weight.MediumThreshold = 5;
		weight.HighThreshold = 6;
		//escrow master weight (1) + dest weight (1) + WorldSquare (4)
		//dest account has weight 1
		dest_account.Signer = ContractParam.DestAccount;
		dest_account.Weight = 1;
		weight.Signers.Add(dest_account);
		ws_account.Signer = WorldSquareAccount.PublicKey;
		ws_account.Weight = 4;
		weight.Signers.Add(ws_account);
		//Let the SignerSecret be null
		weight.SignerSecret = null;

		var SetOptionsOp = _horizon.SetOptionsOp(contract.EscrowAccountId, weight);
		ops.Add(SetOptionsOp);

		var txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase1.SequenceNumber-1);
		/*
        var response = await _horizon.SubmitTxn(_horizon.SignTxn(contract.EscrowAccountId, txnxdr));
		if(response.IsSuccess() == false)
			return null;*/
		PreTransaction pretxn = new PreTransaction();
		pretxn.XdrString = txnxdr;
		Phase1.Transactions.Add(pretxn);

		contract.Phases.Add(Phase1);
		return contract;
	}
	private async Task<Contract> ConstructPhase2(Contract contract)
	{
		var Phase2 = new ContractPhase();
		var ops = new List<Operation>();

		//for phase 1, its base sequence number +1
		Phase2.SequenceNumber=contract.BaseSequenceNumber + 2;
		//success txn, bump to next
		var BumpOp = _horizon.BumpSequenceOps(contract.EscrowAccountId, contract.BaseSequenceNumber + (1+1));
		ops.Add(BumpOp);
		var txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase2.SequenceNumber-1);
		PreTransaction pretxn = new PreTransaction();
		pretxn.XdrString = txnxdr;
		Phase2.Transactions.Add(pretxn);

		//failure txn, bump
		//ToDo: replace bump sequence with transfer fund to source account
		BumpOp = _horizon.BumpSequenceOps(contract.EscrowAccountId, contract.BaseSequenceNumber + (1+3));
		ops.Add(BumpOp);
		txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase2.SequenceNumber-1);
		PreTransaction pretxnride = new PreTransaction();
		pretxnride.XdrString = txnxdr;
		Phase2.Transactions.Add(pretxnride);

		contract.Phases.Add(Phase2);
		return contract;
	}
	private async Task<Contract> ConstructPhase3(Contract contract)
	{
		var Phase3 = new ContractPhase();
		var ops = new List<Operation>();

		Phase3.SequenceNumber=contract.BaseSequenceNumber + 3;

		//success txn, bump to next
		var BumpOp = _horizon.BumpSequenceOps(contract.EscrowAccountId, contract.BaseSequenceNumber + (2+1));
		ops.Add(BumpOp);
		var txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase3.SequenceNumber-1);
		PreTransaction pretxn = new PreTransaction();
		pretxn.XdrString = txnxdr;
		Phase3.Transactions.Add(pretxn);

		//failure txn, bump
		BumpOp = _horizon.BumpSequenceOps(contract.EscrowAccountId, contract.BaseSequenceNumber + (2+2));
		ops.Add(BumpOp);
		txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase3.SequenceNumber-1);
		PreTransaction pretxnride = new PreTransaction();
		pretxnride.XdrString = txnxdr;
		Phase3.Transactions.Add(pretxnride);

		contract.Phases.Add(Phase3);
		return contract;
	}
	private async Task<Contract> ConstructPhase4(Contract contract)
	{
		var Phase4 = new ContractPhase();
		var ops = new List<Operation>();

		Phase4.SequenceNumber=contract.BaseSequenceNumber + 4;

		//dispute txn, bump to next
		var BumpOp = _horizon.BumpSequenceOps(contract.EscrowAccountId, contract.BaseSequenceNumber + (3+1));
		ops.Add(BumpOp);
		var txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase4.SequenceNumber-1);
		PreTransaction pretxn = new PreTransaction();
		pretxn.XdrString = txnxdr;
		Phase4.Transactions.Add(pretxn);

		//success txn, merge txn
		var MergeOp = _horizon.CreateAccountMergeOps(contract.EscrowAccountId, contract.DestAccountId);
		ops.Add(MergeOp);
		txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase4.SequenceNumber-1);
		PreTransaction pretxnmerge = new PreTransaction();
		pretxnmerge.XdrString = txnxdr;
		Phase4.Transactions.Add(pretxnmerge);

		//failure txn, bump
		BumpOp = _horizon.BumpSequenceOps(contract.EscrowAccountId, contract.BaseSequenceNumber + (3+1));
		ops.Add(BumpOp);
		txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase4.SequenceNumber-1);
		PreTransaction pretxnride = new PreTransaction();
		pretxnride.XdrString = txnxdr;
		Phase4.Transactions.Add(pretxnride);

		contract.Phases.Add(Phase4);
		return contract;
	}
	private async Task<Contract> ConstructPhase4Dispute(Contract contract)
	{
		var Phase4D = new ContractPhase();
		var ops = new List<Operation>();

		Phase4D.SequenceNumber=contract.BaseSequenceNumber + 5;

		HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
		HorizonAccountSignerModel dest_account = new HorizonAccountSignerModel();
		HorizonAccountSignerModel ws_account = new HorizonAccountSignerModel();
		weight.Signers = new List<HorizonAccountSignerModel>();

		weight.LowThreshold = 5;
		weight.MediumThreshold = 5;
		weight.HighThreshold = 6;

		dest_account.Signer = contract.DestAccountId;
		dest_account.Weight = 1;
		weight.Signers.Add(dest_account);
		ws_account.Signer = WorldSquareAccount.PublicKey;
		ws_account.Weight = 4;
		weight.Signers.Add(ws_account);
		//Let the SignerSecret be null
		weight.SignerSecret = null;

		var SetOptionsOp = _horizon.SetOptionsOp(contract.EscrowAccountId, weight);
		ops.Add(SetOptionsOp);

		var txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase4D.SequenceNumber-1);

		PreTransaction pretxn = new PreTransaction();
		pretxn.XdrString = txnxdr;
		Phase4D.Transactions.Add(pretxn);

		contract.Phases.Add(Phase4D);
		return contract;
	}
	private async Task<Contract> ConstructPhase5(Contract contract)
	{
		var Phase5 = new ContractPhase();
		var ops = new List<Operation>();

		Phase5.SequenceNumber=contract.BaseSequenceNumber + 6;

		//release txn, merge txn
		var MergeOp = _horizon.CreateAccountMergeOps(contract.EscrowAccountId, contract.DestAccountId);
		ops.Add(MergeOp);
		var txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase5.SequenceNumber-1);
		PreTransaction pretxnmerge = new PreTransaction();
		pretxnmerge.XdrString = txnxdr;
		Phase5.Transactions.Add(pretxnmerge);

		//refund txn, merge txn
		MergeOp = _horizon.CreateAccountMergeOps(contract.EscrowAccountId, contract.SourceAccountId);
		ops.Add(MergeOp);
		txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null, Phase5.SequenceNumber-1);
		PreTransaction pretxnmerge2 = new PreTransaction();
		pretxnmerge2.XdrString = txnxdr;
		Phase5.Transactions.Add(pretxnmerge2);

		contract.Phases.Add(Phase5);
		return contract;
	}
	public async Task<Contract> CreateContract(Contract contract)
	{
		contract = await ConstructPhase2(contract);
		contract = await ConstructPhase3(contract);
		contract = await ConstructPhase4(contract);
		contract = await ConstructPhase4Dispute(contract);
		contract = await ConstructPhase5(contract);
		return contract;
	}

    public void UpdateContract(Contract contract)
    {
       // Update the contract here
    }

	public string SignContract(ContractSignatureModel signature)
	{
		string hash = "";
		return hash;
	}
	public string ExecuteContract()
	{
		string hash = "";
		return hash;
	}
   }
}
