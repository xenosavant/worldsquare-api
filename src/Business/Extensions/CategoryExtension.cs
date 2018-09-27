using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Extensions
{
    public static class CategoryExtension
    {
        public static IList<Category> SortCategories(this IList<Category> categories)
        {
            // selection sort
            var i = categories.Count - 1;
            var j = i;
            while (i > 0)
            {
                while (j > 0 && categories[j] != null && categories[j]?.ParentCategoryId != categories[j-1].Id)
                {
                    j--;
                }
                if (i != j)
                {
                    var temp = categories[i];
                    categories[i] = categories[j];
                    categories[j] = temp;
                }
                i--;
            }
            return categories;
        }
    }
}
