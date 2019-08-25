using System;

using Core.Model.Pedidos.DTOS;
using Core.Model.Pedidos.extensions;

namespace Core.Services.Pedidos.Service.CalculadoraDescontos.DescontosChain.Vendors {

    public class DefaulDescontoChainNode : DescontoChainNodeAbstract {

        public override OrcamentoDTO Execute(OrcamentoDTO contracted) {
            contracted.AdicionarDesconto("Aplicaçao de descontos finalizada");

            return contracted;
        }
    }

}