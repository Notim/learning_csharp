using System;
using System.Data.Entity;
using System.Linq;

using BLL.Arquivos;
using BLL.ProcessosAvaliacoes;
using BLL.Services;

using DAL.Arquivos;
using DAL.Arquivos.Extensions;
using DAL.ProcessosAvaliacoes;

using WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos;
using WEB.Areas.Api.ProcessosAvaliacoes.Models.Forms;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.Fillers {

    public class InscricaoEtapaDetalhesFiller {

        private InscricaoEtapaDetalhesForm Form { get; set; }
        
        private readonly IProcessoAvaliacaoTrabalhoConsultaBL       ConsultaTrabalho;
        private readonly IProcessoAvaliacaoInscricaoEtapaConsultaBL ConsultaInscricaoEtapa;
        
        private readonly IArquivoUploadBL ArquivoUploadBl;

        public InscricaoEtapaDetalhesFiller(IProcessoAvaliacaoTrabalhoConsultaBL       _IProcessoAvaliacaoTrabalhoConsultaBL,
                                            IProcessoAvaliacaoInscricaoEtapaConsultaBL _IProcessoAvaliacaoInscricaoEtapaConsultaBL,
                                            IArquivoUploadBL                           _IArquivoUploadBL) {
            
            ConsultaTrabalho                        = _IProcessoAvaliacaoTrabalhoConsultaBL;
            ConsultaInscricaoEtapa                  = _IProcessoAvaliacaoInscricaoEtapaConsultaBL;
            ArquivoUploadBl                         = _IArquivoUploadBL;
        }

        public InscricaoEtapaDetalhesDTO carregar(InscricaoEtapaDetalhesForm _InscricaoEtapaDetalhesForm) {
            Form = _InscricaoEtapaDetalhesForm;

            var query = montarQuery();
            
            var inscricaoEtapa = carregarInscricaoEtapa(query);

            return inscricaoEtapa;
        }

        internal IQueryable<ProcessoAvaliacaoInscricaoEtapa> montarQuery() { // Realiza os filtros de busca e restrição corretamente
            var query = this.ConsultaInscricaoEtapa
                            .query(Form.idOrganizacao)
                            .Where(x => x.idProcessoAvaliacao == Form.idProcessoAvaliacao &&
                                        x.idEtapa == Form.idEtapa &&
                                        x.idInscrito == Form.idInscricao 
                            );
            
            return query;
        }
        
        internal InscricaoEtapaDetalhesDTO carregarInscricaoEtapa(IQueryable<ProcessoAvaliacaoInscricaoEtapa> query) {
            
            var InscricaoDetalhe = query.Select(x => new InscricaoEtapaDetalhesDTO {
                                              id            =  x.id,
                                              idIncricao    = x.idInscrito,
                                              nomeInscrito  = x.ProcessoAvaliacaoInscricao.nomeInscrito,
                                              dtAprovacao   = x.dtAprovacao,
                                              flagAprovado  = x.flagAprovado,
                                              dtReprovacao  = x.dtReprovacao,
                                              dtFinalizacao = x.dtFinalizacao,
                                              dtFinalizacaoAvaliacao  = x.dtFinalizacaoAvaliacao,
                                              dtFinalizacaoPreAnalise = x.dtFinalizacaoPreAnalise,
                                          }).FirstOrDefault()
                                          .ToJsonObject<InscricaoEtapaDetalhesDTO>() ?? new InscricaoEtapaDetalhesDTO();

            if (InscricaoDetalhe.id <= 0) {
                return InscricaoDetalhe;
            }
                
            var Trabalho = this.ConsultaTrabalho
                               .query(Form.idOrganizacao)
                               .Select(x => new {
                                    x.idEtapa,
                                    x.idProcessoAvaliacao,
                                    x.idInscricao,
                                    x.titulo,
                                    x.nomeInstituicao,
                                    AreaConhecimento = new {x.AreaConhecimento.descricao},
                                    x.objetivo,
                                    x.idArquivoTrabalho,
                                    x.metodo,
                                    x.conclusao,
                                    x.referencia,
                               }).FirstOrDefault(x => x.idEtapa == Form.idEtapa && x.idProcessoAvaliacao == Form.idProcessoAvaliacao 
                                                                                && x.idInscricao == Form.idInscricao)
                               .ToJsonObject<ProcessoAvaliacaoTrabalho>() ?? new ProcessoAvaliacaoTrabalho();
            
            var Arquivo = this.ArquivoUploadBl
                               .listar(0,"","","")
                               .FirstOrDefault(x => x.id == Trabalho.idArquivoTrabalho)
                               .ToJsonObject<ArquivoUpload>() ?? new ArquivoUpload();
            
            InscricaoDetalhe.tituloTrabalho      = Trabalho.titulo;
            InscricaoDetalhe.nomeInstituicao     = Trabalho.nomeInstituicao;
            InscricaoDetalhe.areaConhecimento    = Trabalho.AreaConhecimento.descricao;
            InscricaoDetalhe.objetivo            = Trabalho.objetivo;
            InscricaoDetalhe.metodo              = Trabalho.metodo;
            InscricaoDetalhe.conclusao           = Trabalho.conclusao;
            InscricaoDetalhe.referencia          = Trabalho.referencia;
            InscricaoDetalhe.linkArquivoTrabalho = Arquivo.linkArquivo().isEmpty() ? Arquivo.linkFisico() : Arquivo.linkArquivo();
            
            return InscricaoDetalhe;
        }
    }

}