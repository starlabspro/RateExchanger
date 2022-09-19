using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RateExchanger.Features;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class RateExchangerController : ControllerBase
{
    private readonly ILogger<RateExchangerController> _logger;
    private readonly IMediator _mediator;

    public RateExchangerController(ILogger<RateExchangerController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetExchangeRate([FromBody] GetRateCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get exchange rates for {BaseCurrency}", command.BaseCurrency);

        var rateResponseDto = await _mediator.Send(command, cancellationToken);
        return Ok(rateResponseDto);
    }
}