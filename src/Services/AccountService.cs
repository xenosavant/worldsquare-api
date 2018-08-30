using AutoMapper;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.Account;
using Stellmart.Data.Account;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserDataManager _userDataManager;
        private readonly ISecurityQuestionDataManager _securityQuestionDataManager;
        private readonly IMapper _mapper;

        public AccountService(IUserDataManager userDataManager, ISecurityQuestionDataManager securityQuestionDataManager, IMapper mapper)
        {
            _userDataManager = userDataManager;
            _securityQuestionDataManager = securityQuestionDataManager;
            _mapper = mapper;
        }

        public async Task SignupAsync(SignupRequest request)
        {
            await _userDataManager.SignupAsync(request);
        }

        public async Task<SignupResponse> GetSignupResponseAsync()
        {
            return _mapper.Map<SignupResponse>(await _securityQuestionDataManager.GetSecurityQuestions());
        }
    }
}
