using AutoMapper;
using ViewModel = Stellmart.Data.ViewModels;
using Stellmart.Context.Entities;

namespace Stellmart.Business
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<User, ViewModel.User>();
        }
    }
}
