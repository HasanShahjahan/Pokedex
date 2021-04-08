using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Entities
{
    public abstract class BaseEntity
    {
        public ObjectId Id { get; set; }
    }
}
