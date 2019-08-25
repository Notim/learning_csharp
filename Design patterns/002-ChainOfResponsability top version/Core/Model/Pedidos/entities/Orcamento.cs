using System;

using Core.SharedKernel;

namespace Core.Model.Pedidos.entities {

    public class Orcamento : BaseEntity {

        public double Descontos { get; set; }

        public double Valor { get; set; }

        public DateTime? dataEfetuacao { get; set; }

        public string descricao { get; set; }

        public string observacoes { get; set; }

        public byte statusOrcamento { get; set; }

        public DateTime dtValidadeOrcamento { get; set; }

    }

}