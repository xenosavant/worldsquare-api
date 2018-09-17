using NUnit.Framework;
using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.Services;
using Stellmart.Api.Services.Interfaces;
using System.Collections.Generic;

namespace Stellmart.Api.Tests
{
    [TestFixture]
    public class EncryptionServiceTests
    {
        private IEncryptionService _encryptionService;

        private string _key => "SAV76USXIJOBMEQXPANUOQM6F5LIOTLPDIDVRJBFFE2MDJXG24TAPUU7";

        [SetUp]
        public void Init()
        {
            _encryptionService = new EncryptionService();
        }

        [Test]
        public void TestEncryptDecryptRecoveryKey()
        {
            byte[] iv = _encryptionService.GenerateIv();

            var answers =
            new List<string>()
            {
                "lollipop",
                "popsicle",
                "america",
                "purple",
                "witchitah"
            };
            var encrypted = _encryptionService.EncryptRecoveryKey(_key, answers, iv);
            var decrypted = _encryptionService.DecryptRecoveryKey(encrypted, answers, iv);
            Assert.AreEqual(_key, decrypted);
        }

        [Test]
        public void TestEncryptDecryptSecretKey()
        {
            byte[] iv = _encryptionService.GenerateIv();
            var password = "ksdjf*KAJSDF123ksdjf*KAJSDF123";
            var encrypted = _encryptionService.EncryptSecretKey(_key, iv, password);
            var decrypted = _encryptionService.DecryptSecretKey(encrypted, iv, password);
            Assert.AreEqual(_key, decrypted);
        }

    }
}
