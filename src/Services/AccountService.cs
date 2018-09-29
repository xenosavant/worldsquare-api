using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceStack;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context;
using Stellmart.Api.Data.Account;
using Stellmart.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly ISecurityQuestionDataManager _securityQuestionDataManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHorizonService _horizonService;
        private readonly IEncryptionService _encryptionService;
        private readonly IRequestIpAddressLogic _requestIpAddressLogic;
        private readonly ICountryResolverLogic _countryResolverLogic;
        private readonly ICountryDataManager _countryDataManager;
        private readonly IUserDataManager _userDataManager;
        private readonly IMapper _mapper;

        public AccountService
            (
                ISecurityQuestionDataManager securityQuestionDataManager,
                IHorizonService horizonService,
                IEncryptionService encryptionService,
                IRequestIpAddressLogic requestIpAddressLogic,
                ICountryResolverLogic countryResolverLogic,
                ICountryDataManager countryDataManager,
                IUserDataManager userDataManager,
                IMapper mapper
            )
        {
            _securityQuestionDataManager = securityQuestionDataManager;
            _horizonService = horizonService;
            _encryptionService = encryptionService;
            _requestIpAddressLogic = requestIpAddressLogic;
            _countryResolverLogic = countryResolverLogic;
            _countryDataManager = countryDataManager;
            _userDataManager = userDataManager;
            _mapper = mapper;
        }

        public async Task<bool> SignupAsync(ApplicationUserModel model, HttpContext httpContext)
        {
            var ipAddress = _requestIpAddressLogic.Get(httpContext);

            var resolvedCountryIso = _countryResolverLogic.Resolve(ipAddress);

            var country = await _countryDataManager.GetByIsoAsync(resolvedCountryIso);

            var stellarKeyPair = _horizonService.CreateAccount();

            var stellarPublicAddress = stellarKeyPair.PublicKey;
            var stellarSecretKeyIv = _encryptionService.GenerateIv();
            var stellarEncryptedSecretKey = _encryptionService.EncryptSecretKey(stellarKeyPair.SecretKey, stellarSecretKeyIv, model.Password);

            var stellarRecoveryKey = _encryptionService.EncryptRecoveryKey(stellarKeyPair.SecretKey, model.Answers, stellarSecretKeyIv);

            // to-do: set up mapper
            var userModel = new ApplicationUserModel
            {
                Email = model.Email,
                Password = model.Password,
                StellarPublicKey = stellarPublicAddress,
                StellarEncryptedSecretKey = stellarEncryptedSecretKey,
                StellarRecoveryKey = stellarRecoveryKey,
                StellarSecretKeyIv = stellarSecretKeyIv,
                SecurityQuestions = model.Questions.SerializeToString(),
                IpAddress = ipAddress.ToString(),
                CountryId = country.Id
            };

            return await _userDataManager.SignupAsync(userModel);
        }

        public async Task<IReadOnlyCollection<SecurityQuestionModel>> GetSecurityQuestionsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<SecurityQuestionModel>>(await _securityQuestionDataManager.GetSecurityQuestionsAsync());
        }
    }
}
