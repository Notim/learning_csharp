using System;

using Core.Model.Pedidos.DTOS;
using Core.Model.Pedidos.extensions;
using Core.Services.Pedidos.Service.CalculadoraDescontos;

namespace App {

    public class OrcamentoProgram {
        public void Run() {

            var orcamento = new OrcamentoDTO();
            orcamento.AdicionarItem(new ItemDTO {Nome = "CANETA", Valor    = 250.0});
            orcamento.AdicionarItem(new ItemDTO {Nome = "LAPIS", Valor     = 250.0});
            orcamento.AdicionarItem(new ItemDTO {Nome = "Fogao", Valor     = 250.0});
            orcamento.AdicionarItem(new ItemDTO {Nome = "Geladeira", Valor = 250.0});
            orcamento.AdicionarItem(new ItemDTO {Nome = "Sofa", Valor      = 250.0});
            orcamento.AdicionarItem(new ItemDTO {Nome = "Compiuter", Valor = 250.0});
            orcamento.AdicionarItem(new ItemDTO {Nome = "Teste", Valor     = 250.0});

            var CalculadorDescontos = new CalculadoraDescontos(orcamento);

            orcamento = CalculadorDescontos.CalcularDescontos();

            Console.WriteLine("Valor Compra: R$" + orcamento.Valor);
            Console.WriteLine("Descontos: R$"    + orcamento.Descontos);
            Console.WriteLine("Total: R$"        + (orcamento.Valor - orcamento.Descontos));
        }
    }

}