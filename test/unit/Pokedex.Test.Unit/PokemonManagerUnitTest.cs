using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Pokedex.Api;
using Pokedex.Domain.Interfaces;
using Pokedex.Test.Resolver;
using System.Threading.Tasks;
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
        public async Task NotFoundInformation()
        {
            var pokemonManager = _serviceProvider.GetService<IPokemonManager>();
            var result = await pokemonManager.GetInformation("TrueLayer", false);
            Assert.Empty(result.Name);
        }

        [Fact]
        public async Task GetInformation()
        {
            var pokemonManager = _serviceProvider.GetService<IPokemonManager>();
            var result = await pokemonManager.GetInformation("Hasan", false);
            Assert.Equal("Hasan", result.Name);
            Assert.NotEmpty(result.Description);
        }

        [Fact]
        public async Task NotFoundInformationWithFunTransalation()
        {
            var pokemonManager = _serviceProvider.GetService<IPokemonManager>();
            var result = await pokemonManager.GetInformation("TrueLayer", true);
            Assert.Empty(result.Name);
        }

        [Fact]
        public async Task GetInformationWithFunTransalation()
        {
            var pokemonManager = _serviceProvider.GetService<IPokemonManager>();
            var result = await pokemonManager.GetInformation("Hasan", true);
            Assert.Equal("Hasan", result.Name);
            Assert.NotEmpty(result.Description);
        }
    }
}
