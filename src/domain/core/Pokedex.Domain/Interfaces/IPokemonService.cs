using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Domain.Interfaces
{
    public interface IPokemonService
    {
        Pokemon GetInformation(string pokemonName);
        Task<string> GetDescription(string description, string habitat, bool isLegendary);
    }
}
