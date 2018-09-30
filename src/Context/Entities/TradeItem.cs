using Bounce.Api.Data.Search.Indexes;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.Interfaces;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context
{
    public class TradeItem : UniqueEntity, IItem, ISearchable
    {
        public string Title { get; set; }

        public int OwnerId { get; set; }

        public int? TradeInValueId { get; set; }

        public int? TradeInStateId { get; set; }

        public int ItemMetaDataId { get; set; }

        public int ValueId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual CurrencyAmount TradeInValue { get; set; }

        public virtual TradeInState TradeInState { get; set; }

        public virtual ItemMetaData ItemMetaData { get; set; }

        public virtual LineItem LineItem { get; set; }

        public IList<Field> GetFields()
        {
            return FieldBuilder.BuildForType<ItemMetaDataSearchIndex>();
        }
    }
}
