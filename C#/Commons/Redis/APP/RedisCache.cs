using System;

using StackExchange.Redis;

namespace APP {

    public static class RedisCache {

        static RedisCache() {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost"));
        }
        
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection;

        private static ConnectionMultiplexer Connection => lazyConnection.Value;
        
        public static IDatabase Database => Connection.GetDatabase();

        public static string ReadFromCache(string tableName) {
            
            var value = Database.StringGet(tableName);
            Console.WriteLine(GetTimeRemain(tableName));

            return value;
        }

        public static void SaveToCache(string tableName, string data, TimeSpan? liveTime = null) {
            
            Database.StringSet(tableName, data, liveTime ?? TimeSpan.FromMinutes(1));
            
            Console.WriteLine(GetTimeRemain(tableName));
        }

        public static string GetTimeRemain(string tableName) {

            return $"Time remain for {tableName}: " + GetTimeRemain(tableName);
        }

        public static string Persist(string tableName) {
            Database.KeyPersist(tableName);
            
            return $"Time remain for {tableName}: " + GetTimeRemain(tableName);
        }

        public static string Delete(string tableName) {
            Database.KeyDelete(tableName);

            return $"Time remain for {tableName}: " + GetTimeRemain(tableName);
        }

        public static string Ping() {
            const string cmd = "PING";

            return Database.Execute(cmd).ToString();
        }

        public static string ClientList() {
            const string cmd = "CLIENT LIST";

            return Database.Execute(cmd).ToString();
        }
    }
}