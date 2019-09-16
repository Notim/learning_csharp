using System.Text;
using System.Web.Mvc;
using PagedList;

namespace WEB.AppHelpers {

    public static class FormHelper {
        
        public static MvcHtmlString exibirBotoesFormularioCadastro(this HtmlHelper helper, string urlVoltar, string urlNovo = "0", bool flagExibirBotaoNovo = true) {

            var htm = new StringBuilder();

            var btVoltar = "<a href=\"" + urlVoltar + "\" class=\" btn btn-md btn-light margin-left-20\"><i class=\"fa fa-arrow-left\"></i> Voltar</a>";
            var btNovo   = "<a href=\"" + urlNovo   + "\" class=\"btn btn-md btn-info margin-left-20\"><i class=\"fa fa-plus\"></i> Novo Registro</a>";

            var btSalvar = helper.botaoSalvar().ToString();

            htm.Append(btVoltar);
            if (flagExibirBotaoNovo) {
                htm.Append(btNovo);
            }
            
            htm.Append(btSalvar);

            return new MvcHtmlString(htm.ToString());
        }
        
        public static MvcHtmlString exibirBotoesFormularioFerramenta(this HtmlHelper helper, string urlVoltar, string urlNovo = "0", bool flagExibirBotaoNovo = true) {

            var htm = new StringBuilder();

            var btVoltar = "<a href=\"" + urlVoltar + "\" class=\" btn btn-md btn-light margin-left-20\"><i class=\"fa fa-arrow-left\"></i> Voltar</a>";
            var btNovo   = "<a href=\"" + urlNovo   + "\" class=\"btn btn-md btn-info margin-left-20\"><i class=\"fa fa-wrench\"></i>  Nova conversão</a>";
            
            var btSalvar = helper.botaoConverter().ToString();

            htm.Append(btVoltar);
            if (flagExibirBotaoNovo) {
                htm.Append(btNovo);
            }
            
            htm.Append(btSalvar);

            return new MvcHtmlString(htm.ToString());
        }
        
        public static MvcHtmlString botaoSalvar(this HtmlHelper helper) {

            var btSalvar = "<button type=\"submit\" name=\"enviar\" class=\"btn btn-md btn-primary margin-left-20 link-loading\"><i class=\"far fa-hdd\"></i> Salvar Dados</button>";

            return new MvcHtmlString(btSalvar);
        }
        
        public static MvcHtmlString botaoConverter(this HtmlHelper helper) {

            var btSalvar = "<button type=\"submit\" name=\"enviar\" class=\"btn btn-md btn-primary margin-left-20 link-loading\"><i class=\"far fa-download\"></i> Converter</button>";

            return new MvcHtmlString(btSalvar);
        }
        
        public static MvcHtmlString InputFileSimples(this HtmlHelper helper, string nomeCampo, string flagPreview = "true", bool flagMultiple = true, string flagRemove = "true") {
            var htm = new StringBuilder();
            
            htm.Append($"<div class=\"input-group\">");
            htm.Append($"    <div class=\"input-group-prepend\">");
            htm.Append($"        <span class=\"input-group-text\" id=\"{nomeCampo}Addon\">Upload</span>");
            htm.Append($"    </div>");
            htm.Append($"    <div class=\"custom-file\">");
            htm.Append($"        <input type=\"file\" class=\"input-file file custom-file-input width-300\" id='{nomeCampo}' name={nomeCampo}{(flagMultiple ? " multiple=\"multiple\" " : "")} data-show-upload=\"false\" data-show-preview=\"{flagPreview}\" data-show-remove=\"{flagRemove}\" data-preview-settings=\"[width:'auto', height:'100px']\" data-browse-label=\"Procurar ...\" data-remove-label=\"Remover\" data-show-caption=\"true\" data-preview-file-type=\"image\" aria-describedby=\"{nomeCampo}Addon\" />");
            htm.Append($"        <label class=\"custom-file-label\" for=\"{nomeCampo}\">Procurar ...</label>");
            htm.Append($"    </div>");
            htm.Append($"</div>");
            
            return new MvcHtmlString(htm.ToString());
        }
        
        public static MvcHtmlString Instrucao(this HtmlHelper helper, string instrucao) {
            var htm = new StringBuilder();
            
            htm.Append($"<small><i class=\"fal fa-info-circle\"></i>{instrucao}</small><br/>");
            
            return new MvcHtmlString(htm.ToString());
        }

        public static MvcHtmlString paginarRegistros<T>(this HtmlHelper helper, IPagedList<T> Model, MvcHtmlString pager) {
            var htm = new StringBuilder();
            htm.Append("<div style=\"margin-top:7px;\">");
            htm.Append("    <div class=\"col-sm-12 no-padding-right\">");
            htm.Append("        <div class=\"dataTables_paginate paging_bootstrap text-center\">");
            htm.Append(                "<div>");
            htm.Append(pager);
            htm.Append(                "</div>");
            htm.Append("        </div>");
            htm.Append("    </div>");
            htm.Append("</div>");
            htm.Append("<div class=\"clearfix\"></div><br />");

            return new MvcHtmlString(htm.ToString());
        }

        public static MvcHtmlString paginarRegistrosSemPaginas<T>(this HtmlHelper helper, IPagedList<T> Model, MvcHtmlString pager) {
            var htm = new StringBuilder();
            htm.Append("<div>");
            htm.Append("    <div class=\"col-xs-4 no-padding\">");
            htm.Append("        <div class=\"dataTables_info\">Exibindo ").Append(Model.TotalItemCount < Model.PageSize ? Model.TotalItemCount : Model.PageSize).Append(" de ").Append(Model.TotalItemCount).Append(" registros.").Append("</div>");
            htm.Append("    </div>");
            htm.Append("    <div class=\"col-xs-8 no-padding\">");
            htm.Append("        <div class=\"dataTables_paginate paging_bootstrap\">");
            htm.Append(                pager);
            htm.Append("        </div>");
            htm.Append("    </div>");
            htm.Append("</div>");

            return new MvcHtmlString(htm.ToString());
        }
    }
}

