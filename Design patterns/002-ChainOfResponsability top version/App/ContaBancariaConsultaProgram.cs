using System;

using Core.Model.Financeiro.enums;
using Core.Model.Financeiro.Wrappers;
using Core.Services.Financeiro.Service;

namespace App {

    internal class ConversorProgram {
        public void Run() {

            var requisicao = new RequisicaoAgregate {
                Formato = FormatoDocumentoEnum.JSON
            };

            var requisicaoRetorno = FormatadorResposta.Formatar(requisicao);

            Console.WriteLine($"Nome: {requisicaoRetorno.Conta.NomeTitular}");
            Console.WriteLine($"Saldo: R${requisicaoRetorno.Conta.Saldo}");
            Console.WriteLine($"Resposta:\n{requisicaoRetorno.Resposta}");
        }
    }

}