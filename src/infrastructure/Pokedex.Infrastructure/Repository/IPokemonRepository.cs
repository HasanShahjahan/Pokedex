using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Infrastructure.Repository
{
    public interface IPokemonRepository
    {
        Pokemon GetInformationAsync(string name);
    }
}
