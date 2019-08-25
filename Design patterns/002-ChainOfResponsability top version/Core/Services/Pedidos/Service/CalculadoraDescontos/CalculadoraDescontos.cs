using System;
using System.Collections.Generic;

using Core.Interface.GenericChainRepository;
using Core.Model.Pedidos.DTOS;
using Core.Model.Pedidos.extensions;
using Core.Services.Pedidos.Service.CalculadoraDescontos.DescontosChain.Vendors;

namespace Core.Services.Pedidos.Service.CalculadoraDescontos {

    public class CalculadoraDescontos {

        private readonly IChainRepository<OrcamentoDTO> chainDiscountRepository;

        public CalculadoraDescontos(OrcamentoDTO orcamento) {
            chainDiscountRepository = new ChainRepository<OrcamentoDTO>(orcamento);
        }

        public OrcamentoDTO CalcularDescontos() {
            return this.chainDiscountRepository
                       .Next(new DescontoPorMaisDeQuinhentosReais())
                       .AddNodes(
                           new List<IChainNode<OrcamentoDTO>> {
                              new DescontoPorCincoItens(),
                              new DescontoDezMaisDeCinco(),
                              new DescontoVendaCasada()
                           }
                       ).Next(DescontoAcimaDeMilReais)
                       .Next(DescontosSimples.DescontoAcimaDeDoisMilReais)
                       .Next(
                           chainExpr: delegate(OrcamentoDTO Orcamento) {
                               Console.WriteLine("Aplicando desconto de expr anonima");
                               if (Orcamento.Valor > 0.0) {
                                   Orcamento.Descontos += (double) (Orcamento.GetValorTotal() * 1);
                               }
                               return Orcamento;
                           }
                       )
                       .Finish(new DefaulDescontoChainNode())
                       .Run();
        }

        public static OrcamentoDTO DescontoAcimaDeMilReais(OrcamentoDTO orcamento) {
            Console.WriteLine("Desconto de 20% sobre Venda Acima de R$ 1000 aplicado sobre: " + orcamento.Valor);
            if (orcamento.Valor > 1000.0) {
                orcamento.Descontos += orcamento.Valor * 0.2;
            }
            
            return orcamento;
        }
    }
    public static partial class DescontosSimples {
        public static OrcamentoDTO DescontoAcimaDeDoisMilReais(OrcamentoDTO orcamento) {
            Console.WriteLine("Desconto de 40% sobre Venda Acima de R$ 2000 aplicado sobre: " + orcamento.Valor);
            if (orcamento.Valor > 2000.0) {
                orcamento.Descontos += (double) (orcamento.GetValorTotal() * (decimal) 0.4);
            }
                
            return orcamento;
        }
    }
}