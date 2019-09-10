using System;
using System.Linq;

using mongo.db.Core;
using mongo.db.Entities;

using MongoDB.Bson;
using MongoDB.Driver;

namespace mongo.db {
    
    internal static class Program {
        private static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            var t = new MongoTest();

            t.start();
        }
    }
    
    class MongoTest {

        public void start() {
            var context = new MongoDataContext(
                new MongoOptions {
                    connectionString   = "mongodb://localhost:27017", 
                    dataBase           = "dev_mongo"
                }
            );
            
            var db = new MongoRepository(context);
            
            db.Person.InsertOne(
                new Person {
                    Id       = new ObjectId(),
                    titulo   = "maria",
                    acessos  = "todo",
                    conteudo = "teste de persistencia de dados no mongo db 002"
                }
            );
            
            db.Person.InsertOne(
                new Person {
                   Id       = new ObjectId(),
                   titulo   = "maria",
                   acessos  = "todo",
                   conteudo = "teste de persistencia de dados no mongo db 002"
                }
            );

            var person = db.Person.Find(x => x.conteudo == "jj").ToList().First();
            Console.WriteLine(person);

            var listPersons = db.Person.Find(x => true).ToList();
            Console.WriteLine(listPersons.ToJson());
            
            var locators = db.Locator.Find(x => x.acessos == "0").ToList();
            
            var locators2 = db.Person.AsQueryable()
                                     .Where(x => x.titulo == "Caverna");
            
            locators.ForEach(
                locator => {
                    Console.WriteLine(locator.toString());
                }
            );
        }
    }
}