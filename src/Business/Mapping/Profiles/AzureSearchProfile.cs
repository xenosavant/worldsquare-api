using AutoMapper;
using Bounce.Api.Data.Indexes;
using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class AzureSearchProfile : Profile
    {
        public AzureSearchProfile()

        {
            CreateMap<ItemMetaDataViewModel, ItemMetaDataSearchIndex>();
        }
    }
}
