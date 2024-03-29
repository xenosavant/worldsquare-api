﻿using Stellmart.Api.Data.Kyc;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public interface IKycService
    {
        Task<bool> VerifyAsync(KycRequest request, int userId);
    }
}
