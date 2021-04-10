using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Pokedex.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        IMongoCollection<T> Get();
        T Get(Expression<Func<T, bool>> predicate);
    }
}
