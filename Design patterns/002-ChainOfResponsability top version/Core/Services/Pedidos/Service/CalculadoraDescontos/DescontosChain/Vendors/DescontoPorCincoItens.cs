using System;

using Core.Model.Pedidos.DTOS;
using Core.Model.Pedidos.extensions;

namespace Core.Services.Pedidos.Service.CalculadoraDescontos.DescontosChain.Vendors {

    public class DescontoPorCincoItens : DescontoChainNodeAbstract {
        public override OrcamentoDTO Execute(OrcamentoDTO orcamento) {
            if (orcamento.listItens.Count > 5) {
                orcamento.Descontos += (double) (orcamento.GetValorTotal() * (decimal) 0.1);
                orcamento.AdicionarDesconto("Desconto de 10% sobre 5 itens Aplicada " + orcamento.GetValorTotal());
            }

            return Next.Execute(orcamento);
        }
    }

}