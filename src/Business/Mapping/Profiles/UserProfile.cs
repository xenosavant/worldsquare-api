using AutoMapper;
using Stellmart.Api.Context;
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
        }
    }
}
