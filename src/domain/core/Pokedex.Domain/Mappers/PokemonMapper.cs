using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain.Mappers
{
    public static class PokemonMapper
    {
        public static DataObjects.Models.Pokemon ToObject(this Pokemon model)
        {
            return new DataObjects.Models.Pokemon()
            {
                Name = model.Name,
                Description = model.Description,
                Habitat = model.Habitat,
                IsLegendary = model.IsLegendary
            };
        }
    }
}
