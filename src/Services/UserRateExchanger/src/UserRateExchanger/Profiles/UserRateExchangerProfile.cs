using BuildingBlocks.Contracts;
using UserRateExchanger.Features;

namespace UserRateExchanger.Profiles;

public class UserRateExchangerProfile : AutoMapper.Profile
{
    public UserRateExchangerProfile()
    {
        CreateMap<GetUserRateCommand, GetExchangeRateRequest>();

        CreateMap<GetExchangeRateResponse, GetUserRateResponseDto>();
    }
}