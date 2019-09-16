using System.Linq;

using mongo.db.Entities;

using MongoDB.Driver;

namespace mongo.db.Core {

    public class MongoRepository {

        private readonly MongoDataContext Context;

        public MongoRepository(MongoDataContext _MongoDataContext) {
            Context = _MongoDataContext;
        }
        
        public IMongoCollection<Person> Person => Context.GetCollection<Person>();

        public IMongoCollection<Locator> Locator => Context.GetCollection<Locator>();
    }

}