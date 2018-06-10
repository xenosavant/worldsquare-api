using System;
using System.Collections.Generic;
using Stellmart.Api.Data.ViewModels;
using NUnit.Framework;
using Stellmart.Api.Services;

namespace Stellmart.Api.Tests
{
    [TestFixture]
    public class EncryptionServiceTests
    {
        private IEncryptionService _encryptionService;

        private byte[] IV => new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                                           0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };


        [SetUp]
        public void Init()
        {
            _encryptionService = new EncryptionService();
        }

        [Test]
        public void TestEncryptDecryptRecoveryKey()
        {
            var key = "SAV76USXIJOBMEQXPANUOQM6F5LIOTLPDIDVRJBFFE2MDJXG24TAPUU7";
            var answers =
            new List<SecurityAnswerViewModel>()
            {
                new SecurityAnswerViewModel()
                {
                    Answer = "lollipop",
                    IV = IV,
                    Order = 1
                },
                new SecurityAnswerViewModel()
                {
                    Answer = "popsicle",
                    IV = IV,
                    Order = 2
                },
                new SecurityAnswerViewModel()
                {
                    Answer = "america",
                    IV = IV,
                    Order = 3
                },
                new SecurityAnswerViewModel()
                {
                    Answer = "purple",
                    IV = IV,
                    Order = 4
                },
                new SecurityAnswerViewModel()
                {
                    Answer = "witchitah",
                    IV = IV,
                    Order = 5
                }
            };
            var encrypted = _encryptionService.EncryptRecoveryKey(key, answers);
            var decrypted = _encryptionService.DecryptRecoveryKey(key, answers);
            Assert.AreEqual(decrypted, key);
        }

    }
}
