using Pokedex.DataObjects.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Interfaces
{
    public interface IPokemonService
    {
        Pokemon GetInformation(string pokemonName);
    }
}
