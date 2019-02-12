using System;
using System.Collections.Generic;
using System.Linq;

using DAL.Configuration;
using DAL.Entities;

namespace program {
    
    public static class ListExtensions {
        public static List<TSource> ToList2<TSource, TResult>(this List<TSource> list, Func<TSource, TResult> selector) {
            list.ToList()
                .Select(selector);
            
            return new List<TSource>();
            
        }
    }

    class Program {
        
        public List<Usuario> listUsers { get; set; }
        
        static void Main(string[] args) {
            var Program = new Program();
            List<Usuario> listUsuario = new List<Usuario>();
            
            listUsuario.ToList2(User => User.login);

            listUsuario.Where(User => User.dtExpiracao > DateTime.Now);
    
            /*
             * delegate (Usuario user) {
             *     return user.dtExpiracao > Date.now();
             * }
             */
            
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

            using (var db = new DataContext("StdSqlServer")) {
                var users = db.Usuario.ToList();
                
                if (users.Any(u => u.login == _login)) {
                    var user = db.Usuario.FirstOrDefault(u => u.login == _login) ?? new Usuario();

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
        public void prg() {
            string teste = "";

            var splited = teste.Split(" ").ToList(); 
                
            Console.WriteLine("-------database names--------");
            listUsers.ForEach(
                Person => Console.WriteLine(Person.login)
            );
            Console.WriteLine("-----------------------------");
            
            Console.Write("Write a mail: ");
            var _mail = Console.ReadLine();

            Console.Write("Write a password: ");
            var _pass = Console.ReadLine();

            using (var db = new DataContext("SqlServer")) {
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