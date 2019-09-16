using System;
using System.Web.Mvc;

using UTIL.Utils;

namespace WEB.AppHelpers {

    public static class HtmlHelperCustom {
        
        public static MvcHtmlString ContentLoad(this HtmlHelper helper, string id, string url, string fnCallBack = "") {
            
            fnCallBack = fnCallBack.isEmpty() ? $"console.log('{id} content loaded!')" : fnCallBack;

            var tagBuild = new TagBuilder("section");

            tagBuild.Attributes.Add("id",              id);
            tagBuild.Attributes.Add("data-url",        url);
            tagBuild.Attributes.Add("data-fncallback", fnCallBack);
            tagBuild.AddCssClass("carregando content-load");

            tagBuild.InnerHtml = "";

            return new MvcHtmlString(tagBuild.ToString());
        }

        public static string exibirValor(this decimal? valorPadrao) {
            string html;
            
            if (valorPadrao.HasValue) {
                html = valorPadrao.Value.ToString("C");
            } else {
                html = new decimal(0).ToString("C");
            }

            return html;
        }

        public static MvcHtmlString exibirData(this DateTime dtPadrao, bool incluirHorario = false) {
            var html = "-";
            
            if (dtPadrao <= DateTime.MinValue)
                return new MvcHtmlString(html);
            
            html = dtPadrao.ToShortDateString();

            if (incluirHorario) {
                html = $"{html} {dtPadrao.ToShortTimeString()}";
            }

            return new MvcHtmlString(html);
        }
    }
}

