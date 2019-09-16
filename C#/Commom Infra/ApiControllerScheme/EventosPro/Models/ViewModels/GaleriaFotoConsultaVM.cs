using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL.Arquivos;
using BLL.EventosPro;
using BLL.Hotsites;
using BLL.Publicacoes;
using DAL.Arquivos.Extensions;
using DAL.Entities;
using DAL.Publicacoes;
using PagedList;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.ViewModels {

    public class GaleriaFotoConsultaVM {

        //Atributos
        private IGaleriaFotoBL _GaleriaFotoBL;
        private IHotsiteConsultaBL _HotsiteConsultaBL;
        private IEventoConsultaBL _EventoConsultaBL;
        private IArquivoUploadFotoBL _ArquivoUploadFotoBL;

        //Propriedades
		private IGaleriaFotoBL OGaleriaFotoBL => _GaleriaFotoBL = _GaleriaFotoBL ?? new GaleriaFotoBL();
		private IHotsiteConsultaBL OHotsiteConsultaBL => _HotsiteConsultaBL = _HotsiteConsultaBL ?? new HotsiteConsultaBL();
		private IEventoConsultaBL OEventoConsultaBL => _EventoConsultaBL = _EventoConsultaBL ?? new EventoConsultaBL();
        private IArquivoUploadFotoBL OArquivoUploadFotoBL => _ArquivoUploadFotoBL = _ArquivoUploadFotoBL ?? new ArquivoUploadFotoBL();

        public DefaultDTO carregar() {

            int idHotsite = UtilRequest.getInt32("idHotsite");

            int idOrganizacao = CustomExtensions.getIdOrganizacao();

            var idEventoGaleriaFoto = OHotsiteConsultaBL.query(idOrganizacao).Where(x => x.id == idHotsite).Select(x => x.idEventoGaleriaFoto)
                .FirstOrDefault();
            
            var idGaleriaFoto = OEventoConsultaBL.query(idOrganizacao).Where(x => x.id == idEventoGaleriaFoto).Select(x => x.idGaleriaFoto)
                .FirstOrDefault();

            var OGaleriaFoto = this.OGaleriaFotoBL.query(idOrganizacao).FirstOrDefault(x => x.id == idGaleriaFoto && x.ativo == "S");

            if (OGaleriaFoto == null) {

                var ORetorno = new DefaultDTO {
                    flagErro = true,
                    listaMensagens = new List<string> { "Nenhuma galeria encontrada." }
                };

                return ORetorno;

            }

            var ORetornoMontado = this.montarRetorno(OGaleriaFoto);

            return ORetornoMontado;

        }

        //
        private DefaultDTO montarRetorno(GaleriaFoto OGaleria) {

            var listaFotos = this.OArquivoUploadFotoBL.listar(OGaleria.id, EntityTypes.GALERIAFOTO, "S")
                                 .OrderBy(x => x.ordem).ThenBy(x => x.id).ToList();

            var listaFotosPaginacao = listaFotos.ToPagedList(UtilRequest.getNroPagina(), UtilRequest.getNroRegistros());

            var listaFotosRetorno = new List<object>();
            
            foreach (var OFoto in listaFotosPaginacao) {
                var DadosFoto = new {
                    OFoto.id,
                    OFoto.legenda,
                    urlFotoOriginal = OFoto.linkImagem(),
                    urlFotoThumb = OFoto.linkImagem("home")
                };

                listaFotosRetorno.Add(DadosFoto);
            }

            var Dados = new {
                OGaleria.id,
                OGaleria.idTipoGaleria,
                OGaleria.titulo,
                OGaleria.chamada,
                OGaleria.descricao,
                OGaleria.dtGaleria,
                OGaleria.dtCadastro,
                listaFotos = listaFotosRetorno
            };
            
            var ORetorno = new DefaultDTO();
            ORetorno.carregarDadosPaginacao(listaFotosPaginacao);
            ORetorno.flagErro = false;
            ORetorno.listaResultados = Dados;

            return ORetorno;

        }
	}
    
}
