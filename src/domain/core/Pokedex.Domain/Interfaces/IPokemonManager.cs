using Pokedex.DataObjects.Models;
using System.Threading.Tasks;

namespace Pokedex.Domain.Interfaces
{
    public interface IPokemonManager
    {
        Task<Pokemon> GetInformation(string pokemonName, bool isTranslated);
    }
}
