using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using APP.AppUtils.Extensions;

using Bogus;
using Bogus.Extensions.Brazil;

using Newtonsoft.Json;

using Person = APP.Entities.Person;

namespace APP.Examples {

    public class SimpleSring {
        
        // nedds up the redis server on docker to run
        // sudo sudo docker run --name redis-dokcer -p 127.0.0.1:6379:6379 -d redis
        public async Task Start(string[] args) {
            
            Console.WriteLine("Starting APP!");

            var persons = new List<Person>();

            var list1 = processing(10000, "1");
            var list2 = processing(10000, "2");
            var list3 = processing(10000, "3");
            var list4 = processing(10000, "4");
            
            Console.WriteLine("async processing");

            persons.AddRange(await list1);
            persons.AddRange(await list2);
            persons.AddRange(await list3);
            persons.AddRange(await list4);
            
            Console.WriteLine("Writing random persons data to Redis cache");
            RedisCache.SaveToCache("persons", persons.ToJson<Person>());

            Console.WriteLine("Reading random persons data from Redis cache");
            var listPersons = JsonConvert.DeserializeObject<List<Person>>(RedisCache.ReadFromCache("persons"));

            var cou = 1;
            listPersons.ForEach(
                person => {
                    person.Id = cou++;
                    Console.WriteLine(person.ToString());
                }
            );
            
            Console.ReadLine();
            Console.WriteLine(RedisCache.GetTimeRemain("persons"));
        }
        
        private static async Task<List<Person>> processing(int qtd, string threadName) {
            var userFaker = new Faker<Person>().CustomInstantiator(f => new Person())
                                               .RuleFor(o => o.Birthday,       f => f.Date.Recent(3000))
                                               .RuleFor(o => o.Name,           f => f.Name.FullName())
                                               .RuleFor(o => o.Mail,           f => f.Person.Email)
                                               .RuleFor(o => o.DocumentNumber, f => f.Person.Cpf());

            Console.WriteLine("Processing! thread " + threadName);
            var thread = await Task.Run(() => userFaker.Generate(qtd));
            Console.WriteLine($"thread {threadName} finished");

            return thread;
        }
    }

}