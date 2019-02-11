using System;

namespace program {

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hi!!");
            
            /*using (var dbContext = new EntitiesCore()) {
                var listOfPersons = dbContext.Person.ToList();

                Console.WriteLine("-------database names--------");
                listOfPersons.ForEach(Person => Console.WriteLine(Person.name));
                Console.WriteLine("-----------------------------");
            }
            
            Console.Write("Write a name: ");
            var _name       = Console.ReadLine();
            
            Console.Write("write class ID: ");
            var _classroom  = Console.ReadLine();
            
            using (var db = new EntitiesCore()) {
                
                db.Person.Add(
                      new Person {
                         name = _name,
                         idClassroom = _classroom.ToInt(),
                         birthday = DateTime.Now
                      }
                );
                
                db.SaveChanges();
            }*/
        }
    }
}