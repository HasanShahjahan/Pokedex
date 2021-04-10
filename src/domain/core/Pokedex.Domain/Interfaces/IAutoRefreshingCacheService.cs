using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Interfaces
{
    public interface IAutoRefreshingCacheService
    {
        Hashtable CheckCache(string pokemonName);
        void SetCache(string pokemonName, string value);
    }
}
