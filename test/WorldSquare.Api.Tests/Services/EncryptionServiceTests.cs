using System;
using System.Collections.Generic;
using Stellmart.Api.Data.ViewModels;
using NUnit.Framework;
using Stellmart.Api.Services;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Tests
{
    [TestFixture]
    public class EncryptionServiceTests
    {
        private IEncryptionService _encryptionService;

        private byte[] _iv => new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                                           0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

        private string _key => "SAV76USXIJOBMEQXPANUOQM6F5LIOTLPDIDVRJBFFE2MDJXG24TAPUU7";

        [SetUp]
        public void Init()
        {
            _encryptionService = new EncryptionService();
        }

        [Test]
        public void TestEncryptDecryptRecoveryKey()
        {
            var answers =
            new List<SecurityAnswerViewModel>()
            {
                new SecurityAnswerViewModel()
                {
                    Answer = "lollipop",
                    IV = _iv,
                    Order = 1
                },
                new SecurityAnswerViewModel()
                {
                    Answer = "popsicle",
                    IV = _iv,
                    Order = 2
                },
                new SecurityAnswerViewModel()
                {
                    Answer = "america",
                    IV = _iv,
                    Order = 3
                },
                new SecurityAnswerViewModel()
                {
                    Answer = "purple",
                    IV = _iv,
                    Order = 4
                },
                new SecurityAnswerViewModel()
                {
                    Answer = "witchitah",
                    IV = _iv,
                    Order = 5
                }
            };
            var encrypted = _encryptionService.EncryptRecoveryKey(_key, answers);
            var decrypted = _encryptionService.DecryptRecoveryKey(encrypted, answers);
            Assert.AreEqual(_key, decrypted);
        }

        [Test]
        public void TestEncryptDecryptSecretKey()
        {
            var password = "ksdjf*KAJSDF123";
            var encrypted = _encryptionService.EncryptSecretKey(_key, _iv, password);
            var decrypted = _encryptionService.DecryptSecretKey(encrypted, _iv, password);
            Assert.AreEqual(_key, decrypted);
        }

    }
}
