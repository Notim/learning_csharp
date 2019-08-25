using App.ConsultaContaBancaria.Services.ConversorDocumento.FormatosDocumentoChain.Vendors;
using App.ConversorDocumentos.Entities;

using Core.GenericChainRepository;

namespace App.ConsultaContaBancaria.Services {

    public static class ContaBancariaConsulta {
        public static Conta Consultar(Requisicao requisicao) {
            var retorno = new ChainRepository<Requisicao>(requisicao)
                                 .Next(new CSVConverterChainNode())
                                 .Next(new XMLConverterChainNode())
                                 .Next(new PorCentoConverterChainNode())
                                 .Next(new JsonConverterChainNode())
                                 .Next(new PipeConverterChainNode())
                                 .Finish(new UknownConverterChainNode())
                                 .Run();

            return retorno.Conta;
        }
    }

}