using System.ComponentModel.DataAnnotations;

namespace Stellmart.Api.Context.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhonePrefix { get; set; }
        public int OrderNo { get; set; }
    }
}
