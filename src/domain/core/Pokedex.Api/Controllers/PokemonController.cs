﻿using Microsoft.AspNetCore.Http;
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
        private readonly IPokemonService _pokemonService;
        public PokemonController(IValidator payloadValidator, IPokemonService pokemonService)
        {
            _payloadValidator = payloadValidator;
            _pokemonService = pokemonService;
        }
        [HttpGet("pokemon/{name}")]
        public IActionResult GetInformation(string name)
        {
            var (statusCode, errorResult) = _payloadValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], name);
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);

            var result = _pokemonService.GetInformation(name);
            if (string.IsNullOrEmpty(result.Name)) return StatusCode(StatusCodes.Status401Unauthorized, new ApplicationException { ErrorCode = ApplicationErrorCodes.Unauthorized, Data = new ErrorData() { Field = "Name", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.Unauthorized) } });
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
