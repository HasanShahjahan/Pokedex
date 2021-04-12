using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.External.Interface.CustomService
{
    public interface ITranslatedPokemon
    {
        Task<string> GetDescription(string baseUrl, string resource, bool flag, string apiKey, string text);
    }
}
