using MongoDB.Driver;
using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Infrastructure.Seeder
{
    public class SeedingMongoDb
    {
        public static void BulkInsertMongoDb(IMongoDatabase database, string collectionName)
        {
            var pokemons = new List<Pokemon>
            {
                new Pokemon { Name = "Craig", Description = "It was created by a scientist after years  of horrific genesplicing and DNA engineering experiments", Habitat = "rare", IsLegendary = true },
                new Pokemon { Name = "Law", Description = "It was created by a scientist after years  of horrific genesplicing and DNA engineering experiments", Habitat = "cave", IsLegendary = true },
                new Pokemon { Name = "Hasan", Description = "It was created by a scientist after years  of horrific genesplicing and DNA engineering experiments", Habitat = "rare", IsLegendary = false }
            };
            var collections = database.GetCollection<Pokemon>(collectionName);
            foreach (var pokemon in pokemons)
            {
                var result = collections.Find(c => c.Name == pokemon.Name).FirstOrDefault();
                if(result == null) collections.InsertOneAsync(pokemon);
            }
        }
    }
}
