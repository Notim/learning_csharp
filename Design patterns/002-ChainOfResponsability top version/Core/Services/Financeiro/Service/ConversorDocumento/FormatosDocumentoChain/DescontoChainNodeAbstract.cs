using Core.Interface.GenericChainRepository;
using Core.Model.Financeiro.entities;
using Core.Model.Financeiro.Wrappers;

namespace Core.Services.Financeiro.Service.ConversorDocumento.FormatosDocumentoChain {

    public abstract class DocFormatNodeAbstract : IChainNode<RequisicaoAgregate> {
        
        protected DocFormatNodeAbstract() {
            Conta = new Conta {
                Saldo = (decimal) 55.00, 
                NomeTitular = "Joao vitor paulino martins"
            };
        }

        protected readonly Conta Conta;

        public IChainNode<RequisicaoAgregate> Next { get; set; }

        public abstract RequisicaoAgregate Execute(RequisicaoAgregate contracted);
    }

}