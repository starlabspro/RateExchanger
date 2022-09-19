using BuildingBlocks.FixerClient.Contracts;
using RateExchanger.Features;

namespace RateExchanger.Profiles;

public class RateExchangerProfile : AutoMapper.Profile
{
    public RateExchangerProfile()
    {
        CreateMap<GetRateCommand, GetRateResponseDto>()
            .ForMember(x => x.Rates, x => x.Ignore());

        CreateMap<GetLatestRatesResponse, GetRateResponseDto>()
            .ForMember(x => x.BaseCurrency, opt => opt.MapFrom(x => x.Base));
    }
}