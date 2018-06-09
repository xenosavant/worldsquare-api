using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldSquare.Api.Data.ViewModels
{
    public class SecurityAnswerViewModel
    {
        public int Order { get; set; }

        public string Answer { get; set; }

        public byte[] IV { get; set; }
    }
}
