using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RateExchangerUserLimiter.Extensions
{

    [ApiController]
    [Route("[controller]")]
    public class RateExchangerUserLimiterController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult Test()
        {
            return Ok("This Works!");
        }

        [HttpGet("ping")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult Ping()
        {
            return Ok("pong");
        }
    }
}