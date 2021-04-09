using Pokedex.DataObjects.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Interfaces
{
    public interface IPokemonManager
    {
        Pokemon GetInformation(string pokemonName);
        Pokemon GetTranslatedInformation(string pokemonName);
    }
}
