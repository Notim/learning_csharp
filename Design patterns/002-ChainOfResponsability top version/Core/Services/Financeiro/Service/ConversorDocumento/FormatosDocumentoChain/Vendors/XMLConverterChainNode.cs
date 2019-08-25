using System;

using Core.Interface.GenericChainRepository;
using Core.Model.Financeiro.enums;
using Core.Model.Financeiro.Wrappers;

namespace Core.Services.Financeiro.Service.ConversorDocumento.FormatosDocumentoChain.Vendors {

    internal class XMLConverterChainNode : DocFormatNodeAbstract, IChainNode<RequisicaoAgregate> {

        public override RequisicaoAgregate Execute(RequisicaoAgregate requisicao) {
            if ((byte) requisicao.Formato == (byte) FormatoDocumentoEnum.XML) {

                Console.WriteLine("Requisicao efetuada em formato XML");

                requisicao.Resposta = $"<Conta>\r\n\t<NomeTitular>{this.Conta.NomeTitular}</NomeTitular>\r\n\t<Saldo>{this.Conta.Saldo}</Saldo>\r\n<Conta>";

                return requisicao;
            }

            return Next.Execute(requisicao);
        }
    }

}