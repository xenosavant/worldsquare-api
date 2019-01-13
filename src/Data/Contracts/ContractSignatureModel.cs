using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Context.Entities;

namespace Stellmart.Api.Data.Contracts
{
    public class ContractSignatureModel
    {
        public Signature Signature;
        public String Secret;
    }
}
