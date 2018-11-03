using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
    public class ContractSignatureModel
    {
        SignatureType SignatureType;
        HorizonKeyPairModel Source;
        String secret;
    }
}
