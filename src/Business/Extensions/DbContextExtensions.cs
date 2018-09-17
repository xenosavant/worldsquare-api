using Microsoft.EntityFrameworkCore;
using Stellmart.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Extensions
{
    public static class DbContextExtensions
    {
        public static IEnumerable<Type> TypesOf<T>(this ApplicationDbContext dbContext) where T : class
        {
            return dbContext.GetType().Assembly.GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface);
        }
    }
}
