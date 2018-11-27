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
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WorldSquare.Api.Tests.Services
{
    public class HorizonServiceTests
    {
        private IHorizonService _subjectUnderTest;
        private readonly IOptions<HorizonSettings> _horizonSettingsMock;
        private readonly IMapper _mapperMock;
        private readonly IHorizonServerManager _horizonServerManagerMock;

        private const string _horizonServer = "https://horizon-testnet.stellar.org/";

        // generated secret seed with RNGCryptoServiceProvider and converted to base64string
        private const string _secretSeed = "G3fQ8T/duAPYASsMnV7vwOdcXN2to7eA/FHkT9OEkvA=";
        private byte[] _byteSecretSeed;

        public HorizonServiceTests()
        {
            _horizonSettingsMock = Options.Create(new HorizonSettings()
            {
                Server = "testnet"
            });

            _byteSecretSeed = Convert.FromBase64String(_secretSeed);

            _mapperMock = Substitute.For<IMapper>();
            _horizonServerManagerMock = Substitute.For<IHorizonServerManager>();

            _subjectUnderTest = new HorizonService(_horizonSettingsMock, _mapperMock, _horizonServerManagerMock);
        }

        [Fact]
        public void CreateAccount_GetKeyPair()
        {
            var keyPair = KeyPair.FromSecretSeed(_byteSecretSeed);

            var horizonKeyPairModel = new HorizonKeyPairModel
            {
                PublicKey = keyPair.AccountId,
                SecretKey = keyPair.SecretSeed
            };
            
            _horizonServerManagerMock.CreateAccount().Returns(horizonKeyPairModel);

            var result = _subjectUnderTest.CreateAccount();

            Assert.NotNull(result);
            Assert.Equal(keyPair.AccountId, result.PublicKey);
            Assert.Equal(keyPair.SecretSeed, result.SecretKey);
        }

        [Fact]
        public async Task FundTestAccountAsync_ProvidePublicKey_GetFundedAccount()
        {
            var keyPair = KeyPair.FromSecretSeed(_byteSecretSeed);

            var accountResponse = new AccountResponse(keyPair)
            {
                KeyPair = keyPair,
                Balances = new[]
                {
                    new Balance("XLM", "XLM", "xxx", "10000", "xxx", "xxx", "xxx")
                }
            };

            _mapperMock.Map<HorizonFundTestAccountModel>(Arg.Any<AccountResponse>()).Returns(new HorizonFundTestAccountModel()
            {
                PublicKey = accountResponse.KeyPair.AccountId
            });

            _horizonServerManagerMock.FundTestAccountAsync(accountResponse.KeyPair.AccountId).Returns(Task.CompletedTask);
            _horizonServerManagerMock.GetAccountAsync(accountResponse.KeyPair.AccountId).Returns(accountResponse);

            var account = await _subjectUnderTest.FundTestAccountAsync(keyPair.AccountId);

            Assert.Equal(keyPair.AccountId, account.PublicKey);
        }
    }
}
