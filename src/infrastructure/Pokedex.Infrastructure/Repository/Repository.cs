using MongoDB.Driver;
using Pokedex.DataObjects.Settings;
using Pokedex.Infrastructure.Seeder;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Pokedex.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoClient _mongoClient;
        private readonly AppSettings _appSettings;
        private readonly IMongoDatabase _database;
        public Repository(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _mongoClient = new MongoClient(_appSettings.DatabaseSettings.ConnectionString);
            _database = _mongoClient.GetDatabase(_appSettings.DatabaseSettings.DatabaseName);
            SeedingMongoDb.BulkInsertMongoDb(_database, _appSettings.DatabaseSettings.CollectionName);
        }

        public IMongoCollection<T> Get()
        {
            var result = _database.GetCollection<T>(_appSettings.DatabaseSettings.CollectionName);
            return result;
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            var result = _database.GetCollection<T>(_appSettings.DatabaseSettings.CollectionName).Find(predicate).FirstOrDefault();
            return result;
        }
    }
}