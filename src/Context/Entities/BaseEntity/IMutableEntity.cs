using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.BaseEntity
{
    public interface IMutableEntity
    {
        Guid UniqueId { get; set; }
        bool IsDeleted { get; set; }
    }
}
