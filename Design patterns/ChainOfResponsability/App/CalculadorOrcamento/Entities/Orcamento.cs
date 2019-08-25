using System.Collections.Generic;
using System.Linq;

namespace CalculadorDescontos.CalculadorOrcamento.Entities {

    public class Orcamento {

        public double Valor {
            get { return Itens.Sum(c => c.Valor); }
        }

        public double      Descontos { get; set; }
        public IList<Item> Itens     { get; set; }

        public Orcamento() {
            this.Itens = new List<Item>();
        }

        public Orcamento AdicionaItem(Item item) {
            Itens.Add(item);

            return this;
        }
    }

}