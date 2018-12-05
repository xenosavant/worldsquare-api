using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
        public string UserName { get; set; }
    }
}
