using AutoMapper;
using Stellmart.Api.Data.Kyc;
using System;
using System.Threading.Tasks;
using Yoti.Auth;

namespace Stellmart.Api.Services
{
    public class YotiService : IKycService
    {
        private readonly YotiClient _yotiClient;
        private readonly IMapper _mapper;

        public YotiService(YotiClient yotiClient, IMapper mapper)
        {
            _yotiClient = yotiClient;
            _mapper = mapper;
        }

        public async Task<KycProfileModel> GetUserProfileAsync(string token)
        {
            var profile = await _yotiClient.GetActivityDetailsAsync(token);
            return _mapper.Map<KycProfileModel>(profile.Profile);
        }
    }
}
