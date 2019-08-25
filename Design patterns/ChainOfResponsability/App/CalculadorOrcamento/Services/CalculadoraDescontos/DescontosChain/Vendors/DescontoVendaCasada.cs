using System;
using System.Linq;

using CalculadorDescontos.CalculadorOrcamento.Entities;

namespace App.CalculadorOrcamento.Services.CalculadoraDescontos.DescontosChain.Vendors {

    public class DescontoVendaCasada : DescontoChainNodeAbstract {
        public override Orcamento Execute(Orcamento orcamento) {
            
            if (Existe("LAPIS", orcamento) && Existe("CANETA", orcamento)) {
                orcamento.Descontos += orcamento.Valor * 0.05;
                Console.WriteLine("Desconto de 5% sobre Venda casada com LAPIS e CANETA aplicado sobre: " + orcamento.Valor);
            }

            return Next.Execute(orcamento);
        }

        private static bool Existe(string nomeDoItem, Orcamento orcamento) {
            return orcamento.Itens.Any(item => item.Nome.Equals(nomeDoItem));
        }
    }

}