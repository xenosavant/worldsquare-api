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
	private  HorizonKeyPairModel WorldSquareAccount;
	public ContractService(IHorizonService horizon)
	{
		_horizon = horizon;
	}

	public async Task<Contract> SetupContract()
	{
		var ContractSetup = new Contract();

		HorizonKeyPairModel escrow = _horizon.CreateAccount();
		ContractSetup.EscrowAccountId = escrow.PublicKey;
		ContractSetup.CurrentSequenceNumber = await _horizon.GetSequenceNumber(escrow.PublicKey);
		ContractSetup.ContractStateId = (int)ContractState.Initial;
		ContractSetup.ContractTypeId = 0;
		return ContractSetup;
	}
	public async Task<Contract> FundContract(ContractParamModel ContractParam, Contract ContractFund)
	{
		HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
		HorizonAccountSignerModel dest_account = new HorizonAccountSignerModel();
		HorizonAccountSignerModel ws_account = new HorizonAccountSignerModel();
		weight.Signers = new List<HorizonAccountSignerModel>();
		var ops = new List<Operation>();
		//Transfer funds tp Escrow
        //TBD: consider other assets too
		//TBD: transfer 1 % to WorldSquare 
		var PaymentOp = _horizon.CreatePaymentOps(ContractParam.SourceAccount, ContractParam.EscrowAccount.PublicKey,
                ContractParam.Asset.Amount);
		ops.Add(PaymentOp);
		var txnxdr = await _horizon.CreateTxn(ContractParam.SourceAccount, ops, null);
		await _horizon.SubmitTxn(_horizon.SignTxn(ContractParam.SourceAccount, txnxdr));
		//clear ops
		ops.Clear();
		//Escrow threshold weights are 4
		weight.LowThreshold = 5;
		weight.MediumThreshold = 5;
		weight.HighThreshold = 7;
		//escrow master weight (1) + dest weight (1) + WorldSquare (2)
		//dest account has weight 1
		dest_account.Signer = ContractParam.DestAccount;
		dest_account.Weight = 1;
		weight.Signers.Add(dest_account);
		//Make sure that WorldSquare weight is added as 2
		ws_account.Signer = WorldSquareAccount.PublicKey;
		ws_account.Weight = 4;
		weight.Signers.Add(ws_account);
		//Let the SignerSecret be null
		weight.SignerSecret = null;

		var SetOptionsOp = _horizon.SetOptionsOp(ContractParam.EscrowAccount, weight);
		ops.Add(SetOptionsOp);

		txnxdr = await _horizon.CreateTxn(ContractParam.EscrowAccount, ops, null);
        await _horizon.SubmitTxn(_horizon.SignTxn(ContractParam.EscrowAccount, txnxdr));

		ContractFund.EscrowAccountId = ContractParam.EscrowAccount.PublicKey;
		ContractFund.CurrentSequenceNumber = await _horizon.GetSequenceNumber(ContractParam.EscrowAccount.PublicKey);
		ContractFund.ContractStateId = (int)ContractState.Initial;
		ContractFund.ContractTypeId = 0;
		return ContractFund;
	}
	public async Task<Contract> CreateContract(Contract contract)
	{
/*
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
		}
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
