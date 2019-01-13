using AutoMapper;
using Stellmart.Api.Business.Helpers;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context;
using Stellmart.Api.Data.Account;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Stellmart.Api.Business.Managers
{
    public class UserDataManager : IUserDataManager
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserDataManager
            (
                IRepository repository,
                IMapper mapper,
                UserManager<ApplicationUser> userManager
            )
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> SignupAsync(ApplicationUserModel model)
        {
            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("role", RolesTypes.Member.GetEnumDescription()));
                return result.Succeeded;
            }

            return false;
        }

        public async Task<ApplicationUser> GetByIdAsync(int id, string navParams = null)
        {
            return await _repository.GetOneAsync<ApplicationUser>(u => u.Id == id, navParams);
        }

        public async Task<IReadOnlyCollection<ApplicationUser>> GetAllAsync()
        {
            return await _repository.GetAllAsync<ApplicationUser>() as IReadOnlyCollection<ApplicationUser>;
        }

        //example method for ordering and including entities by eager loading (aka include())
        public async Task<IReadOnlyCollection<ApplicationUser>> GetAllOrderingAndIncludeExampleAsync()
        {
            return await _repository.GetAsync<ApplicationUser>(x => x.IsActive, x => x.OrderByDescending(y => y.CreatedDate), "NameOfRelatedEntity,NameOfOtherRelatedEntity,NameOfRelatedEntity.ChildEntity") as IReadOnlyCollection<ApplicationUser>;
        }
    }
}
