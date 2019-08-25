using System;

using Core.Interface.GenericChainRepository;
using Core.Model.Financeiro.enums;
using Core.Model.Financeiro.Wrappers;

namespace Core.Services.Financeiro.Service.ConversorDocumento.FormatosDocumentoChain.Vendors {

    internal class PipeConverterChainNode : DocFormatNodeAbstract, IChainNode<RequisicaoAgregate> {

        public override RequisicaoAgregate Execute(RequisicaoAgregate requisicao) {
            if ((byte) requisicao.Formato == (byte) FormatoDocumentoEnum.PIPE) {
                Console.WriteLine("Requisicao efetuada em formato PIPE");

                requisicao.Resposta = $"NomeTitular|Saldo|\r\n {Conta.NomeTitular}|{Conta.Saldo}|";

                return requisicao;
            }

            return Next.Execute(requisicao);
        }
    }

}