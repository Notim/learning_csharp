using System;
using System.Linq;

using DAL.Configuration;

namespace program {

    class Program {
        static void Main(string[] args) {
            /*using (var db = new EntitiesCore()) {

                for (int i = 0; i < 50; i++) {
                 
                    db.Usuario.Add(
                       new Usuario {
                           login = Populate.GenerateMail(),
                           senha = "123465456"
                       }
                    );
                }

                db.SaveChanges();
            }*/

            Console.WriteLine("Hi!!");
            Console.WriteLine("Write you user!");
            var _login    = Console.ReadLine();
            Console.WriteLine("Now your password!");
            var _password = Console.ReadLine();

            using (var db = new EntitiesCore()) {
                var users = db.Usuario.ToList();

                if (users.Any(u => u.login == _login)) {
                    var user = db.Usuario.FirstOrDefault(u => u.login == _login);

                    if (user.senha != _password) {
                        Console.WriteLine("incorrect password");
                    } else {
                        Console.WriteLine("welcome!!" + user.login);
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