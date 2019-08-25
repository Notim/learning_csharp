using System;

using CalculadorDescontos.CalculadorOrcamento.Entities;

namespace App.CalculadorOrcamento.Services.CalculadoraDescontos.DescontosChain.Vendors {

    public class DefaulDescontoChainNode : DescontoChainNodeAbstract {

        public override Orcamento Execute(Orcamento contracted) {
            Console.WriteLine("Aplicaçao de descontos finalizada");
                
            return contracted;
        }
    }

}