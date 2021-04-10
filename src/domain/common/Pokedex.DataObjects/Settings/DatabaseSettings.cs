using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.DataObjects.Settings
{
    public class DatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
