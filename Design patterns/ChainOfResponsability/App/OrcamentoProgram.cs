using System;

using App.CalculadorOrcamento.Services.CalculadoraDescontos;

using CalculadorDescontos.CalculadorOrcamento.Entities;
namespace App {

    public class OrcamentoProgram {
        public void Run() {
            var orcamento = new Orcamento();
            orcamento.AdicionaItem(new Item("CANETA",    250.0));
            orcamento.AdicionaItem(new Item("LAPIS",     250.0));
            orcamento.AdicionaItem(new Item("Fogao",     250.0));
            orcamento.AdicionaItem(new Item("Geladeira", 250.0));
            orcamento.AdicionaItem(new Item("Sofa",      250.0));
            orcamento.AdicionaItem(new Item("Compiuter", 250.0));
            orcamento.AdicionaItem(new Item("Teste",     250.0));

            var CalculadorDescontos = new CalculadoraDescontos(orcamento);

            orcamento = CalculadorDescontos.CalcularDescontos();

            Console.WriteLine("Valor Compra: R$" + orcamento.Valor);
            Console.WriteLine("Descontos: R$"    + orcamento.Descontos);
            Console.WriteLine("Total: R$"        + (orcamento.Valor - orcamento.Descontos));
        }
    }

}