using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Infrastructure.Repository
{
    public interface IPokemonRepository
    {
        Pokemon GetInformationAsync(string name);
    }
}
