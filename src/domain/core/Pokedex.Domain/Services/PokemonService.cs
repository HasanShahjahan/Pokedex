using Pokedex.DataObjects.Models;
using Pokedex.Domain.Interfaces;
using Pokedex.Domain.Mappers;
using Pokedex.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public Pokemon GetInformation(string pokemonName)
        {
           var result = _pokemonRepository.GetInformationAsync(pokemonName);
            return PokemonMapper.ToObject(result);
        }
    }
}
