using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Pokedex.Api;
using Pokedex.Domain.Interfaces;
using Pokedex.Test.Resolver;
using Xunit;

namespace Pokedex.Test.Unit
{
    public class PokemonManagerUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        public PokemonManagerUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void NotFoundInformation()
        {
            var pokemonManager = _serviceProvider.GetService<IPokemonManager>();
            var result = pokemonManager.GetInformation("TrueLayer", false);
            Assert.Empty(result.Name);
        }

        [Fact]
        public void GetInformation()
        {
            var pokemonManager = _serviceProvider.GetService<IPokemonManager>();
            var result = pokemonManager.GetInformation("Hasan", false);
            Assert.Equal("Hasan", result.Name);
            Assert.NotEmpty(result.Description);
        }

        [Fact]
        public void NotFoundInformationWithFunTransalation()
        {
            var pokemonManager = _serviceProvider.GetService<IPokemonManager>();
            var result = pokemonManager.GetInformation("TrueLayer", true);
            Assert.Empty(result.Name);
        }

        [Fact]
        public void GetInformationWithFunTransalation()
        {
            var pokemonManager = _serviceProvider.GetService<IPokemonManager>();
            var result = pokemonManager.GetInformation("Hasan", true);
            Assert.Equal("Hasan", result.Name);
            Assert.NotEmpty(result.Description);
        }
    }
}
