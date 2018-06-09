using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Stellmart.Api.Data.ViewModels;
using System.Text;
using System.IO;

namespace Stellmart.Api.Services
{

    public interface IEncryptionService
    {
        void EncryptRecoveryKey(List<SecurityAnswerViewModel> answers);
        string DecryptRecveryKey(string text, List<SecurityAnswerViewModel> answers);
    }

    public class EncryptionService
    {

        private ICryptoTransform _decryptor => _decryptor ?? Aes.Create().CreateDecryptor();
        private ICryptoTransform _encryptor => _encryptor ?? Aes.Create().CreateEncryptor();

        public string DecryptRecoveryKey(string text, List<SecurityAnswerViewModel> answers)
        {
            var sortedAnswers = answers.OrderByDescending(a => a.Order);
            foreach (var answer in sortedAnswers)
            {
                var key = Encoding.ASCII.GetBytes(answer.Answer);
                text = DecryptStringFromString(text, key, answer.IV);
            }
            return text;
        }

        public string EncryptRecoveryKey(string text, List<SecurityAnswerViewModel> answers)
        {
            var sortedAnswers = answers.OrderBy(a => a.Order);
            foreach (var answer in sortedAnswers)
            {
                var key = Encoding.ASCII.GetBytes(answer.Answer);
                text = EncryptStringToString(text, key, answer.IV);
            }
            return text;
        }

        public string EncyrptSecretKey(string text, byte [] iv, string password)
        {
            var key = Encoding.ASCII.GetBytes(password);
            return EncryptStringToString(text, key, iv);
        }

        public string DecryptSecretKey(string text, byte[] iv, string password)
        {
            var key = Encoding.ASCII.GetBytes(password);
            return DecryptStringFromString(text, key, iv);
        }

        private string DecryptStringFromString(string plainText, byte[] Key, byte[] IV)
        {
            string decryptedtext = null;
            byte [] cipherText = Encoding.ASCII.GetBytes(plainText);

            using (Aes aes = _decryptor as Aes)
            {
                aes.Key = Key;
                aes.IV = IV;

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, _decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            decryptedtext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return decryptedtext;

        }

        private string EncryptStringToString(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            using (Aes aes = _encryptor as Aes)
            {
                aes.Key = Key;
                aes.IV = IV;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, _encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Encoding.ASCII.GetString(encrypted);

        }

    }
}
