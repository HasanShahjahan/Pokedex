using Pokedex.DataObjects.Settings;
using Pokedex.External.Interface.RestClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.External.Interface.CustomService
{
    public class TranslatedPokemon : ITranslatedPokemon
    {
        private readonly IRestClientHandler _restClientHandler;

        public TranslatedPokemon(IRestClientHandler restClientHandler)
        {
            _restClientHandler = restClientHandler;
        }

        public async Task<string> GetDescription(string baseUrl, string resource, bool flag, string apiKey, string text) 
        {
            var description = await _restClientHandler.GetDescription(baseUrl, resource, flag, apiKey, text);
            return description;
        }
    }
}
