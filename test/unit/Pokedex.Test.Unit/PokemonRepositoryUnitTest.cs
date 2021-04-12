using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Pokedex.Api;
using Pokedex.DataObjects.Settings;
using Pokedex.External.Interface.CustomService;
using Pokedex.External.Interface.RestClient;
using Pokedex.Infrastructure.Repository;
using Pokedex.Test.Resolver;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.Test.Unit
{
    public class PokemonRepositoryUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        private readonly AppSettings _appSettings;

        public PokemonRepositoryUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
            _appSettings = _serviceProvider.GetService<AppSettings>();
        }

        [Fact]
        public void InformationNotFound()
        {
            var pokemonRepository = _serviceProvider.GetService<IPokemonRepository>();
            var result = pokemonRepository.GetInformationAsync("TrueLayer");
            Assert.Null(result);
        }

        [Fact]
        public void GetInformation()
        {
            var pokemonRepository = _serviceProvider.GetService<IPokemonRepository>();
            var result = pokemonRepository.GetInformationAsync("Hasan");
            if(result != null) Assert.Equal("Hasan", result.Name);
        }
    }
}
