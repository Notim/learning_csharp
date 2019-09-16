using System.Text;
using System.Web.Mvc;

using WEB.AppInfrastructure.Core.Config;

namespace WEB.AppHelpers {

    public static class ArquivoHelper {
       
        // Inclusao de CSSs necessarios para modulo de Arquivos
        public static MvcHtmlString includeCSSModuloArquivos(this HtmlHelper helper) {
            
            var htm = new StringBuilder();
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            htm.Append("<link href=\"").Append(urlHelper.Content("Areas/Arquivos/js/plugins/bootstrap-editable/bootstrap3-editable/css/bootstrap-editable.css")).Append("\" rel=\"stylesheet\" />");
            htm.Append("<link href=\"").Append(urlHelper.Content("Areas/Arquivos/js/plugins/bootstrap-fileinput/css/fileinput.min.css")).Append("\" rel=\"stylesheet\" />");
            htm.Append("<link href=\"").Append(urlHelper.Content("Areas/Arquivos/js/plugins/jqueryFancybox/jquery.fancybox.css")).Append("\" rel=\"stylesheet\" />");

            return new MvcHtmlString(htm.ToString());
        }

        // Inclusao de JSs necessarios para modulo de Arquivos
        public static MvcHtmlString includeJSModuloArquivos(this HtmlHelper helper) {
            StringBuilder htm = new StringBuilder();
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            htm.Append("<script src=\"").Append(urlHelper.Content("" + UtilConfig.linkResourses() + "Areas/Arquivos/js/plugins/bootstrap-editable/bootstrap3-editable/js/bootstrap-editable.min.js")).Append("\"></script>");
            htm.Append("<script src=\"").Append(urlHelper.Content("" + UtilConfig.linkResourses() + "Areas/Arquivos/js/plugins/bootstrap-fileinput/js/fileinput.min.js")).Append("\"></script>");
            htm.Append("<script src=\"").Append(urlHelper.Content("" + UtilConfig.linkResourses() + "Areas/Arquivos/js/plugins/jqueryFancybox/jquery.fancybox.js")).Append("\"></script>");
            return new MvcHtmlString(htm.ToString());
        }

        // Inclusaode JSs necessarios para modulo de Arquivos
        public static MvcHtmlString includeJSModuloArquivosFotos(this HtmlHelper helper) {
            var htm = new StringBuilder();
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            htm.Append("<script src=\"").Append(urlHelper.Content("~/Assets/Js/plugins/associatec/plugins/jQueryUI/jquery-ui.js")).Append("\"></script>");
            htm.Append(helper.includeJSModuloArquivos());
            htm.Append("<script src=\"").Append(urlHelper.Content("Areas/Arquivos/js/arquivo-foto.js")).Append("\"></script>");

            return new MvcHtmlString(htm.ToString());
        }

        public static SelectList selectListTipoArquivo(string selected) {

            var list = new[] {
                    new{value = "img", text = "Imagem"},
                    new{value = "doc", text = "Documento"}
            };
            
            return new SelectList(list, "value", "text", selected);
        }

    }

}