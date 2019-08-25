using Core.Interface.GenericChainRepository;
using Core.Model.Pedidos.DTOS;

namespace Core.Services.Pedidos.Service.CalculadoraDescontos.DescontosChain {

    public abstract class DescontoChainNodeAbstract : IChainNode<OrcamentoDTO> {

        public IChainNode<OrcamentoDTO> Next { get; set; }

        public abstract OrcamentoDTO Execute(OrcamentoDTO contracted);
    }

}