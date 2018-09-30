using System;
using AutoMapper;
using Stellmart.Api.Context;
using Stellmart.Api.Data.Account;
using Stellmart.Api.Data.Enums;
using Stellmart.Data.ViewModels;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUserViewModel, ApplicationUser>()
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.UserName))
                .ForMember(dest => dest.VerificationLevelId, opts => opts.MapFrom(src => src.VerificationLevelId))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.UserName))
                .ForMember(dest => dest.VerificationLevelId, opts => opts.MapFrom(src => src.VerificationLevelId))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<ApplicationUserModel, ApplicationUser>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.StellarPublicKey, opts => opts.MapFrom(src => src.StellarPublicKey))
                .ForMember(dest => dest.StellarEncryptedSecretKey, opts => opts.MapFrom(src => src.StellarEncryptedSecretKey))
                .ForMember(dest => dest.StellarRecoveryKey, opts => opts.MapFrom(src => src.StellarRecoveryKey))
                .ForMember(dest => dest.StellarSecretKeyIv, opts => opts.MapFrom(src => src.StellarSecretKeyIv))
                .ForMember(dest => dest.PrimaryShippingLocationId, opts => opts.UseValue((byte)PrimaryShippingLocationTypes.Default))
                .ForMember(dest => dest.RewardsLevelId, opts => opts.UseValue((byte)RewardsLevelTypes.Default))
                .ForMember(dest => dest.TwoFactorTypeId, opts => opts.UseValue((byte)TwoFactorTypes.None))
                .ForMember(dest => dest.NativeCurrencyId, opts => opts.UseValue((byte)NativeCurrencyTypes.Default))
                .ForMember(dest => dest.VerificationLevelId, opts => opts.UseValue((byte)VerificationLevelTypes.NonVerified))
                .ForMember(dest => dest.UniqueId, opts => opts.UseValue(Guid.NewGuid()))
                .ForMember(dest => dest.SecurityQuestions, opts => opts.MapFrom(src => src.SecurityQuestions))
                .ForMember(dest => dest.IpAddress, opts => opts.MapFrom(src => src.IpAddress))
                .ForMember(dest => dest.CountryId, opts => opts.MapFrom(src => src.CountryId))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
