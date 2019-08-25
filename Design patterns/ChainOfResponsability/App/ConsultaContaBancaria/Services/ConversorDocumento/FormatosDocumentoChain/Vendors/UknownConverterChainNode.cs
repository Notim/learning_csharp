using App.ConversorDocumentos.Entities;

using Core.GenericChainRepository;

namespace App.ConsultaContaBancaria.Services.ConversorDocumento.FormatosDocumentoChain.Vendors {

    internal class UknownConverterChainNode : DocFormatNodeAbstract, IChainNode<Requisicao> {

        public override Requisicao Execute(Requisicao contracted) {

            Conta.Resposta = "Uknown Format";

            contracted.Conta = Conta;

            return contracted;
        }
    }

}