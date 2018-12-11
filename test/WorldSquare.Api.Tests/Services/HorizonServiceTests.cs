using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using NSubstitute;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services;
using Stellmart.Api.Services.Interfaces;
using Xunit;

namespace WorldSquare.Api.Tests.Services
{
    public class HorizonServiceTests
    {
        public HorizonServiceTests()
        {
            _horizonSettingsMock = Options.Create(new HorizonSettings {Server = "testnet"});

            _byteSecretSeed = Convert.FromBase64String(SECRET_SEED);

            _mapperMock = Substitute.For<IMapper>();
            _horizonServerManagerMock = Substitute.For<IHorizonServerManager>();

            _subjectUnderTest = new HorizonService(_horizonSettingsMock, _mapperMock, _horizonServerManagerMock);
        }

        private readonly IHorizonService _subjectUnderTest;
        private readonly IOptions<HorizonSettings> _horizonSettingsMock;
        private readonly IMapper _mapperMock;
        private readonly IHorizonServerManager _horizonServerManagerMock;

        // generated secret seed with RNGCryptoServiceProvider and converted to base64string
        private const string SECRET_SEED = "G3fQ8T/duAPYASsMnV7vwOdcXN2to7eA/FHkT9OEkvA=";
        private readonly byte[] _byteSecretSeed;

        [Fact]
        public void CreateAccount_GetKeyPair()
        {
            var keyPair = KeyPair.FromSecretSeed(_byteSecretSeed);

            var horizonKeyPairModel = new HorizonKeyPairModel {PublicKey = keyPair.AccountId, SecretKey = keyPair.SecretSeed};

            _horizonServerManagerMock.CreateAccount()
                .Returns(horizonKeyPairModel);

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
                                                     new Balance(assetType: "native",
                                                                 assetCode: "XLM",
                                                                 assetIssuer: "xxx",
                                                                 balance: "10000",
                                                                 limit: "xxx",
                                                                 buyingLiabilities: "xxx",
                                                                 sellingLiabilities: "xxx")
                                                 }
                                  };

            _mapperMock.Map<HorizonFundTestAccountModel>(Arg.Any<AccountResponse>())
                .Returns(new HorizonFundTestAccountModel {PublicKey = accountResponse.KeyPair.AccountId});

            _horizonServerManagerMock.FundTestAccountAsync(accountResponse.KeyPair.AccountId)
                .Returns(Task.CompletedTask);
            _horizonServerManagerMock.GetAccountAsync(accountResponse.KeyPair.AccountId)
                .Returns(accountResponse);

            var account = await _subjectUnderTest.FundTestAccountAsync(keyPair.AccountId);

            Assert.Equal(keyPair.AccountId, account.PublicKey);
        }

        [Fact]
        public async Task GetAccountBalance_RequestBalanceWithAssetTypeNull_GetNativeBalance()
        {
            const string expectedBalance = "10000";

            var keyPair = KeyPair.FromSecretSeed(_byteSecretSeed);

            var assetModel = new HorizonAssetModel {AssetType = null, AccountPublicKey = keyPair.AccountId};

            var accountResponse = new AccountResponse(keyPair)
                                  {
                                      KeyPair = keyPair,
                                      Balances = new[]
                                                 {
                                                     new Balance(assetType: "native",
                                                                 assetCode: "XLM",
                                                                 assetIssuer: "xxx",
                                                                 balance: "10000",
                                                                 limit: "xxx",
                                                                 buyingLiabilities: "xxx",
                                                                 sellingLiabilities: "xxx")
                                                 }
                                  };

            _horizonServerManagerMock.GetAccountAsync(accountResponse.KeyPair.AccountId)
                .Returns(accountResponse);

            var result = await _subjectUnderTest.GetAccountBalance(assetModel);

            Assert.Equal(expectedBalance, result);
        }

        [Fact]
        public async Task GetAccountBalance_RequestBalanceWithAssetTypeWST_GetWSTBalance()
        {
            const string expectedBalance = "1234";
            const string assetIssuerPublicKey = "GAEHO5MAKDWY3AHG6HCFIMTQT4XXTPFX6MWESLROASNHRPSW4NUR2F7Y";
            var keyPair = KeyPair.FromSecretSeed(_byteSecretSeed);

            var assetModel = new HorizonAssetModel {AssetCode = "WST", AssetIssuerPublicKey = assetIssuerPublicKey};

            var accountResponse = new AccountResponse(keyPair)
                                  {
                                      KeyPair = keyPair,
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

            _horizonServerManagerMock.GetAccountAsync(Arg.Any<string>())
                .Returns(accountResponse);

            var result = await _subjectUnderTest.GetAccountBalance(assetModel);

            Assert.Equal(expectedBalance, result);
        }

        [Fact]
        public async Task GetSequenceNumber_ProvidePublicKey_GetsSequenceNumber()
        {
            var keyPair = KeyPair.FromSecretSeed(_byteSecretSeed);

            var accountResponse = new AccountResponse(keyPair) {SequenceNumber = 42};

            _horizonServerManagerMock.GetAccountAsync(accountResponse.KeyPair.AccountId)
                .Returns(accountResponse);

            var result = await _subjectUnderTest.GetSequenceNumber(keyPair.AccountId);

            Assert.Equal(accountResponse.SequenceNumber, result);
        }
    }
}