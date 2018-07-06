using AutoMapper;
using Stellmart.Api.Context.Entities;
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
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.EmailAddress.GetValue()))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender.GetValue()))
                .ForMember(dest => dest.MobileNumber, opts => opts.MapFrom(src => src.MobileNumber.GetValue()))
                .ForMember(dest => dest.Nationality, opts => opts.MapFrom(src => src.Nationality.GetValue()))
                .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth.GetValue()))
                .ForMember(dest => dest.UserId, opts => opts.UseValue(1)) //to-do: get user id from claim - custom resolver
                .ForMember(dest => dest.CountryId, opts => opts.UseValue(1)) // to-do get user id from countries table
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<KycProfileModel, KycData>()
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
                .ForMember(dest => dest.MobileNumber, opts => opts.MapFrom(src => src.MobileNumber))
                .ForMember(dest => dest.Nationality, opts => opts.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CountryId, opts => opts.MapFrom(src => src.CountryId))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
