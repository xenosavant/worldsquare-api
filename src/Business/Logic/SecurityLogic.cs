using Stellmart.Api.Context;
using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stellmart.Api.Services;

namespace Stellmart.Api.Business.Logic
{
    public class SecurityLogic
    {
        private readonly IRepository _repository;
        private readonly IEncryptionService _encryptionService;

        public SecurityLogic(IRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }

        public async Task<List<SecurityQuestionViewModel>> GetQuestionsAsync(string userName)
        {
            var user = await _repository.GetOneAsync<ApplicationUser>(u => u.UserName == userName);
            return JsonConvert.DeserializeObject<List<SecurityQuestionViewModel>>(user.SecurityQuestions);
        }

        public async Task<bool> AnswerQuestionsAsync(string userName, List<SecurityAnswerViewModel> answers)
        {
            var user = await _repository.GetOneAsync<ApplicationUser>(u => u.UserName == userName);
            var recoveryKey = user.StellarRecoveryKey;
            var decryptedKey = _encryptionService.DecryptRecoveryKey(recoveryKey, answers);

            // check that key is valid from horizon

            return true;
        }
    }
}
