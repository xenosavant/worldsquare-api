using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Stellmart.Context.Entities;
using Stellmart.Context;

namespace Stellmart.DataAccess
{
    public class UserDataAccess : GenericDataAccess<User>, IUserDataAccess
    {
        public UserDataAccess(StellmartContext context) : base(context)
        {}
    }

    public interface IUserDataAccess : IGenericDataAccess<User>
    {
    }

}
