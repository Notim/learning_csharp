using Core.SharedKernel;

namespace Core.Model.Financeiro.entities {

    public class Conta : BaseEntity {

        public int idPessoaTitular { get; set; }

        public string nroConta { get; set; }

        public string nroAgencia { get; set; }

        public decimal Saldo { get; set; }

        public string NomeTitular { get; set; }

    }

}