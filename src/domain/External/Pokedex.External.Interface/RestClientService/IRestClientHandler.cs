using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.External.Interface.RestClient
{
    public interface IRestClientHandler
    {
        string GetDescription(string baseUrl, string resource, bool flag, string apiKey, string text);
    }
}
