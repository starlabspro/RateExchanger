using BuildingBlocks.FixerClient;
using MediatR;
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
    private readonly IMediator _mediator;

    public RateExchangerController(ILogger<RateExchangerController> logger, IFixerClient fixerClient, IMediator mediator)
    {
        _logger = logger;
        _fixerClient = fixerClient;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetExchangeRate([FromBody] GetRateCommand command, CancellationToken cancellationToken)
    {
        decimal amount = 10;
        string from = "EUR";
        string to = "USD";

        var res = await _mediator.Send(new GetRateCommand(to, from, amount), cancellationToken);
        var response = await _fixerClient.ConvertAsync(to, from, amount);
        _logger.LogInformation(JsonConvert.SerializeObject(response));
        return Ok();
    }
}