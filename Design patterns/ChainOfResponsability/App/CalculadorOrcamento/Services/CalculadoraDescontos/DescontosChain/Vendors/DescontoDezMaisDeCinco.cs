using System;

using CalculadorDescontos.CalculadorOrcamento.Entities;

namespace App.CalculadorOrcamento.Services.CalculadoraDescontos.DescontosChain.Vendors {

    public class DescontoDezMaisDeCinco : DescontoChainNodeAbstract {

        public override Orcamento Execute(Orcamento contracted) {
            if (contracted.Itens.Count > 5) {
                contracted.Descontos += (contracted.Valor * 0.10);
                Console.WriteLine("Desconto de 10% com mais de 5 Itens Aplicado sobre: " + contracted.Valor);
            }

            return Next.Execute(contracted);
        }
    }

}