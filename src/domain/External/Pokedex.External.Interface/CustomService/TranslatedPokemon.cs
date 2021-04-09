using Pokedex.DataObjects.Settings;
using Pokedex.External.Interface.RestClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.External.Interface.CustomService
{
    public class TranslatedPokemon : ITranslatedPokemon
    {
        private readonly AppSettings _appSettings;
        private readonly IRestClientHandler _restClientHandler;

        public TranslatedPokemon(AppSettings appSettings, IRestClientHandler restClientHandler)
        {
            _appSettings = appSettings;
            _restClientHandler = restClientHandler;
        }

        public string GetDescription(string baseUrl, string resource, bool flag, string apiKey, string text) 
        {
            var description = _restClientHandler.GetDescription(baseUrl, resource, flag, apiKey, text);
            return description;
        }
    }
}
