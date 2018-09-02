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
	private long _sequence;

	public ContractService(IHorizonService horizon)
	{
		_horizon = horizon;
	}
	//TBD: Add WorldSquare account as signer
	//TBD: Add async and await
	public async Task<HorizonKeyPairModel> SetupContract(HorizonKeyPairModel SourceAccount, string DestAccount,
						string Amount)
	{
		HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
		HorizonAccountSignerModel dest_account = new HorizonAccountSignerModel();
		weight.Signers = new List<HorizonAccountSignerModel>();

		HorizonKeyPairModel escrow = _horizon.CreateAccount();
		//TBT: catch the return param
		await _horizon.TransferNativeFund(SourceAccount, escrow.PublicKey, Amount);

		weight.LowThreshold = 2;
		weight.MediumThreshold = 2;
		weight.HighThreshold = 2;
		dest_account.Signer = DestAccount;
		dest_account.Weight = 1;
		weight.Signers.Add(dest_account);

		//TBT: catch the return param
		await _horizon.SetWeightSigner(escrow, weight);
		_sequence = await _horizon.GetSequenceNumber(escrow.PublicKey);

		return escrow;
	}
	public ContractModel CreateContract(ContractParamModel ContractParam)
	{
		ContractModel model = new ContractModel();
		return model;
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
