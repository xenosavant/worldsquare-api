using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.BaseEntity
{
    public interface IUniqueEntity
    {
        Guid UniqueId { get; set; }
    }
}
