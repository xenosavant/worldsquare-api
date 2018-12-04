using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Business.Managers;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.Horizon;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WorldSquare.Api.Tests.Services
{
    public class HorizonServiceManagerIntegrationTests
    {
        private readonly IHorizonServerManager _subjectUnderTest;

        private const string HorizonServer = "https://horizon-testnet.stellar.org/";

        // generated secret seed with RNGCryptoServiceProvider and converted to base64string
        private const string SecretSeed = "G3fQ8T/duAPYASsMnV7vwOdcXN2to7eA/FHkT9OEkvA=";
        private readonly byte[] _byteSecretSeed;

        private void BuildService(IServiceCollection serviceCollection)
        {
            // required for reseting automapper instance for each test
            ServiceCollectionExtensions.UseStaticRegistration = false;

            serviceCollection.AddAutoMapper();
        }

        public HorizonServiceManagerIntegrationTests()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            BuildService(serviceCollection);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            var mapperInstance = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _byteSecretSeed = Convert.FromBase64String(SecretSeed);

            var _server = new Server(HorizonServer,
                new HttpClient
                {
                    BaseAddress = new Uri(HorizonServer)
                });

            _subjectUnderTest = new HorizonServerManager(_server, mapperInstance);
        }

        [Fact]
        public void CreateAccount_GetKeyPair()
        {
            const string publicKeyRegex = "G([A-Z0-9]){54}[A-Z0-9]";
            const string secretKeyRegex = "S([A-Z0-9]){54}[A-Z0-9]";
            var result = _subjectUnderTest.CreateAccount();

            Assert.NotNull(result);
            Assert.IsType<HorizonKeyPairModel>(result);
            Assert.Matches(publicKeyRegex, result.PublicKey);
            Assert.Matches(secretKeyRegex, result.SecretKey);
        }

        [Fact]
        public async Task FundTestAccountAsync_ProvidePublicKey_GetFundedAccount()
        {
            var keyPair = KeyPair.FromSecretSeed(_byteSecretSeed);
            const string expectedNativeBalance = "10000.0000000";
            const string expectedAssetType = "native";

            await _subjectUnderTest.FundTestAccountAsync(keyPair.AccountId);
            var result = await _subjectUnderTest.GetAccountAsync(keyPair.AccountId);

            Assert.NotNull(result);
            Assert.IsType<AccountResponse>(result);
            Assert.Equal(keyPair.AccountId, result.KeyPair.AccountId);
            Assert.Equal(expectedNativeBalance, result.Balances.FirstOrDefault(x => x.AssetType == "native")?.BalanceString);
            Assert.Equal(expectedAssetType, result.Balances.FirstOrDefault(x => x.AssetType == "native")?.AssetType);
        }

        //[Fact]
        //public async Task GetSequenceNumber_ProvidePublicKey_GetsSequenceNumber()
        //{
        //    var keyPair = KeyPair.FromSecretSeed(_byteSecretSeed);

        //    var accountResponse = new AccountResponse(keyPair)
        //    {
        //        SequenceNumber = 42
        //    };

        //    _horizonServerManagerMock.GetAccountAsync(accountResponse.KeyPair.AccountId).Returns(accountResponse);

        //    var result = await _subjectUnderTest.GetSequenceNumber(keyPair.AccountId);

        //    Assert.Equal(accountResponse.SequenceNumber, result);
        //}
    }
}
