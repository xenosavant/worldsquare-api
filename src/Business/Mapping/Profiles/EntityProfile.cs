using AutoMapper;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<OnlineStoreViewModel, OnlineStore>()
                .ForMember(dest => dest.TagLine, opts => opts.MapFrom(src => src.TagLine))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.NativeCurrencyId, opts => opts.MapFrom(src => src.NativeCurrency.Id))
                .ForMember(dest => dest.Verified, opts => opts.UseValue<bool>(false))
                .ForAllOtherMembers(x => x.Ignore());

        }
    }
}
