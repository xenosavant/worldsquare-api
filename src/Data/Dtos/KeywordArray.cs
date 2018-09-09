using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Data.ModelBinders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Dtos
{
    [ModelBinder(BinderType = typeof(CommaDelimitedArrayModelBinder))]
    public class KeywordArray : IEnumerable<string>
    {
        List<string> list = new List<string>();

        public string this[int index]
        {
            get { return list[index]; }
            set { list.Insert(index, value); }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
