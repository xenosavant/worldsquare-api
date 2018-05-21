using AutoMapper;
using stellar_dotnetcore_sdk;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class HorizonProfile : Profile
    {
        public HorizonProfile()
        {
            CreateMap<KeyPair, HorizonKeyPairModel>()
                .ForMember(dest => dest.PublicKey, opts => opts.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.EncodedSecret, opts => opts.MapFrom(src => src.SecretSeed))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
