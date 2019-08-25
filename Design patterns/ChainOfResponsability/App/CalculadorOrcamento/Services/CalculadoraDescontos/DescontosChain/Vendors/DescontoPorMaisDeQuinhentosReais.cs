using System;

using CalculadorDescontos.CalculadorOrcamento.Entities;

namespace App.CalculadorOrcamento.Services.CalculadoraDescontos.DescontosChain.Vendors {

    public class DescontoPorMaisDeQuinhentosReais : DescontoChainNodeAbstract {

        public override Orcamento Execute(Orcamento contracted) {
            if (contracted.Valor > 500) {
                Console.WriteLine("Desconto de 7% com mais de R$500 Aplicado sobre: " + contracted.Valor);
                contracted.Descontos += contracted.Valor * 0.07;
            }

            return Next.Execute(contracted);
        }
    }

}