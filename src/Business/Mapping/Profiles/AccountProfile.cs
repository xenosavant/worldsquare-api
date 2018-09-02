using AutoMapper;
using Stellmart.Api.Data.Account;
using Stellmart.Data.Account;
using Stellmart.Data.ViewModels;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<SignupRequest, ApplicationUserModel>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opts => opts.MapFrom(src => src.Password))
                .ForMember(dest => dest.Answers, opts => opts.MapFrom(src => src.SecurityAnswers))
                .ForMember(dest => dest.Questions, opts => opts.MapFrom(src => src.SecurityQuestions))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
