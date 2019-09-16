using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Arquivos;
using BLL.EventosPro;
using BLL.Pedidos;
using BLL.Services;
using DAL.Arquivos;
using DAL.Arquivos.Extensions;
using DAL.Entities;
using DAL.Eventos;
using DAL.Pedidos;
using DAL.Produtos;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoProdutoListagemVM {
        
        // Atributos
        private IEventoProdutoConsultaBL _IEventoProdutoConsultaBL;
        private IArquivoUploadFotoBL _IArquivoUploadFotoBL;
        private IPedidoProdutoBL _IPedidoProdutoBL;
        
        // Serviços
        private IEventoProdutoConsultaBL OEventoProdutoConsultaBL => _IEventoProdutoConsultaBL = _IEventoProdutoConsultaBL ?? new EventoProdutoConsultaBL();
        private IArquivoUploadFotoBL OArquivoUploadFotoBL => _IArquivoUploadFotoBL = _IArquivoUploadFotoBL ?? new ArquivoUploadFotoBL();
        private IPedidoProdutoBL OPedidoProdutoBL => _IPedidoProdutoBL = _IPedidoProdutoBL ?? new PedidoProdutoBL();

        // Propriedades
        public int idEvento{ get; set; }
        public int idOrganizacao{ get; set; }

        public List<TipoProduto> listaTipoProduto { get; set; }
        public List<EventoProduto> listaEventoProduto { get; set; }
        public List<PedidoProduto> listaPedidosProdutos { get; set; }
        
        public EventoProdutoListagemVM() {
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }

        public DefaultDTO carregar(){

            var ORetorno = new DefaultDTO();
            
            this.carregarProdutos();
            
            this.carregarPedidos();
            
            this.carregarArquivos();
            
            ORetorno.listaResultados = this.listaTipoProduto.Select(this.montarRetorno);
            
            return ORetorno;
        }

        private void carregarPedidos(){

            var idsProdutos = this.listaEventoProduto.Select(x => x.idProduto).ToList();
            
            this.listaPedidosProdutos = this.OPedidoProdutoBL.query()
                .Where(x => idsProdutos.Contains(x.idProduto) && x.Pedido.idStatusPedido != StatusPedidoConst.CANCELADO
                                                     && x.Pedido.idEvento == idEvento
                                                     && x.Pedido.idOrganizacao == this.idOrganizacao)
                .Select(x => new { x.id, x.qtde, x.idProduto }).ToListJsonObject<PedidoProduto>();
            
        }

        private void carregarProdutos() {
            
            var query = this.OEventoProdutoConsultaBL.query(this.idOrganizacao)
                .Where(x => 
                    x.idEvento == idEvento &&
                    x.flagOnline == true &&
                    x.ativo == true &&
                    (x.dtInicioVenda <= DateTime.Now || x.dtInicioVenda == null) && 
                    x.Produto.flagExcluido == false
                );
            
            this.listaEventoProduto = query.Select(x => new {
                                    x.id,
                                    x.qtdeDisponivel,
                                    x.qtdeParaVenda,
                                    x.idProduto,
                                    Produto = new {
                                        x.Produto.id,
                                        x.Produto.nome,
                                        x.Produto.descricaoResumida,
                                        x.Produto.valor, 
                                        x.Produto.valorDescontoAssociado,
                                        x.Produto.percentualDescontoAssociado,
                                        x.Produto.flagCortesia,
                                        x.Produto.flagParaTodos,
                                        x.Produto.flagSomenteAssociados,
                                        x.Produto.flagSomenteAssociadosQuites,
                                        x.Produto.idTipoProduto,
                                        TipoProduto = new { id = x.Produto.idTipoProduto, x.Produto.TipoProduto.descricao}
                                    }
                                }).OrderBy(x => x.id).ToListJsonObject<EventoProduto>();

            this.listaTipoProduto = this.listaEventoProduto.Select(x => new { id = x.Produto.idTipoProduto, x.Produto.TipoProduto.descricao })
                                        .Distinct().OrderBy(x => x.descricao).ToListJsonObject<TipoProduto>();
            
        }

        private void carregarArquivos(){

            var idsProduto = this.listaEventoProduto.Select(x => x.idProduto).ToList();
            
            var listaFotos = this.OArquivoUploadFotoBL.listar(0, EntityTypes.PRODUTO, "S")
                                    .Where(x => idsProduto.Contains(x.idReferenciaEntidade)).ToList();
            
            this.listaEventoProduto.ForEach(Item => {
                Item.OFoto = listaFotos.FirstOrDefault(x => x.idReferenciaEntidade == Item.id) ?? new ArquivoUpload();
            });
             
        }
        
        //
        private object montarRetorno(TipoProduto OTipoProduto) {
            
            var listaProdutos = this.listaEventoProduto.Where(x => x.Produto.idTipoProduto == OTipoProduto.id)
                .Select(OEventoProduto => new {
                    OEventoProduto.id,
                    OEventoProduto.qtdeDisponivel,
                    OEventoProduto.qtdeParaVenda,
                    idProduto = OEventoProduto.Produto.id,
                    nomeProduto = OEventoProduto.Produto.nome,
                    descricaoResumidaProduto = OEventoProduto.Produto.descricaoResumida,
                    valorProduto = OEventoProduto.Produto.valor, 
                    OEventoProduto.Produto.valorDescontoAssociado,
                    OEventoProduto.Produto.percentualDescontoAssociado,
                    OEventoProduto.Produto.flagCortesia,
                    OEventoProduto.Produto.flagParaTodos,
                    OEventoProduto.Produto.flagSomenteAssociados,
                    OEventoProduto.Produto.flagSomenteAssociadosQuites,
                    flagEsgotado = this.verificarEstoque(OEventoProduto),
                    OEventoProduto.Produto.idTipoProduto,
                    descricaoTipoProduto = OEventoProduto.Produto.TipoProduto.descricao,
                    legendaFoto = OEventoProduto.OFoto.legenda,
                    urlFotoOriginal = OEventoProduto.OFoto.linkImagem(),
                    urlFotoThumb = OEventoProduto.OFoto.linkImagem("sistema")
                });
            
            var Dados = new {
                idTipoProduto = OTipoProduto.id,
                OTipoProduto.descricao,
                listaProdutos
            };

            return Dados;

        }

        private bool verificarEstoque(EventoProduto OEventoProduto){
            
            var qtdPedidos = this.listaPedidosProdutos.Where(x => x.idProduto == OEventoProduto.idProduto).Sum(x => x.qtde);

            if (qtdPedidos >= OEventoProduto.qtdeParaVenda){
                return true;
            }

            return false;

        }
    }
}