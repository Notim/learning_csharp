using App.ConversorDocumentos.Entities;

using Core.GenericChainRepository;

namespace App.ConsultaContaBancaria.Services.ConversorDocumento.FormatosDocumentoChain {

    public abstract class DocFormatNodeAbstract : IChainNode<Requisicao> {
        protected DocFormatNodeAbstract() {

            Conta = new Conta {
                Saldo = (decimal) 55.00, 
                NomeTitular = "Joao vitor paulino martins"
            };
        }

        protected readonly Conta Conta;

        public IChainNode<Requisicao> Next { get; set; }

        public abstract Requisicao Execute(Requisicao contracted);
    }

}