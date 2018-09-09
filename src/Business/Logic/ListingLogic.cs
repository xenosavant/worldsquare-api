﻿using AutoMapper;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class ListingLogic : IListingLogic
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public static string NavigationProperties => "ListingInventoryItems.InventoryItems.Price," +
            "ListingInventoryItems.InventoryItems.UnitType,Thread";

        public ListingLogic(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public Task<IEnumerable<Listing>> GetAsync(string[] keywords, int? onlineStroreId,  int? categoryId, 
            int? subcategoryId, int? listingCategoryId, 
            int? conditionId, string searchString)
        {
            // integrate with elastic search
            throw new NotImplementedException();
        } 

        public async Task<Listing> GetByIdAsync(int id)
        {
            return await _repository.GetOneAsync<Listing>(s => s.Id == id, NavigationProperties);
        }

        public async Task<Listing> CreateAsync(int userId, ListingViewModel listingViewModel)
        {
            var listing = _mapper.Map<Listing>(listingViewModel);
            _repository.Create(listing);
            await _repository.SaveAsync();
            return await _repository.GetOneAsync<Listing>(l => l.Id == listing.Id, NavigationProperties);
        }

        public async Task<Listing> UpdateAsync(Listing listing, Delta<Listing> delta)
        {
            throw new NotImplementedException();
        }

        public async Task<Listing> UpdateAsync(Listing listing)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Listing store)
        {
            throw new NotImplementedException();
        }
    }
}
