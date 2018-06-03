﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class SuperCategory
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }
    }
}
