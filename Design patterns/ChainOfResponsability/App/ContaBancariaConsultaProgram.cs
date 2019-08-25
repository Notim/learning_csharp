using System;

using App.ConsultaContaBancaria.Services;
using App.ConversorDocumentos.Entities;

namespace App {

    internal class ConversorProgram {
        public void Run() {
            var requisicao = new Requisicao {
                Formato = FormatoDocumentoEnum.JSON
            };

            var Conta = ContaBancariaConsulta.Consultar(requisicao);

            Console.WriteLine($"Nome: {Conta.NomeTitular}");
            Console.WriteLine($"Saldo: R${Conta.Saldo}");
            Console.WriteLine($"Resposta:\n{Conta.Resposta}");
        }
    }

}