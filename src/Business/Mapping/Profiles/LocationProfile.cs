using AutoMapper;
using Stellmart.Api.Context;
using Stellmart.Api.Data;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<LocationModel, Location>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address))
                .ForMember(dest => dest.LocationComponentsFromApp, opts => opts.MapFrom(src => src.LocationComponentsFromApp))
                .ForMember(dest => dest.LocationComponentsFromGoogleApi, opts => opts.MapFrom(src => src.LocationComponentsFromGoogleApi))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.IsDefault, opts => opts.MapFrom(src => src.IsDefault))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Location, LocationModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address))
                .ForMember(dest => dest.LocationComponentsFromApp, opts => opts.MapFrom(src => src.LocationComponentsFromApp))
                .ForMember(dest => dest.LocationComponentsFromGoogleApi, opts => opts.MapFrom(src => src.LocationComponentsFromGoogleApi))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.IsDefault, opts => opts.MapFrom(src => src.IsDefault))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
