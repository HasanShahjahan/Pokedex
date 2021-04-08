using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
            var result = _repository.Get(c => c.Name == name);
            return result;
        }
    }
}
