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
        
        private DataContext context = new DataContext("StdMysql");
        
        public List<Usuario> listUsers { get; set; }
        
        static void Main(string[] args) {
            var Program = new Program();
            
            Program.login();
        }
        
        void login(){
            /*
            using (var db = new DataContext("StdSqlServer")){

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

            
            var users = context.Usuario.ToList();
                
            if (users.Any(u => u.login == _login)) {
                var user = context.Usuario.FirstOrDefault(u => u.login == _login) ?? new Usuario();

                if (user.senha != _password) {
                    Console.WriteLine("incorrect password");
                } else {
                    Console.WriteLine("welcome!!" + user.login);

                    listUsers = context.Usuario.ToList();
                    prg();
                }

            } else {
                Console.WriteLine("we can't find this user");
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

            context.Usuario.Add(
               new Usuario {
                   login      = _mail,
                   senha      = _pass,
                   dtExpiracao = DateTime.Now,
               }
            );

            context.SaveChanges();
        }
    }

}