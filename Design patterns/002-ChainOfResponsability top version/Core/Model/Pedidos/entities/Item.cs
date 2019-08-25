using Core.SharedKernel;

namespace Core.Model.Pedidos.entities {

    public class Item : BaseEntity {
        public string Nome { get; set; }

        public double Valor { get; set; }

        public int IdFornecedor { get; set; }

        public int IdCategoriaProduto { get; set; }

        public int IdFotoProduto { get; set; }

        public string descricao { get; set; }

        public string observacoes { get; set; }

    }

}