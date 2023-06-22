using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserRateExchanger.Features;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UserRateExchangerController : ControllerBase
{
    private readonly ILogger<UserRateExchangerController> _logger;
    private readonly IMediator _mediator;

    public UserRateExchangerController(ILogger<UserRateExchangerController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetExchangeRate([FromBody] GetUserRateCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get exchange rates for {BaseCurrency} and user {UserId}",
            command.BaseCurrency, command.UserId);

        var rateResponseDto = await _mediator.Send(command, cancellationToken);
        return Ok(rateResponseDto);
    }
}