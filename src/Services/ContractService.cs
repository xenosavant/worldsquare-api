using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;
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
	public HorizonKeyPairModel SetupContract(HorizonKeyPairModel SourceAccount, string DestAccount,
						string Amount)
	{
		HorizonKeyPairModel kp =  new HorizonKeyPairModel();
		return kp;
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
