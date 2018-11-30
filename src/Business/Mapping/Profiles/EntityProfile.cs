using AutoMapper;
using Newtonsoft.Json;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.ReadOnly;
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
                .ForMember(dest => dest.UnitTypeId, opts => opts.MapFrom(src => src.UnitTypeId))
                .ForMember(dest => dest.ItemMetaData, opts => opts.MapFrom(src => src.ItemMetaData))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Listing, ListingViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.OnlineStoreId, opts => opts.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.InventoryItems, opts => opts.MapFrom(src => src.InventoryItems))
                .ForMember(dest => dest.UnitTypeId, opts => opts.MapFrom(src => src.UnitTypeId))
                .ForMember(dest => dest.ItemMetaData, opts => opts.MapFrom(src => src.ItemMetaData))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Listing, ListingDetailViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.OnlineStoreId, opts => opts.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.InventoryItems, opts => opts.MapFrom(src => src.InventoryItems))
                .ForMember(dest => dest.ItemConditionId, opts => opts.MapFrom(src => src.ItemMetaData.ItemConditionId))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<InventoryItem, InventoryItemDetailViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Descriptors, opts => opts.MapFrom(src => src.Descriptors))
                .ForMember(dest => dest.CurrencyAmount, opts => opts.MapFrom(src => src.Price.Amount))
                .ForMember(dest => dest.CurrencyTypeId, opts => opts.MapFrom(src => src.Price.CurrencyTypeId))
                .ForAllOtherMembers(x => x.Ignore());


            CreateMap<InventoryItemViewModel, InventoryItem>()
                .ForMember(dest => dest.ListingId, opts => opts.MapFrom(src => src.ListingId))
                .ForMember(dest => dest.SKU, opts => opts.MapFrom(src => src.SKU))
                .ForMember(dest => dest.UPC, opts => opts.MapFrom(src => src.UPC))
                .ForMember(dest => dest.Descriptors, opts => opts.MapFrom(src => src.Descriptors))
                .ForMember(dest => dest.UnitsAvailable, opts => opts.MapFrom(src => src.UnitsAvailable))
                .ForMember(dest => dest.UnitsSold, opts => opts.MapFrom(src => src.UnitsSold))
                .ForMember(dest => dest.UnitsReturned, opts => opts.MapFrom(src => src.UnitsReturned))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => 
                new CurrencyAmount()
                {
                    Amount = src.CurrencyAmount,
                    CurrencyTypeId = src.CurrencyTypeId
                }))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<InventoryItem, InventoryItemViewModel>()
                .ForMember(dest => dest.ListingId, opts => opts.MapFrom(src => src.ListingId))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.SKU, opts => opts.MapFrom(src => src.SKU))
                .ForMember(dest => dest.UPC, opts => opts.MapFrom(src => src.UPC))
                .ForMember(dest => dest.Descriptors, opts => opts.MapFrom(src => src.Descriptors))
                .ForMember(dest => dest.UnitsAvailable, opts => opts.MapFrom(src => src.UnitsAvailable))
                .ForMember(dest => dest.UnitsSold, opts => opts.MapFrom(src => src.UnitsSold))
                .ForMember(dest => dest.UnitsReturned, opts => opts.MapFrom(src => src.UnitsReturned))
                .ForMember(dest => dest.CurrencyAmount, opts => opts.MapFrom(src => src.Price.Amount))
                .ForMember(dest => dest.CurrencyTypeId, opts => opts.MapFrom(src => src.Price.CurrencyTypeId))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<ItemMetaData, ItemMetaDataViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.ItemConditionId, opts => opts.MapFrom(src => src.ItemConditionId))
                .ForMember(dest => dest.KeyWords, opts => opts.MapFrom(src => JsonConvert.DeserializeObject<string[]>(src.KeyWords)))
                .ForMember(dest => dest.CategoryIds, opts => opts.MapFrom(src => src.Categories.Select(c => c.Id)))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<ItemMetaDataViewModel, ItemMetaData>()
                .ForMember(dest => dest.ItemConditionId, opts => opts.MapFrom(src => src.ItemConditionId))
                .ForMember(dest => dest.KeyWords, opts => opts.MapFrom(src => JsonConvert.SerializeObject(src.KeyWords)))
                .ForMember(dest => dest.Categories, opts => opts.UseValue<ICollection<Category>>(null))
                .ForMember(dest => dest.ItemMetaDataCategories, opts => opts.MapFrom(src => src.CategoryIds.Select(
                    id => new ItemMetaDataCategory() { CategoryId = id})))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Cart, CartViewModel>()
                .ForMember(dest => dest.LineItems, opts => opts.MapFrom(src => src.LineItems))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<LineItem, LineItemViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.ItemConditionId, opts => opts.MapFrom(src => src.InventoryItem.Listing.ItemMetaData.ItemConditionId))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.InventoryItem.Listing.Title))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.InventoryItem.Listing.Description))
                .ForMember(dest => dest.Descriptors, opts => opts.MapFrom(src => src.InventoryItem.Descriptors))
                .ForMember(dest => dest.UnitTypeId, opts => opts.MapFrom(src => src.InventoryItem.Listing.UnitTypeId))
                .ForMember(dest => dest.CurrencyAmount, opts => opts.MapFrom(src => src.InventoryItem.Price.Amount))
                .ForMember(dest => dest.CurrencyTypeId, opts => opts.MapFrom(src => src.InventoryItem.Price.CurrencyTypeId))
                .ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.Quantity))
                .ForAllOtherMembers(x => x.Ignore());

        }
    }
}
