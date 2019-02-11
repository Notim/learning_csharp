using System;
using System.Linq;

using entities.core;
using entities.core.Entities;

namespace program {

    class Program {
        static void Main(string[] args) {
            using (var dbContext = new EntitiesCore()) {
                var listOfPersons = dbContext.Person.ToList();

                listOfPersons.ForEach(Person => Console.WriteLine(Person.name));
            }
            Console.WriteLine("Write a name!");

            using (var db = new EntitiesCore()) {
                db.Classrooms.Add(
                   new Classroom {
                        desc = "class room 2"
                   }
                );
                db.SaveChanges();
                
                db.Person.Add(
                      new Person {
                         name = "joao",
                         idClassroom = 1,
                         birthday = DateTime.Now
                      }
                );
                
                db.SaveChanges();
            }
        }
    }
}