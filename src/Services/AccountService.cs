using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ServiceStack;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context;
using Stellmart.Api.Data.Account;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly ISecurityQuestionDataManager _securityQuestionDataManager;
        private readonly IHorizonService _horizonService;
        private readonly IEncryptionService _encryptionService;
        private readonly IRequestIpAddressLogic _requestIpAddressLogic;
        private readonly ICountryResolverLogic _countryResolverLogic;
        private readonly ICountryDataManager _countryDataManager;
        private readonly IUserDataManager _userDataManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<HostSettings> _hostSettings;
        private readonly IEmailTemplateService _emailTemplateService;

        public AccountService
            (
                ISecurityQuestionDataManager securityQuestionDataManager,
                IHorizonService horizonService,
                IEncryptionService encryptionService,
                IRequestIpAddressLogic requestIpAddressLogic,
                ICountryResolverLogic countryResolverLogic,
                ICountryDataManager countryDataManager,
                IUserDataManager userDataManager,
                IMapper mapper,
                UserManager<ApplicationUser> userManager,
                IOptions<HostSettings> hostSettings,
                IEmailTemplateService emailTemplateService
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
            _userManager = userManager;
            _hostSettings = hostSettings;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<bool> SignupAsync(ApplicationUserModel model, HttpContext httpContext)
        {
            var ipAddress = _requestIpAddressLogic.Get(httpContext);

            var resolvedCountryIso = _countryResolverLogic.Resolve(IPAddress.Parse("193.77.124.158"));

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

        public async Task<bool> ForgotPassword(ForgotPasswordRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return true;
            }

            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            // Send an email with this link
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"{_hostSettings.Value.AppUrl}resetpassword/{user.Id}/{WebUtility.UrlEncode(code)}";
            await _emailTemplateService.SendForgotPasswordEmailAsync(model.Email, "Reset Password",
                $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return true;
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            return true;
        }
    }
}
