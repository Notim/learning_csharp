using System.Collections.Generic;

using PagedList;

namespace WEB.AppInfrastructure.Api {

    public class DefaultDTO {

        public object listaResultados { get; set; }

        public int totalRegistros { get; set; }

        public int totalRegistrosPagina { get; set; }

        public int paginaAtual { get; set; }

        public int totalPaginas { get; set; }

        public IList<string> listaMensagens { get; set; }

        public bool flagErro { get; set; }

        public DefaultDTO() {
            this.listaMensagens       = new List<string>();
            this.paginaAtual          = 1;
            this.totalRegistrosPagina = 20;
        }

        public void carregarDadosPaginacao(IPagedList<object> listaPaginada) {

            this.totalRegistros       = listaPaginada.TotalItemCount;
            this.totalRegistrosPagina = listaPaginada.PageSize;
            this.paginaAtual          = listaPaginada.PageNumber;
            this.totalPaginas         = listaPaginada.PageCount;

        }
    }

}