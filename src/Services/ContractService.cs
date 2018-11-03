﻿using stellar_dotnet_sdk;
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
	public ContractModel Contract {get; private set;}

	public ContractService(IHorizonService horizon)
	{
		_horizon = horizon;
	}
	//TBD: Add WorldSquare account as signer
	//TBD: Add async and await
	public async Task<int> SetupContract(ContractParamModel ContractParam)
	{
		if (ContractParam.Type != ContractPreTnxType.Setup)
			return 0;
		HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
		HorizonAccountSignerModel dest_account = new HorizonAccountSignerModel();
		HorizonAccountSignerModel ws_account = new HorizonAccountSignerModel();
		weight.Signers = new List<HorizonAccountSignerModel>();
		Contract.Txn = new List<SubmitTransactionResponse>();
		var ops = new List<Operation>();
		//Create Escrow Account
		HorizonKeyPairModel escrow = _horizon.CreateAccount();
		//Transfer funds tp Escrow
        //TBD: consider other assets too
		//TBD: transfer 1 % to WorldSquare 
		var PaymentOp = _horizon.CreatePaymentOps(ContractParam.SourceAccount, escrow.PublicKey,
                ContractParam.Asset.Amount);
		ops.Add(PaymentOp);
		var txnxdr = await _horizon.CreateTxn(ContractParam.SourceAccount, ops, null);
		Contract.Txn.Add(await _horizon.SubmitTxn(_horizon.SignTxn(escrow, txnxdr)));
		//clear ops
		ops.Clear();
		//Escrow threshold weights are 4
		weight.LowThreshold = 4;
		weight.MediumThreshold = 4;
		weight.HighThreshold = 4;
		//escrow master weight (1) + dest weight (1) + WorldSquare (2)
		//dest account has weight 1
		dest_account.Signer = ContractParam.DestAccount;
		dest_account.Weight = 1;
		weight.Signers.Add(dest_account);
		//Make sure that WorldSquare weight is added as 2
		ws_account.Signer = Contract.WorldSquareAccount.PublicKey;
		ws_account.Weight = 2;
		weight.Signers.Add(ws_account);
		//Let the SignerSecret be null
		weight.SignerSecret = null;

		var SetOptionsOp = _horizon.SetOptionsOp(escrow, weight);
		ops.Add(SetOptionsOp);

		txnxdr = await _horizon.CreateTxn(escrow, ops, null);
        Contract.Txn.Add(await _horizon.SubmitTxn(_horizon.SignTxn(escrow, txnxdr)));
		Contract.Sequence = await _horizon.GetSequenceNumber(escrow.PublicKey);
		Contract.EscrowAccount = escrow;
		Contract.DestAccount = ContractParam.DestAccount;
		Contract.CurrentState = ContractState.Initial;
		Contract.CurrentPhase = ContractPhaseType.NoPhase;
		return 1;
	}
	public async Task<int> CreateContract(ContractParamModel ContractParam)
	{
		var ops = new List<Operation>();
		if (ContractParam.Type == ContractPreTnxType.PreTxnAccountMerge) {
			HorizonTimeBoundModel Time = new HorizonTimeBoundModel();
			Time.MinTime = ContractParam.MinTime;
			Time.MaxTime = ContractParam.MaxTime;
			var MergeOp = _horizon.CreateAccountMergeOps(Contract.EscrowAccount, Contract.DestAccount);
			ops.Add(MergeOp);
			//save the xdr
			var pretxn1 = new ContractPreTxnModel();
			pretxn1.XdrString = await _horizon.CreateTxn(ContractParam.EscrowAccount, ops, Time);
			Contract.PreTransactions.Add(pretxn1);
		} else if (ContractParam.Type == ContractPreTnxType.PreTxnSetWeight) {
			HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
			HorizonTimeBoundModel Time = new HorizonTimeBoundModel();
			//make escrow threshold weights as 3, 
			//WorldSquare (2) + Buyer (1) or WorldSquare (2) + Seller (1)
			weight.LowThreshold = 3;
			weight.MediumThreshold = 3;
			weight.HighThreshold = 3;
			Time.MinTime = ContractParam.MinTime;
			Time.MaxTime = ContractParam.MaxTime;
			var SetOptionsOp = _horizon.SetOptionsOp(Contract.EscrowAccount, weight);
			ops.Add(SetOptionsOp);
			//save the xdr
			var pretxn2 = new ContractPreTxnModel();
			pretxn2.XdrString = await _horizon.CreateTxn(ContractParam.EscrowAccount, ops, Time);
			Contract.PreTransactions.Add(pretxn2);
		} else {
			return 0;
		}
		return 1;
	}

    public void UpdateContract(Contract contract)
    {
       // Update the contract here
    }

	public string SignContract(ContractSignatureModel signature)
	{
		string hash = "";
		if(Contract.CurrentPhase != ContractPhaseType.NoPhase)
		{
			//todo
		}
		return hash;
	}
	public string ExecuteContract()
	{
		string hash = "";
		return hash;
	}
   }
}
