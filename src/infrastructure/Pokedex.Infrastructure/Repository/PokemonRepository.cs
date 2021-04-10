using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Infrastructure.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IRepository<Pokemon> _repository;
        public PokemonRepository(IRepository<Pokemon> repository)
        {
            _repository = repository;
        }
        public Pokemon GetInformationAsync(string name)
        {
            var pokemon = new Pokemon();
            pokemon = _repository.Get(c => c.Name.ToLower() == name.ToLower());
            return pokemon;
        }
    }
}
