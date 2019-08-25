using System;

using App.ConversorDocumentos.Entities;

using Core.GenericChainRepository;

namespace App.ConsultaContaBancaria.Services.ConversorDocumento.FormatosDocumentoChain.Vendors {

    internal class XMLConverterChainNode : DocFormatNodeAbstract, IChainNode<Requisicao> {

        public override Requisicao Execute(Requisicao requisicao) {
            if ((byte) requisicao.Formato == (byte) FormatoDocumentoEnum.XML) {
                
                Console.WriteLine("Requisicao efetuada em formato XML");

                Conta.Resposta = $"<Conta>\r\n\t<NomeTitular>{this.Conta.NomeTitular}</NomeTitular>\r\n\t<Saldo>{this.Conta.Saldo}</Saldo>\r\n<Conta>";
                requisicao.Conta = Conta;

                return requisicao;
            }

            return Next.Execute(requisicao);
        }
    }
}