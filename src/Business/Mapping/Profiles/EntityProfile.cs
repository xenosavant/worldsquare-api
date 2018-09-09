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

            CreateMap<ListingViewModel, Listing>()
                .ForMember(dest => dest.ServiceId, opts => opts.MapFrom(src => src.OnlineStoreId))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.InventoryItems, opts => opts.MapFrom(src => src.InventoryItems))
                .ForMember(dest => dest.ItemMetaData, opts => opts.MapFrom(src => src.ItemMetaData))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<InventoryItemViewModel, InventoryItem>()
                .ForMember(dest => dest.UniqueId, opts => opts.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.SKU, opts => opts.MapFrom(src => src.SKU))
                .ForMember(dest => dest.UPC, opts => opts.MapFrom(src => src.UPC))
                .ForMember(dest => dest.Descriptors, opts => opts.MapFrom(src => src.Descriptors))
                .ForMember(dest => dest.UnitsAvailable, opts => opts.MapFrom(src => src.UnitsAvailable))
                .ForMember(dest => dest.UnitsSold, opts => opts.MapFrom(src => src.UnitsSold))
                .ForMember(dest => dest.UnitsReturned, opts => opts.MapFrom(src => src.UnitsReturned))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(dest => dest.UnitTypeId, opts => opts.MapFrom(src => src.UnitType.Id))
                .ForAllOtherMembers(x => x.Ignore());

        }
    }
}
