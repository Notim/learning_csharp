using Core.Interface.GenericChainRepository;
using Core.Model.Financeiro.Wrappers;

namespace Core.Services.Financeiro.Service.ConversorDocumento.FormatosDocumentoChain.Vendors {

    internal class UknownConverterChainNode : DocFormatNodeAbstract, IChainNode<RequisicaoAgregate> {

        public override RequisicaoAgregate Execute(RequisicaoAgregate contracted) {

            contracted.Resposta = "Uknown Format";

            return contracted;
        }
    }

}