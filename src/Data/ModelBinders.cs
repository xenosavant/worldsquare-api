using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ModelBinders
{
    public class CommaDelimitedArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var val = bindingContext.ValueProvider.GetValue(key);
            if (val != null)
            {
                var s = val.FirstValue;
                if (s != null)
                {
                    var elementType = bindingContext.ModelType.GetElementType();
                    var converter = TypeDescriptor.GetConverter(elementType);
                    var values = Array.ConvertAll(s.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries),
                        x => { return converter.ConvertFromString(x != null ? x.Trim() : x); });

                    var typedValues = Array.CreateInstance(elementType, values.Length);

                    values.CopyTo(typedValues, 0);

                    bindingContext.Result = ModelBindingResult.Success(typedValues);
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(Array.CreateInstance(bindingContext.ModelType.GetElementType(), 0));
                }
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
