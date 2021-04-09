using Pokedex.Common.Enums;
using Pokedex.DataObjects.Models;
using Pokedex.DataObjects.Settings;
using Pokedex.Domain.Interfaces;
using Pokedex.Domain.Mappers;
using Pokedex.External.Interface.CustomService;

namespace Pokedex.Domain.Managers
{
    public class PokemonManager : IPokemonManager
    {
        private readonly IPokemonService _pokemonService;
        private readonly ITranslatedPokemon _translatedPokemon;
        private readonly AppSettings _appSettings;

        public PokemonManager(IPokemonService pokemonService, ITranslatedPokemon translatedPokemon, AppSettings appSettings)
        {
            _pokemonService = pokemonService;
            _translatedPokemon = translatedPokemon;
            _appSettings = appSettings;
        }

        public Pokemon GetInformation(string pokemonName)
        {
            var result = _pokemonService.GetInformation(pokemonName);
            if (result == null) return new Pokemon();

            return PokemonMapper.ToObject(result);
        }

        public Pokemon GetTranslatedInformation(string pokemonName)
        {
            var result = _pokemonService.GetInformation(pokemonName);
            if (result == null) return new Pokemon();

            ManageDescription(result);
            return PokemonMapper.ToObject(result);
        }

        private void ManageDescription(Entities.Pokemon result)
        {
            string description;
            if (result.Habitat == Habitat.Cave || result.IsLegendary)
            {
                description = _translatedPokemon.GetDescription(_appSettings.Translator.Url, _appSettings.Translator.Yoda.Resource, _appSettings.Translator.Yoda.IsPaid, _appSettings.Translator.Yoda.Key, result.Description);
            }
            else
            {
                description = _translatedPokemon.GetDescription(_appSettings.Translator.Url, _appSettings.Translator.Shakespeare.Resource, _appSettings.Translator.Shakespeare.IsPaid, _appSettings.Translator.Shakespeare.Key, result.Description);
            }
            result.Description = string.IsNullOrEmpty(description) ? result.Description : description;
        }
    }
}
