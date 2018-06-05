using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public interface IReadOnlyData
    {
        int Id { get; set; }
        string Description { get; set; }
        int DisplayOrder { get; set; }
        bool Active { get; set; }
    }

    public class LookupData : IReadOnlyData
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool Active { get; set; }
    }

}
