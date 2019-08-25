using System;

using CalculadorDescontos.CalculadorOrcamento.Entities;

namespace App.CalculadorOrcamento.Services.CalculadoraDescontos.DescontosChain.Vendors {

    public class DescontoPorCincoItens : DescontoChainNodeAbstract {
        public override Orcamento Execute(Orcamento orcamento) {
            if (orcamento.Itens.Count > 5) {
                orcamento.Descontos += orcamento.Valor * 0.1;
                Console.WriteLine("Desconto de 10% sobre 5 itens Aplicada " + orcamento.Valor);
            }

            return Next.Execute(orcamento);
        }
    }

}