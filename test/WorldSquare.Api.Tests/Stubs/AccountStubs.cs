using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;

namespace WorldSquare.Api.Tests.Stubs
{
    public static class AccountStubs
    {
        public static AccountResponse GetAccount(KeyPair keypair, string assetIssuerPublicKey)
        {
            return new AccountResponse(keypair)
                   {
                       KeyPair = keypair,
                       Balances = new[]
                                  {
                                      new Balance(assetType: "native",
                                                  assetCode: "XLM",
                                                  assetIssuer: "xxx",
                                                  balance: "10000",
                                                  limit: "xxx",
                                                  buyingLiabilities: "xxx",
                                                  sellingLiabilities: "xxx"),
                                      new Balance(assetType: "alpha",
                                                  assetCode: "WST",
                                                  assetIssuer: assetIssuerPublicKey,
                                                  balance: "1234",
                                                  limit: "xxx",
                                                  buyingLiabilities: "xxx",
                                                  sellingLiabilities: "xxx")
                                  }
                   };
        }
    }
}
