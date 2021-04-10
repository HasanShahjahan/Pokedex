using Pokedex.Common.Enums;
using Pokedex.DataObjects.Settings;
using Pokedex.Domain.Interfaces;
using Pokedex.Entities;
using Pokedex.External.Interface.CustomService;
using Pokedex.Infrastructure.Repository;

namespace Pokedex.Domain.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ITranslatedPokemon _translatedPokemon;
        private readonly AppSettings _appSettings;

        public PokemonService(IPokemonRepository pokemonRepository, ITranslatedPokemon translatedPokemon, AppSettings appSettings)
        {
            _pokemonRepository = pokemonRepository;
            _translatedPokemon = translatedPokemon;
            _appSettings = appSettings;
        }

        public Pokemon GetInformation(string pokemonName)
        {
            return _pokemonRepository.GetInformationAsync(pokemonName);
        }

        public string GetDescription(Pokemon model)
        {
            string description = model.Habitat == Habitat.Cave || model.IsLegendary
                ? _translatedPokemon.GetDescription(_appSettings.Translator.Url, _appSettings.Translator.Yoda.Resource, _appSettings.Translator.Yoda.IsPaid, _appSettings.Translator.Yoda.Key, model.Description)
                : _translatedPokemon.GetDescription(_appSettings.Translator.Url, _appSettings.Translator.Shakespeare.Resource, _appSettings.Translator.Shakespeare.IsPaid, _appSettings.Translator.Shakespeare.Key, model.Description);
            
            return model.Description = string.IsNullOrEmpty(description) ? model.Description : description;
        }
    }
}
