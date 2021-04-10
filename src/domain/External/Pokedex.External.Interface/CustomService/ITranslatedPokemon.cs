using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.External.Interface.CustomService
{
    public interface ITranslatedPokemon
    {
        string GetDescription(string baseUrl, string resource, bool flag, string apiKey, string text);
    }
}
