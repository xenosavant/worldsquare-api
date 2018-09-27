﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.BaseEntity
{
    public interface IMutableEntity
    {
        bool IsDeleted { get; set; }
    }
}
