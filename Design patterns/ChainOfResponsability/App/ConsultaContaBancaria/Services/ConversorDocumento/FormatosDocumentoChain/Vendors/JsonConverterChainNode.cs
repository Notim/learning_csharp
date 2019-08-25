using System;

using App.ConversorDocumentos.Entities;

using Core.GenericChainRepository;

namespace App.ConsultaContaBancaria.Services.ConversorDocumento.FormatosDocumentoChain.Vendors {

    internal class JsonConverterChainNode : DocFormatNodeAbstract, IChainNode<Requisicao> {

        public override Requisicao Execute(Requisicao requisicao) {
            if ((byte) requisicao.Formato == (byte) FormatoDocumentoEnum.JSON) {
                Console.WriteLine("Requisicao efetuada em formato Json");

                Conta.Resposta = $"{{\r\n\t\"NomeTitular\" : \"{Conta.NomeTitular}\",\r\n\t\"Saldo\" :{Conta.Saldo:C}\r\n}}";
                requisicao.Conta = Conta;

                return requisicao;
            }

            return Next.Execute(requisicao);
        }
    }

}