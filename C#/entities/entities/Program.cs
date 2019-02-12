using System;
using System.Collections.Generic;
using System.Linq;

using DAL.Configuration;
using DAL.Entities;

using UTIL.methods;

namespace program {

    class Program {
        public List<Usuario> listUsers { get; set; }

        static void Main(string[] args) {
            var Program = new Program();
            
            Program.login();
        }
        
        void login(){
            
            /*using (var db = new DataContext()){

                db.Usuario.Add(
                    new Usuario {
                        login = "joao",
                        senha = "12345",
                        dtCadastro = DateTime.Now,
                        ativo = "S",
                    }
                );

                db.SaveChanges();
            }*/

            Console.WriteLine("Write you user!");
            var _login = Console.ReadLine();

            Console.WriteLine("Now your password!");
            var _password = Console.ReadLine();

            using (var db = new DataContext()) {
                var users = db.Usuario.ToList();

                if (users.Any(u => u.login == _login)) {
                    var user = db.Usuario.FirstOrDefault(u => u.login == _login);

                    if (user.senha != _password) {
                        Console.WriteLine("incorrect password");
                    } else {
                        Console.WriteLine("welcome!!" + user.login);

                        listUsers = db.Usuario.ToList();
                        prg();
                    }

                } else {
                    Console.WriteLine("we can't find this user");

                }
            }
        }
        public void prg(){
            
            Console.WriteLine("-------database names--------");
            listUsers.ForEach(
                Person => Console.WriteLine(Person.login)
            );
            Console.WriteLine("-----------------------------");
            
            Console.Write("Write a mail: ");
            var _mail = Console.ReadLine();

            Console.Write("Write a password: ");
            var _pass = Console.ReadLine();

            using (var db = new DataContext()) {
                db.Usuario.Add(
                    new Usuario{
                        login        = _mail,
                        senha        = _pass,
                        dtCadastro   = DateTime.Now,
                    }
                );
                db.SaveChanges();
            }
        }
    }

}