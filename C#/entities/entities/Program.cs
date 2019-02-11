using System;
using System.Linq;
using System.Xml.Linq;

using DAL.Configuration;
using DAL.Entities;

namespace program {

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hi!!");
            Console.WriteLine("Write you user!");

            var _login    = Console.ReadLine();
            var _password = Console.ReadLine();

            using (var db = new EntitiesCore()) {
                var users = db.Usuario.ToList();

                if (users.Any(u => u.login == _login)) {
                    var user = db.Usuario.FirstOrDefault(u => u.login == _login);

                    if (user.senha != _password) {
                        Console.WriteLine("incorrect password");
                    }

                } else {
                    Console.WriteLine("we can't find this user");
                }
            }
            
            /*
            Console.WriteLine("-------database names--------");
                listOfPersons.ForEach(Person => Console.WriteLine(Person.name));
                Console.WriteLine("-----------------------------");
            }

            Console.Write("Write a name: ");
            var _name = Console.ReadLine();

            Console.Write("write class ID: ");
            var _classroom = Console.ReadLine();

            using (var db = new EntitiesCore()) {

                db.Person.Add(new Person {
                                             name        = _name,
                                             idClassroom = _classroom.ToInt(),
                                             birthday    = DateTime.Now
                                         }
                             );

                db.SaveChanges();
            }*/
        }
    }

}