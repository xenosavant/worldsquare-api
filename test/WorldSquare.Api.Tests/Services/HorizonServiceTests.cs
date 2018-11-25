using AutoMapper;
using Microsoft.Extensions.Options;
using NSubstitute;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using Stellmart.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace WorldSquare.Api.Tests.Services
{
    public class HorizonServiceTests
    {
        private IHorizonService _subjectUnderTest;
        private readonly IOptions<HorizonSettings> _horizonSettingsMock;
        private readonly IMapper _mapperMock;
        private readonly IHorizonServerManager _horizonServerManager;

        private const string horizonServer = "https://horizon-testnet.stellar.org/";

        public HorizonServiceTests()
        {
            _horizonSettingsMock = Options.Create(new HorizonSettings()
            {
                Server = "testnet"
            });

            _mapperMock = Substitute.For<IMapper>();
            _horizonServerManager = Substitute.For<IHorizonServerManager>();

            _subjectUnderTest = new HorizonService(_horizonSettingsMock, _mapperMock, _horizonServerManager);
        }

        [Fact]
        public async Task FundTestAccountAsync_ProvidePublicKey_GetKeyPair()
        {
            // generated secret seed with RNGCryptoServiceProvider and converted to base64string
            const string secretSeed = "G3fQ8T/duAPYASsMnV7vwOdcXN2to7eA/FHkT9OEkvA=";
            var byteSecretSeed = Convert.FromBase64String(secretSeed);

            var keyPair = KeyPair.FromSecretSeed(byteSecretSeed);

            var accountResponse = new AccountResponse(keyPair)
            {
                KeyPair = keyPair
            };

            _mapperMock.Map<HorizonFundTestAccountModel>(Arg.Any<AccountResponse>()).Returns(new HorizonFundTestAccountModel()
            {
                PublicKey = accountResponse.KeyPair.AccountId
            });

            _horizonServerManager.FundTestAccountAsync(accountResponse.KeyPair.AccountId).Returns(Task.CompletedTask);
            _horizonServerManager.GetAccountAsync(accountResponse.KeyPair.AccountId).Returns(accountResponse);

            var account = await _subjectUnderTest.FundTestAccountAsync(keyPair.AccountId);

            Assert.Equal(keyPair.AccountId, account.PublicKey);
        }
    }
}
