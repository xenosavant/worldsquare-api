using AutoMapper;
using Stellmart.Api.Business.Managers;
using Stellmart.Api.Data.Kyc;
using System.Threading.Tasks;
using Yoti.Auth;

namespace Stellmart.Api.Services
{
    public class YotiService : IKycService
    {
        private readonly YotiClient _yotiClient;
        private readonly IMapper _mapper;
        private readonly IKycDataManager _kycDataManager;

        public YotiService(YotiClient yotiClient, IMapper mapper, IKycDataManager kycDataManager)
        {
            _yotiClient = yotiClient;
            _mapper = mapper;
            _kycDataManager = kycDataManager;
        }

        public async Task<bool> VerifyAsync(KycRequest request)
        {
            var profile = await _yotiClient.GetActivityDetailsAsync(request.Token);
            
            if(profile.Outcome == ActivityOutcome.Success)
            {
                var result = _mapper.Map<KycProfileModel>(profile.Profile);
                // to do - user id should come from claims service - hardcoded 1
                await _kycDataManager.CreateAsync(result, 1);
                return true;
            }

            return false;
        }
    }
}
