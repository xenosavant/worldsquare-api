using Stellmart.Api.Data.ViewModels;
using System.Collections.Generic;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IEncryptionService
    {
        byte[] EncryptRecoveryKey(string text, IReadOnlyCollection<string> answers, byte[] IV);
        string DecryptRecoveryKey(byte[] text, IReadOnlyCollection<string> answers, byte[] IV);
        byte[] EncryptSecretKey(string text, byte[] iv, string password);
        string DecryptSecretKey(byte[] bytes, byte[] iv, string password);
        byte[] GenerateIv();
    }
}
