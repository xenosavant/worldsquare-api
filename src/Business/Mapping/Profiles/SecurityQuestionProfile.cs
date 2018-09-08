using AutoMapper;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.Data.Account;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class SecurityQuestionProfile : Profile
    {
        public SecurityQuestionProfile()
        {
            CreateMap<SecurityQuestion, SecurityQuestionModel>()
                .ForMember(dest => dest.Question, opts => opts.MapFrom(src => src.Description))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
