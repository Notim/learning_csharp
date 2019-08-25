using System.Collections.Generic;

namespace Core.Model.Pedidos.DTOS {

    public sealed class OrcamentoDTO {

        public double Descontos { get; set; }

        public double Valor { get; set; }

        public IList<ItemDTO> listItens { get; set; }

        public IList<string> listDescontosAplicados { get; set; }

        public OrcamentoDTO() {
            this.listItens = new List<ItemDTO>();

            this.listDescontosAplicados = new List<string>();
        }
    }

}