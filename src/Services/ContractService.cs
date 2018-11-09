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
		contract.CurrentSequenceNumber = await _horizon.GetSequenceNumber(escrow.PublicKey);
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
		HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
		HorizonAccountSignerModel dest_account = new HorizonAccountSignerModel();
		HorizonAccountSignerModel ws_account = new HorizonAccountSignerModel();
		weight.Signers = new List<HorizonAccountSignerModel>();
		var ops = new List<Operation>();
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

		var txnxdr = await _horizon.CreateTxn(contract.EscrowAccountId, ops, null);
		/*
        var response = await _horizon.SubmitTxn(_horizon.SignTxn(contract.EscrowAccountId, txnxdr));
		if(response.IsSuccess() == false)
			return null;*/

		contract.CurrentSequenceNumber = await _horizon.GetSequenceNumber(contract.EscrowAccountId);
		contract.ContractStateId = (int)ContractState.Initial;

		var Phase1 = new ContractPhase();
		Phase1.Completed=true;
		Phase1.SequenceNumber=contract.CurrentSequenceNumber;
		PreTransaction pretxn = new PreTransaction();
		pretxn.XdrString = txnxdr;
		Phase1.Transactions.Add(pretxn);

		contract.Phases.Add(Phase1);
		return contract;
	}
	public async Task<Contract> CreateContract(Contract contract)
	{
		//todo : Add all phases pre txn here.
		var ops = new List<Operation>();
		HorizonTimeBoundModel Time = new HorizonTimeBoundModel();
		//Time.MinTime = ContractParam.MinTime;
		//Time.MaxTime = ContractParam.MaxTime;
		var MergeOp = _horizon.CreateAccountMergeOps(contract.EscrowAccountId, contract.DestAccountId);
		ops.Add(MergeOp);
		//save the xdr
		var pretxn1 = new PreTransaction();
		pretxn1.XdrString = await _horizon.CreateTxn(contract.EscrowAccountId, ops, Time);
		//Contract.PreTransactions.Add(pretxn1);

		// Remove changeweight since we will be using Voting to resolve dispute
		/*
		HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
		HorizonTimeBoundModel Time = new HorizonTimeBoundModel();
		//make escrow threshold weights as 3,
		//WorldSquare (2) + Buyer (1) or WorldSquare (2) + Seller (1)
		weight.LowThreshold = 3;
		weight.MediumThreshold = 3;
		weight.HighThreshold = 3;
		Time.MinTime = ContractParam.MinTime;
		Time.MaxTime = ContractParam.MaxTime;
		var SetOptionsOp = _horizon.SetOptionsOp(ContractParam.EscrowAccount, weight);
		ops.Add(SetOptionsOp);
		//save the xdr
		var pretxn2 = new ContractPreTxnModel();
		pretxn2.XdrString = await _horizon.CreateTxn(ContractParam.EscrowAccount, ops, Time);
		//Contract.PreTransactions.Add(pretxn2);
		*/
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
