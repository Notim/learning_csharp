using System.Linq;

using MongoDB.Driver;

namespace mongo.db.Core {

    public class MongoDataContext {

        private static IMongoClient   client   { get; set; }
        private static IMongoDatabase dataBase { get; set; }

        public MongoDataContext(MongoOptions options) {
            client   = new MongoClient(options.connectionString);
            dataBase = client.GetDatabase(options.dataBase);
        }

        public IMongoCollection<T> GetCollection<T>() {
            return dataBase.GetCollection<T>(typeof (T).Name.ToLower());
        }

        private IQueryable<T> GetQueryable<T>() {
            return GetCollection<T>().AsQueryable();
        }
    }

}