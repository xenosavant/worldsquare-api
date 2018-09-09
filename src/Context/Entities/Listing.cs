﻿using Newtonsoft.Json;
using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Listing : EntityMaximum
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Flagged { get; set; }

        public int? ThreadId { get; set; }

        public bool Internal { get; set; }

        public int ItemMetaDataId { get; set; }

        public virtual OnlineStore OnlineStore { get; set; }

        public virtual MessageThread Thread { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }

        public virtual ItemMetaData ItemMetaData { get; set; }

    }
}
