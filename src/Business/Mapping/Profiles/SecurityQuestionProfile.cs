using AutoMapper;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.Data.Account;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class SecurityQuestionProfile : Profile
    {
        public SecurityQuestionProfile()
        {
            CreateMap<SecurityQuestion, SecurityQuestionsResponse>()
                .ForMember(dest => dest.Question, opts => opts.MapFrom(src => src.Description))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<string, SecurityQuestionsResponse>()
                .ForMember(dest => dest.Question, opts => opts.MapFrom(src => src))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
