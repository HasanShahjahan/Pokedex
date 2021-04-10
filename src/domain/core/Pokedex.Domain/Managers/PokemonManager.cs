using Microsoft.Extensions.Logging;
using Pokedex.DataObjects.Models;
using Pokedex.Domain.Interfaces;
using Pokedex.Domain.Mappers;

namespace Pokedex.Domain.Managers
{
    public class PokemonManager : IPokemonManager
    {

        private readonly IPokemonService _pokemonService;
        private readonly ILogger<PokemonManager> _logger;

        public PokemonManager(IPokemonService pokemonService, ILogger<PokemonManager> logger)
        {
            _pokemonService = pokemonService;
            _logger = logger;
        }

        public Pokemon GetInformation(string pokemonName, bool isTranslated)
        {
            _logger.LogInformation("[Pokemon Manager] [Get Information] [Pokemon Type : isTranslated - ] " + isTranslated);
            var result = _pokemonService.GetInformation(pokemonName);
            if (result == null) return new Pokemon();

            if(isTranslated) result.Description = _pokemonService.GetDescription(result.Description, result.Habitat, result.IsLegendary);
            return PokemonMapper.ToObject(result);
        }
    }
}
