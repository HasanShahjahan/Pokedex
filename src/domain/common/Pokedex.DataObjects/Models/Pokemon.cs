using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Pokedex.DataObjects.Models
{
    /// <summary>
    ///   Represents pokeman class as a sequence of object units.
    ///</summary>
    public class Pokemon
    {
        /// <summary>
        ///     Initializes a new instance of the pokemon object to the value indicated
        ///     by all members.
        /// </summary>
        public Pokemon()
        {
            Name = string.Empty;
            Description = string.Empty;
            Habitat = string.Empty;
            IsLegendary = false;
        }

        /// <summary>
        ///     Initializes a new instance of the pokemon object to the value indicated
        ///     by all members.
        /// </summary>
        public Pokemon(string name, string description, string habitat, bool isLegendary)
        {
            Name = name;
            Description = description;
            Habitat = habitat;
            IsLegendary = isLegendary;
        }

        /// <summary>
        ///     Gets and sets the Pokemon name in the current url object.
        /// </summary>
        /// <returns>
        ///     The Pokemon name in the current url.
        ///</returns>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets and sets the Pokemon description in the current url object.
        /// </summary>
        /// <returns>
        ///     The Pokemon description in the current url.
        ///</returns>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Gets and sets the Pokemon habitat in the current url object.
        /// </summary>
        /// <returns>
        ///     The Pokemon habitat in the current url.
        ///</returns>
        [JsonPropertyName("habitat")]
        public string Habitat { get; set; }

        /// <summary>
        ///     Gets and sets the Pokemon legendary status in the current url object.
        /// </summary>
        /// <returns>
        ///     The Pokemon legendary status in the current url.
        ///</returns>
        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }
    }
}
