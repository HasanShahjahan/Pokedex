using Pokedex.DataObjects.Models;
using Pokedex.Domain.Interfaces;
using Pokedex.Domain.Mappers;

namespace Pokedex.Domain.Managers
{
    public class PokemonManager : IPokemonManager
    {

        private readonly IPokemonService _pokemonService;

        public PokemonManager(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        public Pokemon GetInformation(string pokemonName, bool isTranslated)
        {
            var result = _pokemonService.GetInformation(pokemonName);
            if (result == null) return new Pokemon();

            if(isTranslated) result.Description = _pokemonService.GetDescription(result);
            return PokemonMapper.ToObject(result);
        }
    }
}
