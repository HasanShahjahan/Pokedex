using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Interfaces
{
    public interface IPokemonService
    {
        Pokemon GetInformation(string pokemonName);
        string GetDescription(Pokemon model);
    }
}
