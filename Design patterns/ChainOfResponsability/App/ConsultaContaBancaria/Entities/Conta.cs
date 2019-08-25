namespace App.ConversorDocumentos.Entities {

    public class Conta { // possui apenas saldo e nome do titular:
        public decimal Saldo       { get; set; }
        public string  NomeTitular { get; set; }
        public string  Resposta    { get; set; }
    }

}