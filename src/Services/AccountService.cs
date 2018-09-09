using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Stellmart.Api.Business.Helpers;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context;
using Stellmart.Api.Data.Account;
using Stellmart.Api.Data.Enumerations;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserDataManager _userDataManager;
        private readonly ISecurityQuestionDataManager _securityQuestionDataManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHorizonService _horizonService;
        private readonly IEncryptionService _encryptionService;

        public AccountService
            (
                IUserDataManager userDataManager,
                ISecurityQuestionDataManager securityQuestionDataManager,
                UserManager<ApplicationUser> userManager,
                IMapper mapper,
                IHorizonService horizonService,
                IEncryptionService encryptionService
            )
        {
            _userDataManager = userDataManager;
            _securityQuestionDataManager = securityQuestionDataManager;
            _userManager = userManager;
            _mapper = mapper;
            _horizonService = horizonService;
            _encryptionService = encryptionService;
        }

        public async Task<bool> SignupAsync(ApplicationUserModel model)
        {
            var stellarKeyPair = _horizonService.CreateAccount();

            var stellarPublicAddress = stellarKeyPair.PublicKey;
            var stellarSecretKeyIv = _encryptionService.GenerateIv();
            var stellarEncryptedSecretKey = _encryptionService.EncryptSecretKey(stellarKeyPair.SecretKey, stellarSecretKeyIv, model.Password);

            var stellarRecoveryKey = _encryptionService.EncryptRecoveryKey(stellarKeyPair.SecretKey, model.Answers, stellarSecretKeyIv);

            // to-do: set up mapper

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                StellarPublicKey = stellarPublicAddress,
                StellarEncryptedSecretKey = stellarEncryptedSecretKey,
                StellarRecoveryKey = stellarRecoveryKey,
                StellarSecretKeyIv = stellarSecretKeyIv,
                PrimaryShippingLocationId = (byte)PrimaryShippingLocationTypes.Default,
                RewardsLevelId = (byte)RewardsLevelTypes.Default,
                TwoFactorTypeId = (byte)TwoFactorTypes.None,
                NativeCurrencyId = (byte)NativeCurrencyTypes.Default,
                VerificationLevelId = (byte)VerificationLevelTypes.NonVerified,
                UniqueId = Guid.NewGuid()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("role", RolesTypes.Member.GetEnumDescription()));
                return result.Succeeded;
            }

            return false;
        }

        public async Task<IReadOnlyCollection<SecurityQuestionModel>> GetSecurityQuestionsAsync()
        {
            return await _securityQuestionDataManager.GetSecurityQuestionsAsync();
        }
    }
}
