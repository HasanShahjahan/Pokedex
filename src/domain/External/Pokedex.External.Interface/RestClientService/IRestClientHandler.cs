using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.External.Interface.RestClient
{
    public interface IRestClientHandler
    {
        Task<string> GetDescription(string baseUrl, string resource, bool flag, string apiKey, string text);
    }
}
