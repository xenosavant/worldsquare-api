using Stellmart.Api.Services;
using Stellmart.Api.Services.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace WorldSquare.Api.Tests.Services
{
    public class EncryptionServiceTests
    {
        private IEncryptionService _subjectUnderTest;

        private string _key => "SAV76USXIJOBMEQXPANUOQM6F5LIOTLPDIDVRJBFFE2MDJXG24TAPUU7";

        public EncryptionServiceTests()
        {
            _subjectUnderTest = new EncryptionService();
        }

        [Fact]
        public void TestEncryptDecryptRecoveryKey()
        {
            byte[] iv = _subjectUnderTest.GenerateIv();

            var answers =
            new List<string>()
            {
                "lollipop",
                "popsicle",
                "america",
                "purple",
                "witchitah"
            };
            var encrypted = _subjectUnderTest.EncryptRecoveryKey(_key, answers, iv);
            var decrypted = _subjectUnderTest.DecryptRecoveryKey(encrypted, answers, iv);
            Assert.Equal(_key, decrypted);
        }

        [Fact]
        public void TestEncryptDecryptSecretKey()
        {
            byte[] iv = _subjectUnderTest.GenerateIv();
            var password = "ksdjf*KAJSDF123ksdjf*KAJSDF123";
            var encrypted = _subjectUnderTest.EncryptSecretKey(_key, iv, password);
            var decrypted = _subjectUnderTest.DecryptSecretKey(encrypted, iv, password);
            Assert.Equal(_key, decrypted);
        }

    }
}
