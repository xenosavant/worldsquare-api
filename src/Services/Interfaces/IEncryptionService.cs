using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IEncryptionService
    {
        byte[] EncryptRecoveryKey(string text, List<SecurityAnswerViewModel> answers);
        string DecryptRecoveryKey(byte[] text, List<SecurityAnswerViewModel> answers);
        byte[] EncryptSecretKey(string text, byte[] iv, string password);
        string DecryptSecretKey(byte[] bytes, byte[] iv, string password);
    }
}
