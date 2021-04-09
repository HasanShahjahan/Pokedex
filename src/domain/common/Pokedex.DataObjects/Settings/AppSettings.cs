using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.DataObjects.Settings
{
    public class AppSettings
    {
        public DatabaseSettings DatabaseSettings { get; set; }
        public Token JsonWebTokens { get; set; }
        public MemoryCache MemoryCache { get; set; }
        public Translator Translator { get; set; }
    }
}
