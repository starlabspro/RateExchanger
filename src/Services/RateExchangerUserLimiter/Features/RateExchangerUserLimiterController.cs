using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RateExchangerUserLimiter.Models;
using System.Net;

namespace RateExchangerUserLimiter.Extensions
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class RateExchangerUserLimiterController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult GetExchangeRateUser([FromBody] UserMessage userMessage)
        {
            if (userMessage == null) throw new ArgumentNullException("message is null");

            return Ok(userMessage);
        }

        [HttpGet("ping")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult Ping()
        {
            return Ok("pong");
        }
    }
}