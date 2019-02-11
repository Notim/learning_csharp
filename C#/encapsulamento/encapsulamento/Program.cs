using System;

namespace encapsulamento
{
    class Program
    {
        static void Main(string[] args)
        {
            Teste ObjTeste = new Teste();

            ObjTeste.name = "Joao";

            Console.WriteLine(ObjTeste.name);
        }
    }

    class Teste
    {
        public string name { get; set; }
    }
}