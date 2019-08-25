using System;

using Core.Model.Pedidos.DTOS;
using Core.Model.Pedidos.extensions;

namespace Core.Services.Pedidos.Service.CalculadoraDescontos.DescontosChain.Vendors {

    public class DescontoDezMaisDeCinco : DescontoChainNodeAbstract {

        public override OrcamentoDTO Execute(OrcamentoDTO contracted) {
            if (contracted.listItens.Count > 5) {
                contracted.Descontos += (double) (contracted.GetValorTotal() * (decimal) 0.10);
                contracted.AdicionarDesconto("Desconto de 10% com mais de 5 Itens Aplicado sobre: " + contracted.GetValorTotal());
            }

            return Next.Execute(contracted);
        }
    }

}