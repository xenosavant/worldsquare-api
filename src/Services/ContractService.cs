using stellar_dotnet_sdk.responses;
using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
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
		HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
		HorizonAccountSignerModel dest_account = new HorizonAccountSignerModel();
		weight.Signers = new List<HorizonAccountSignerModel>();

		Contract.Txn = new List<SubmitTransactionResponse>();
		HorizonKeyPairModel escrow = _horizon.CreateAccount();
            //TBD: consider other assets too
        string txnxdr = await _horizon.TransferNativeFund(ContractParam.SourceAccount, escrow.PublicKey,
                ContractParam.Asset.Amount);
        Contract.Txn.Add(await _horizon.SubmitTxn(txnxdr));

		weight.LowThreshold = 2;
		weight.MediumThreshold = 2;
		weight.HighThreshold = 2;
		dest_account.Signer = ContractParam.DestAccount;
		dest_account.Weight = 1;
		weight.Signers.Add(dest_account);

        txnxdr = await _horizon.SetWeightSigner(escrow, weight);
        Contract.Txn.Add(await _horizon.SubmitTxn(txnxdr));
		Contract.Sequence = await _horizon.GetSequenceNumber(escrow.PublicKey);
		Contract.EscrowAccount = escrow;
		Contract.DestAccount = ContractParam.DestAccount;
		Contract.State = ContractState.Initial;
		return 1;
	}
	public async Task<int> CreateContract(ContractParamModel ContractParam)
	{		
		return 1;
	}
	public string SignContract(HorizonKeyPairModel Account)
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
