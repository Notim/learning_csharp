using Core.Interface.GenericChainRepository;
using Core.Model.Financeiro.Wrappers;
using Core.Services.Financeiro.Service.ConversorDocumento.FormatosDocumentoChain.Vendors;

namespace Core.Services.Financeiro.Service {

    public static class FormatadorResposta {
        public static RequisicaoAgregate Formatar(RequisicaoAgregate requisicao) {
            var retorno = new ChainRepository<RequisicaoAgregate>(requisicao)
                                .Next(new CSVConverterChainNode())
                                .Next(new XMLConverterChainNode())
                                .Next(new PorCentoConverterChainNode())
                                .Next(new JsonConverterChainNode())
                                .Next(new PipeConverterChainNode())
                                .Finish(new UknownConverterChainNode())
                                .Run();

            return retorno;
        }
    }

}