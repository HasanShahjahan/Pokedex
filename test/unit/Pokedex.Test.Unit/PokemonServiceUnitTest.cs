using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pokedex.Api;
using Pokedex.Domain.Interfaces;
using Pokedex.Test.Resolver;
using Pokedex.Validator;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.Test.Unit
{
    public class PokemonServiceUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        private readonly string description = "Open Banking is the secure way to give providers access to your financial information.";
        private readonly string yodaDescription = "The secure way to give providers access to your financial information,  open banking is.";
        private readonly string shakespeareDescription = "Ope banking is the secure way to giveth providers access to thy financial information.";
        public PokemonServiceUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void PokemonIsNotExists()
        {
            var pokemonService = _serviceProvider.GetService<IPokemonService>();
            var result = pokemonService.GetInformation("TrueLayer");
            Assert.Null(result);
        }

        [Fact]
        public void PokemonExists()
        {
            var pokemonService = _serviceProvider.GetService<IPokemonService>();
            var result = pokemonService.GetInformation("Hasan");
            Assert.Equal("Hasan", result.Name);
        }

        [Fact]
        public async Task GetYodaTranslatedDescriptionOrDefault()
        {
            var pokemonService = _serviceProvider.GetService<IPokemonService>();
            var result = await pokemonService.GetDescription("Open Banking is the secure way to give providers access to your financial information.", "cave", true);

            if (result == yodaDescription) Assert.Equal(yodaDescription, result);
            else Assert.Equal(description, result);
        }

        [Fact]
        public async Task GetShakespeareTranslatedDescriptionOrDefault()
        {
            var pokemonService = _serviceProvider.GetService<IPokemonService>();
            var result = await pokemonService.GetDescription("Open Banking is the secure way to give providers access to your financial information.", "rare", false);

            if (result == shakespeareDescription) Assert.Equal(shakespeareDescription, result);
            else Assert.Equal(description, result);
        }
    }
}
