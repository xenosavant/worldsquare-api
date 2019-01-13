using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contracts;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface ISignatureDataManager
    {
        Task<UserSignature> GetUserSignatureAsync(GetSignatureModel data);
    }
}
