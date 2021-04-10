using Pokedex.DataObjects.Models;
namespace Pokedex.Domain.Interfaces
{
    public interface IPokemonManager
    {
        Pokemon GetInformation(string pokemonName, bool isTranslated);
    }
}
