using Pokedex.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Common.Enums
{
    public sealed class Habitat : StringEnum
    {
        public Habitat(string value) : base(value)
        {
        }
        public const string Cave = "cave";
        public const string Rare = "rare";
    }
}
