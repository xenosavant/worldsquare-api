using AutoMapper;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Kyc;
using System;
using Yoti.Auth;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class KycProfile : Profile
    {
        public KycProfile()
        {
            CreateMap<YotiProfile, KycProfileModel>()
                .ForMember(dest => dest.UserIdentifier, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.GivenNames.GetValue()))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.FamilyName.GetValue()))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.EmailAddress.GetValue()))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender.GetValue()))
                .ForMember(dest => dest.MobileNumber, opts => opts.MapFrom(src => src.MobileNumber.GetValue()))
                .ForMember(dest => dest.Nationality, opts => opts.MapFrom(src => src.Nationality.GetValue()))
                .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth.GetValue()))
                .ForMember(dest => dest.UserId, opts => opts.UseValue(1)) //to-do: get user id from claim - custom resolver
                .AfterMap<AddressAfterMapAction>()
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<KycProfileModel, KycData>()
                .ForMember(dest => dest.UserIdentifier, opts => opts.MapFrom(src => src.UserIdentifier))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
                .ForMember(dest => dest.MobileNumber, opts => opts.MapFrom(src => src.MobileNumber))
                .ForMember(dest => dest.Nationality, opts => opts.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.AddressLine1, opts => opts.MapFrom(src => src.AddressLine1))
                .ForMember(dest => dest.AddressLine2, opts => opts.MapFrom(src => src.AddressLine2))
                .ForMember(dest => dest.AddressLine3, opts => opts.MapFrom(src => src.AddressLine3))
                .ForMember(dest => dest.AddressLine4, opts => opts.MapFrom(src => src.AddressLine4))
                .ForMember(dest => dest.AddressLine5, opts => opts.MapFrom(src => src.AddressLine5))
                .ForMember(dest => dest.AddressLine6, opts => opts.MapFrom(src => src.AddressLine6))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }

    public class AddressAfterMapAction : IMappingAction<YotiProfile, KycProfileModel>
    {
        public void Process(YotiProfile source, KycProfileModel destination)
        {
            string[] addressArray = source.Address.GetValue().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            destination.AddressLine1 = addressArray[0] ?? string.Empty;
            destination.AddressLine2 = addressArray[1] ?? string.Empty;
            destination.AddressLine3 = addressArray.Length >= 3 ? addressArray[2] ?? string.Empty : null;
            destination.AddressLine4 = addressArray.Length >= 4 ? addressArray[3] ?? string.Empty : null;
            destination.AddressLine5 = addressArray.Length >= 5 ? addressArray[4] ?? string.Empty : null;
            destination.AddressLine6 = addressArray.Length >= 6 ? addressArray[5] ?? string.Empty : null;
        }
    }
}
