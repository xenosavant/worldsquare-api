using AutoMapper;
using Stellmart.Api.Data.Kyc;
using Yoti.Auth;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class KycProfile : Profile
    {
        public KycProfile()
        {
            CreateMap<YotiProfile, KycProfileModel>()
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.GivenNames.GetValue()))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.FamilyName.GetValue()))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
