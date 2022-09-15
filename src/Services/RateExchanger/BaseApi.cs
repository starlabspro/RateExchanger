using Microsoft.AspNetCore.Mvc;

namespace RateExchanger;

[Route(BasePath)]
[ApiController]
[ApiVersion("1.0")]
public abstract class BaseController
{
    private const string BasePath = "api/v{version:apiVersion}";
}