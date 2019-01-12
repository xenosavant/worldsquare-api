using Bounce.Api.Data.Search.Indexes;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.Interfaces;
using Stellmart.Api.Context.Entities.ReadOnly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Stellmart.Api.Context.Entities
{
    public class Listing : EntityMaximum, ISearchable
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Flagged { get; set; }

        public bool Internal { get; set; }

        public int UnitTypeId { get; set; }

        public int ItemMetaDataId { get; set; }

        public int CurrencyId { get; set; }

        public virtual QuantityUnit UnitType { get; set; }

        public virtual OnlineStore OnlineStore { get; set; }

        public virtual ItemMetaData ItemMetaData { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }

        public virtual ICollection<MessageThreadMember> ListingMessageThreads { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public IList<Field> GetFields()
        {
            return FieldBuilder.BuildForType<ItemMetaDataSearchIndex>();
        }

        public int? GetMinimumPrice(string currencyName)
        {
            if (Currency.Description == currencyName)
            {
                return InventoryItems?.Select(i => i.Price).Min();
            }
            return null;
        }

        public int? GetMaximumPrice(string currencyName)
        {
            if (Currency.Description == currencyName)
            {
                return InventoryItems?.Select(i => i.Price).Max();
            }
            return null;
        }
    }
}
