using System;

using Core.Interface.GenericChainRepository;
using Core.Model.Financeiro.enums;
using Core.Model.Financeiro.Wrappers;

namespace Core.Services.Financeiro.Service.ConversorDocumento.FormatosDocumentoChain.Vendors {

    internal class JsonConverterChainNode : DocFormatNodeAbstract, IChainNode<RequisicaoAgregate> {

        public override RequisicaoAgregate Execute(RequisicaoAgregate requisicao) {
            if ((byte) requisicao.Formato == (byte) FormatoDocumentoEnum.JSON) {
                Console.WriteLine("Requisicao efetuada em formato Json");

                requisicao.Resposta = $"{{\r\n\t\"NomeTitular\" : \"{Conta.NomeTitular}\",\r\n\t\"Saldo\" :{Conta.Saldo:C}\r\n}}";

                return requisicao;
            }

            return Next.Execute(requisicao);
        }
    }

}