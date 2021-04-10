using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Pokedex.Api.Filters;
using Pokedex.Common.Exceptions;
using Pokedex.Domain.Interfaces;
using Pokedex.Validator;

namespace Pokedex.Api.Controllers
{
    [GlobalExceptionFilter]
    public class PokemonController : ControllerBase
    {
        private readonly IValidator _payloadValidator;
        private readonly IPokemonManager _pokemonManager;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IValidator payloadValidator, IPokemonManager pokemonManager, ILogger<PokemonController> logger)
        {
            _payloadValidator = payloadValidator;
            _pokemonManager = pokemonManager;
            _logger = logger;
        }

        [HttpGet("pokemon/{name}")]
        public IActionResult GetInformation(string name)
        {
            _logger.LogInformation("[Pokemon Controller] [Get Information] [Pokemon Name : ]" + JsonConvert.SerializeObject(name));
            var (statusCode, errorResult) = _payloadValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], name);

            _logger.LogWarning("[Pokemon Controller] [Get Information] [Pokemon Name : ] " + JsonConvert.SerializeObject(errorResult));
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);

            var result = _pokemonManager.GetInformation(name, false); // IsTranslated information is false
            _logger.LogWarning("[Pokemon Controller] [Get Information] [Result : ] " + JsonConvert.SerializeObject(result));
            if (string.IsNullOrEmpty(result.Name)) return StatusCode(StatusCodes.Status401Unauthorized, new ApplicationException { ErrorCode = ApplicationErrorCodes.Unauthorized, Data = new ErrorData() { Field = "Name", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.Unauthorized) } });
            
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("pokemon/translated/{name}")]
        public IActionResult GetTranslatedInformation(string name)
        {
            _logger.LogInformation("[Pokemon Controller] [Get Translated Information] [Pokemon Name : ]" + JsonConvert.SerializeObject(name));
            var (statusCode, errorResult) = _payloadValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], name);

            _logger.LogWarning("[Pokemon Controller] [Get Translated Information] [Pokemon Name : ] " + JsonConvert.SerializeObject(errorResult));
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);

            var result = _pokemonManager.GetInformation(name, true); // IsTranslated information is true
            _logger.LogWarning("[Pokemon Controller] [Get Translated Information] [Result : ] " + JsonConvert.SerializeObject(result));
            if (string.IsNullOrEmpty(result.Name)) return StatusCode(StatusCodes.Status401Unauthorized, new ApplicationException { ErrorCode = ApplicationErrorCodes.Unauthorized, Data = new ErrorData() { Field = "Name", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.Unauthorized) } });
            
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
