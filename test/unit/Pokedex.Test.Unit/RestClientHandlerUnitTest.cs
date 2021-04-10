using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Pokedex.Api;
using Pokedex.DataObjects.Settings;
using Pokedex.External.Interface.CustomService;
using Pokedex.External.Interface.RestClient;
using Pokedex.Test.Resolver;
using Xunit;

namespace Pokedex.Test.Unit
{
    public class RestClientHandlerUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        private readonly AppSettings _appSettings;

        private readonly string description = "Open Banking is the secure way to give providers access to your financial information.";
        private readonly string yodaDescription = "The secure way to give providers access to your financial information,  open banking is.";
        private readonly string shakespeareDescription = "Ope banking is the secure way to giveth providers access to thy financial information.";
        public RestClientHandlerUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
            _appSettings = _serviceProvider.GetService<AppSettings>();
        }

       
        [Fact]
        public void GetYodaTranslatedDescriptionOrDefault()
        {
            var restClientHandler = _serviceProvider.GetService<IRestClientHandler>();
            var result = restClientHandler.GetDescription(_appSettings.Translator.Url, _appSettings.Translator.Yoda.Resource, _appSettings.Translator.Yoda.IsPaid, _appSettings.Translator.Yoda.Key , description);
            if(!string.IsNullOrEmpty(result)) Assert.Equal(yodaDescription, result);
            else Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetShakespeareTranslatedDescriptionOrDefault()
        {
            var restClientHandler = _serviceProvider.GetService<IRestClientHandler>();
            var result = restClientHandler.GetDescription(_appSettings.Translator.Url, _appSettings.Translator.Shakespeare.Resource, _appSettings.Translator.Shakespeare.IsPaid, _appSettings.Translator.Shakespeare.Key, description);
            if(!string.IsNullOrEmpty(result)) Assert.Equal(shakespeareDescription, result);
            else Assert.Equal(string.Empty, result);
        }
    }
}
