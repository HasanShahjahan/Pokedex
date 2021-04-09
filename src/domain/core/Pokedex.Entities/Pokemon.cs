using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pokedex.Entities
{
    public class Pokemon : BaseEntity
    {
        public Pokemon() 
        {
            Name = string.Empty;
            Description = string.Empty;
            Habitat = string.Empty;
            IsLegendary = false;
        }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Habitat")]
        public string Habitat { get; set; }

        [BsonElement("IsLegendary")]
        public bool IsLegendary { get; set; }
    }
}
