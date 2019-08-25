using System;

using Core.Model.Pedidos.DTOS;
using Core.Model.Pedidos.extensions;

namespace Core.Services.Pedidos.Service.CalculadoraDescontos.DescontosChain.Vendors {

    public class DescontoPorMaisDeQuinhentosReais : DescontoChainNodeAbstract {

        public override OrcamentoDTO Execute(OrcamentoDTO contracted) {
            if (contracted.Valor > 500) {
                contracted.Descontos += (double) (contracted.GetValorTotal() * (decimal) 0.07);
                contracted.AdicionarDesconto("Desconto de 7% com mais de R$500 Aplicado sobre: " + contracted.GetValorTotal());
            }

            return Next.Execute(contracted);
        }
    }

}