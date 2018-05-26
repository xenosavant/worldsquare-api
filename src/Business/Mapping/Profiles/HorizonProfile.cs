using AutoMapper;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.ViewModels.Horizon;
using System.Collections.Generic;

namespace Stellmart.Api.Business.Mapping.Profiles
{
    public class HorizonProfile : Profile
    {
        public HorizonProfile()
        {
            CreateMap<KeyPair, HorizonKeyPairModel>()
                .ForMember(dest => dest.PublicKey, opts => opts.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.SecretKey, opts => opts.MapFrom(src => src.SecretSeed))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<AccountResponse, HorizonFundTestAccountModel>()
                .ForMember(dest => dest.PublicKey, opts => opts.MapFrom(src => src.KeyPair.AccountId))
                .ForMember(dest => dest.Balances, opts => opts.MapFrom(src => src.Balances))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Balance, HorizonBalanceModel>()
                .ForMember(dest => dest.Asset, opts => opts.MapFrom(src => src.AssetType))
                .ForMember(dest => dest.Balance, opts => opts.MapFrom(src => src.BalanceString))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<HorizonKeyPairModel, HorizonKeyPairViewModel>();
            CreateMap<HorizonFundTestAccountModel, HorizonFundTestAccountViewModel>();
            CreateMap<HorizonBalanceModel, HorizonBalanceViewModel>();
        }
    }
}
