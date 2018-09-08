using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Stellmart.Api.Services
{
    public class EncryptionService : IEncryptionService
    {

        private Aes _aes;
        private Aes Aes
        {
            get
            {
                if (ReferenceEquals(_aes, null))
                {
                    var aes = Aes.Create();
                    aes.BlockSize = 128;
                    aes.KeySize = 256;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Mode = CipherMode.CBC;
                    return _aes = aes;
                }
                else
                {
                    return _aes;
                }
            }
        }

        public string DecryptRecoveryKey(byte [] bytes, IReadOnlyCollection<string> answers, byte[] IV)
        {
            var reversedAnswers = answers.Reverse();
            foreach (var answer in reversedAnswers)
            {
                var downCased = answer.ToLower();
                var key = KeyToThirtyTwoBytes(downCased);
                bytes = DecryptBytesFromBytes(bytes, key, IV);
            }
            return Encoding.UTF8.GetString(bytes);
        }

        public byte[] EncryptRecoveryKey(string text, IReadOnlyCollection<string> answers, byte[] IV)
        {
            var textAsBytes = Encoding.UTF8.GetBytes(text);
            foreach (var answer in answers)
            {
                var downCased = answer.ToLower();
                var key = KeyToThirtyTwoBytes(downCased);
                textAsBytes = EncryptBytesToBytes(textAsBytes, key, IV);
            }
            return textAsBytes;
        }

        public byte [] EncryptSecretKey(string text, byte[] iv, string password)
        {
            var key = KeyToThirtyTwoBytes(password);
            var bytes = Encoding.UTF8.GetBytes(text);
            return EncryptBytesToBytes(bytes, key, iv);
        }

        public string DecryptSecretKey(byte[] bytes, byte[] iv, string password)
        {
            var key = KeyToThirtyTwoBytes(password);
            return Encoding.UTF8.GetString(DecryptBytesFromBytes(bytes, key, iv));
        }

        // Generate a random Initialization Vector
        public byte[] GenerateIv()
        {
            var array = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(array);
            return array;
        }

        // This method fits the key to 32 bytes so it can be used in AES
        public byte[] KeyToThirtyTwoBytes(string key)
        {
            var keyAsBytes = Encoding.UTF8.GetBytes(key);
            if (keyAsBytes.Length < 32)
            {
                var buffer = new byte[32];
                var keyIndex = 0;
                for (var i = 0; i < 32; i++)
                {
                    if (keyIndex == keyAsBytes.Length)
                    {
                        keyIndex = 0;
                    }
                    buffer[i] = keyAsBytes[keyIndex];
                    keyIndex++;
                }
                return buffer;
            }
            else
            {
                return keyAsBytes;
            }
        }

        private byte[] DecryptBytesFromBytes(byte[] bytes, byte[] Key, byte[] IV)
        {

            Aes.Key = Key;
            Aes.IV = IV;
            var decryptor = Aes.CreateDecryptor();

            using (MemoryStream msDecrypt = new MemoryStream())
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                {
                    csDecrypt.Write(bytes, 0, bytes.Length);
                    csDecrypt.FlushFinalBlock();
                    return msDecrypt.ToArray();
                }
            }
        }

        private byte[] EncryptBytesToBytes(byte[] text, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            Aes.Key = Key;
            Aes.IV = IV;
            var encryptor = Aes.CreateEncryptor();

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(text, 0, text.Length);
                    csEncrypt.Close();
                    encrypted = msEncrypt.ToArray();
                }
            }
            return encrypted;
        }



    }
}
