using BuildingBlocks.FixerClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RateExchanger.Features;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class RateExchangerController : ControllerBase
{
    private readonly ILogger<RateExchangerController> _logger;
    private readonly IFixerClient _fixerClient;

    public RateExchangerController(ILogger<RateExchangerController> logger, IFixerClient fixerClient)
    {
        _logger = logger;
        _fixerClient = fixerClient;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetExchangeRate()
    {
        decimal amount = 10;
        string from = "EUR";
        string to = "USD";

        var response = await _fixerClient.ConvertAsync(to, from, amount);
        _logger.LogInformation(JsonConvert.SerializeObject(response));
        return Ok();
    }
}