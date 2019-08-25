using CalculadorDescontos.CalculadorOrcamento.Entities;

using Core.GenericChainRepository;

namespace App.CalculadorOrcamento.Services.CalculadoraDescontos.DescontosChain {

    public abstract class DescontoChainNodeAbstract : IChainNode<Orcamento> {

        public IChainNode<Orcamento> Next { get; set; }

        public abstract Orcamento Execute(Orcamento contracted);
    }

}