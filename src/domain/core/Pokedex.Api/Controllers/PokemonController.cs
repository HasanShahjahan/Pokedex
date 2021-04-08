using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Pokedex.Validator;

namespace Pokedex.Api.Controllers
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/")]
    public class PokemonController : ControllerBase
    {
        private readonly IValidator _payloadValidator;
        public PokemonController(IValidator payloadValidator)
        {
            _payloadValidator = payloadValidator;
        }
        [HttpGet("pokemon/{name}")]
        public IActionResult GetInformation(string name)
        {
            var (statusCode, errorResult) = _payloadValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], name);
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);

            return StatusCode(StatusCodes.Status200OK, null);
        }
    }
}
