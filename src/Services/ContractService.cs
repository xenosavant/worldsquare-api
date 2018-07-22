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
	private long _sequence;

	public ContractService(IHorizonService horizon)
	{
		_horizon = horizon;
	}
	//TBD: Add WorldSquare account as signer
	//TBD: Add async and await
	public async Task<ContractModel> SetupContract(ContractParamModel ContractParam)
	{
		HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
		HorizonAccountSignerModel dest_account = new HorizonAccountSignerModel();
		weight.Signers = new List<HorizonAccountSignerModel>();
		ContractModel Contract = new ContractModel();

		Contract.Txn = new List<SubmitTransactionResponse>();
		HorizonKeyPairModel escrow = _horizon.CreateAccount();
		//TBD: consider other assets too
		Contract.Txn.Add(await _horizon.TransferNativeFund(ContractParam.SourceAccount, escrow.PublicKey,
			ContractParam.Asset.Amount));

		weight.LowThreshold = 2;
		weight.MediumThreshold = 2;
		weight.HighThreshold = 2;
		dest_account.Signer = ContractParam.DestAccount;
		dest_account.Weight = 1;
		weight.Signers.Add(dest_account);

		Contract.Txn.Add(await _horizon.SetWeightSigner(escrow, weight));
		_sequence = await _horizon.GetSequenceNumber(escrow.PublicKey);
		Contract.EscrowAccount = escrow;
		Contract.DestAccount = ContractParam.DestAccount;
		Contract.Sequence = _sequence;
		Contract.State = ContractState.Initial;
		return Contract;
	}
	public async Task<ContractModel> CreateContract(ContractParamModel ContractParam, ContractModel Contract)
	{		
		return Contract;
	}
	public string SignContract(HorizonKeyPairModel Account, ContractModel Contract)
	{
		string hash = "";
		return hash;
	}
	public string ExecuteContract(ContractModel Contract)
	{
		string hash = "";
		return hash;
	}
   }
}
