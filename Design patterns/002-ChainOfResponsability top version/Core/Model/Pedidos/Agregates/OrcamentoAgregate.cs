using System.Collections.Generic;

using Core.Interface;
using Core.Model.Financeiro.entities;
using Core.Model.Pedidos.entities;
using Core.Model.Pedidos.enums;

namespace Core.Model.Pedidos.Agregates {

    public class OrcamentoAgregate : IAggregateRoot {

        public Conta Conta { get; private set; }

        public Orcamento Orcamento { get; private set; }

        public IList<Item> listaItens { get; }

        public StatusOrcamento statusOrcamento { get; private set; }

        public OrcamentoAgregate(Orcamento orcamento) {
            this.Orcamento = orcamento;

            this.listaItens = new List<Item>();
        }

        public OrcamentoAgregate AdicionarItem(OrcamentoAgregate agregate, Item item) {
            agregate.listaItens.Add(item);

            return this;
        }

        public OrcamentoAgregate AlterarStatus(OrcamentoAgregate agregate, StatusOrcamento status) {
            agregate.statusOrcamento = status;

            return this;
        }
    }

}