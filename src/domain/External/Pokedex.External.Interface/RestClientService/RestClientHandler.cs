using Newtonsoft.Json;
using Pokedex.DataObjects.Settings;
using Pokedex.External.Interface.Model;
using Pokedex.External.Interface.RestClient;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.External.Interface.RestClientService
{
    public class RestClientHandler : IRestClientHandler
    {
        private readonly AppSettings _appSettings;
        public RestClientHandler(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public string GetDescription(string baseUrl, string resource, bool flag, string apiKey, string text)
        {
            try 
            {
                var client = new RestSharp.RestClient(baseUrl);
                var request = new RestRequest(resource, Method.GET);
                if (flag) request.AddParameter("api_key", apiKey);
                request.AddParameter("text", text, ParameterType.QueryString);
                var response = client.Execute(request);
                var result = JsonConvert.DeserializeObject<TranslationResponse>(response.Content);
                return result?.Contents?.Translated;
            }
            catch 
            {
                return string.Empty;
            }
            
        }
        
    }
}
