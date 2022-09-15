using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RateExchanger.Features;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class RateExchangerController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetExchangeRate()
    {
        return Ok();
    }
}