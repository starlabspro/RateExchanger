using BuildingBlocks.Validation.Contracts;
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
        private IValidatorService _validatorService;

        public RateExchangerUserLimiterController(IValidatorService validatorService)
        {
            _validatorService = validatorService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult GetExchangeRateUser([FromBody] UserMessage userMessage)
        {
            if (userMessage == null) throw new ArgumentNullException("message is null");
            if (_validatorService.ValidateRequestOfTheUser(userMessage.UserId))
            {
                return Ok(userMessage);
            }
            else
            {
                throw new NotSupportedException("user has exceeded more then 10 request for 1 hour");
            }      
        }
    }
}