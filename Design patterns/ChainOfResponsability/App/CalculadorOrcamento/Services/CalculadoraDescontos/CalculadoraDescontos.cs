using System;
using System.Collections.Generic;

using App.CalculadorOrcamento.Services.CalculadoraDescontos.DescontosChain.Vendors;

using CalculadorDescontos.CalculadorOrcamento.Entities;

using Core.GenericChainRepository;

namespace App.CalculadorOrcamento.Services.CalculadoraDescontos {

    public class CalculadoraDescontos {

        private readonly IChainRepository<Orcamento> chainDiscountRepository;

        public CalculadoraDescontos(Orcamento orcamento) {
            chainDiscountRepository = new ChainRepository<Orcamento>(orcamento);
        }

        public Orcamento CalcularDescontos() {
            return this.chainDiscountRepository
                       .Next(new DescontoPorMaisDeQuinhentosReais())
                       .AddNodes(
                           new List<IChainNode<Orcamento>> {
                              new DescontoPorCincoItens(),
                              new DescontoDezMaisDeCinco(),
                              new DescontoVendaCasada()
                           }
                       ).Next(DescontoAcimaDeMilReais)
                       .Next(DescontosSimples.DescontoAcimaDeDoisMilReais)
                       .Next(
                           chainExpr: delegate(Orcamento Orcamento) {
                               Console.WriteLine("Aplicando desconto de expr anonima");
                               if (Orcamento.Valor > 0.0) {
                                   Orcamento.Descontos += Orcamento.Valor * 1;
                               }
                               return Orcamento;
                           }
                       )
                       .Finish(new DefaulDescontoChainNode())
                       .Run();
        }

        public static Orcamento DescontoAcimaDeMilReais(Orcamento orcamento) {
            Console.WriteLine("Desconto de 20% sobre Venda Acima de R$ 1000 aplicado sobre: " + orcamento.Valor);
            if (orcamento.Valor > 1000.0) {
                orcamento.Descontos += orcamento.Valor * 0.2;
            }
            
            return orcamento;
        }
    }
    public static partial class DescontosSimples {
        public static Orcamento DescontoAcimaDeDoisMilReais(Orcamento orcamento) {
            Console.WriteLine("Desconto de 40% sobre Venda Acima de R$ 2000 aplicado sobre: " + orcamento.Valor);
            if (orcamento.Valor > 2000.0) {
                orcamento.Descontos += orcamento.Valor * 0.4;
            }
                
            return orcamento;
        }
    }
}