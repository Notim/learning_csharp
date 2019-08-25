using System;
using System.Linq;

using Core.Model.Pedidos.DTOS;
using Core.Model.Pedidos.extensions;

namespace Core.Services.Pedidos.Service.CalculadoraDescontos.DescontosChain.Vendors {

    public class DescontoVendaCasada : DescontoChainNodeAbstract {
        public override OrcamentoDTO Execute(OrcamentoDTO orcamento) {

            if (Existe("LAPIS", orcamento) && Existe("CANETA", orcamento)) {
                orcamento.Descontos += (double) (orcamento.GetValorTotal() * (decimal) 0.05);
                Console.WriteLine("Desconto de 5% sobre Venda casada com LAPIS e CANETA aplicado sobre: " + orcamento.Valor);
            }

            return Next.Execute(orcamento);
        }

        private static bool Existe(string nomeDoItem, OrcamentoDTO orcamento) {
            return orcamento.listItens.Any(item => item.Nome.Equals(nomeDoItem));
        }
    }

}