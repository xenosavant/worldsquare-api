using System.Collections.Generic;
using System.Threading.Tasks;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Contract;
using stellar_dotnetcore_sdk;


namespace Stellmart.Services
{
    public class ContractService : IContractService
    {
	private readonly IHorizonService _horizon;

	public ContractService(IHorizonService horizon)
	{
		_horizon = horizon;
	}
	//TBD: Add WorldSquare account as signer
	//TBD: Add async and await
	public HorizonKeyPairModel SetupContract(HorizonKeyPairModel SourceAccount, string DestAccount,
						string Amount)
	{
		HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
		HorizonKeyPairModel escrow = _horizon.CreateAccount();
		HorizonAccountSignerModel dest_account = new HorizonAccountSignerModel();

		_horizon.TransferNativeFund(SourceAccount, DestAccount, Amount);

		weight.Signers = new List<HorizonAccountSignerModel>();
		weight.LowThreshold = 2;
		weight.MediumThreshold = 2;
		weight.HighThreshold = 2;
		dest_account.Signer = DestAccount;
		dest_account.Weight = 1;
		weight.Signers.Add(dest_account);

		//TBT: catch the return param
		_horizon.SetWeightSigner(escrow, weight);
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
