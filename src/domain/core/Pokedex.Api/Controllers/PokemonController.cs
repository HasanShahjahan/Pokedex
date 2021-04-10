using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Pokedex.Common.Exceptions;
using Pokedex.Domain.Interfaces;
using Pokedex.Validator;

namespace Pokedex.Api.Controllers
{
    public class PokemonController : ControllerBase
    {
        private readonly IValidator _payloadValidator;
        private readonly IPokemonManager _pokemonManager;

        public PokemonController(IValidator payloadValidator, IPokemonManager pokemonManager)
        {
            _payloadValidator = payloadValidator;
            _pokemonManager = pokemonManager;
        }

        [HttpGet("pokemon/{name}")]
        public IActionResult GetInformation(string name)
        {
            var (statusCode, errorResult) = _payloadValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], name);
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);

            var result = _pokemonManager.GetInformation(name, false); // IsTranslated information is false
            if (string.IsNullOrEmpty(result.Name)) return StatusCode(StatusCodes.Status401Unauthorized, new ApplicationException { ErrorCode = ApplicationErrorCodes.Unauthorized, Data = new ErrorData() { Field = "Name", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.Unauthorized) } });
            
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("pokemon/translated/{name}")]
        public IActionResult GetTranslatedInformation(string name)
        {
            var (statusCode, errorResult) = _payloadValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], name);
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);

            var result = _pokemonManager.GetInformation(name, true); // IsTranslated information is true
            if (string.IsNullOrEmpty(result.Name)) return StatusCode(StatusCodes.Status401Unauthorized, new ApplicationException { ErrorCode = ApplicationErrorCodes.Unauthorized, Data = new ErrorData() { Field = "Name", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.Unauthorized) } });
            
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
