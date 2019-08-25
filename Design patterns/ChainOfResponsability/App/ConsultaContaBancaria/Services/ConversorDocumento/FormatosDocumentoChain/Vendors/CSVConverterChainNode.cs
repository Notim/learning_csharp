using System;

using App.ConversorDocumentos.Entities;

using Core.GenericChainRepository;

namespace App.ConsultaContaBancaria.Services.ConversorDocumento.FormatosDocumentoChain.Vendors {

    internal class CSVConverterChainNode : DocFormatNodeAbstract, IChainNode<Requisicao> {

        public override Requisicao Execute(Requisicao requisicao) {
            if ((byte) requisicao.Formato == (byte) FormatoDocumentoEnum.CSV) {
                Console.WriteLine("Requisicao efetuada em formato CSV");

                Conta.Resposta   = $"NomeTitular;Saldo;\r\n {Conta.NomeTitular};{Conta.Saldo};";
                requisicao.Conta = Conta;

                return requisicao;
            }

            return Next.Execute(requisicao);
        }
    }

}