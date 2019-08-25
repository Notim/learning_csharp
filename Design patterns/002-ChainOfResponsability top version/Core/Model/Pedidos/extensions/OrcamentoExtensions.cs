using System.Linq;

using Core.Model.Pedidos.DTOS;

namespace Core.Model.Pedidos.extensions {

    public static class OrcamentoExtensions {
        public static OrcamentoDTO AdicionarItem(this OrcamentoDTO orcamento, ItemDTO item) {
            orcamento.listItens.Add(item);

            return orcamento;
        }

        public static OrcamentoDTO AdicionarDesconto(this OrcamentoDTO orcamento, string desconto) {
            orcamento.listDescontosAplicados.Add(desconto);

            return orcamento;
        }

        public static decimal GetValorTotal(this OrcamentoDTO orcamento) {

            return (decimal) orcamento.listItens.Sum(item => item.Valor);
        }
    }

}