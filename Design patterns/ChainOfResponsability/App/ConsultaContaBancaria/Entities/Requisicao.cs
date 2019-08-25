namespace App.ConversorDocumentos.Entities {

    public class Requisicao {
        public Conta Conta { get; set; }

        public FormatoDocumentoEnum Formato { get; set; } = FormatoDocumentoEnum.Uknown;

        public Requisicao() {
            this.Conta = new Conta();
        }
    }

}