using Core.Interface;
using Core.Model.Financeiro.entities;
using Core.Model.Financeiro.enums;

namespace Core.Model.Financeiro.Wrappers {

    public class RequisicaoAgregate : IAggregateRoot {

        public Conta Conta { get; set; }

        public string Resposta { get; set; }

        public FormatoDocumentoEnum Formato { get; set; } = FormatoDocumentoEnum.Uknown;

        public RequisicaoAgregate() {
            this.Conta = new Conta();
        }
    }

}