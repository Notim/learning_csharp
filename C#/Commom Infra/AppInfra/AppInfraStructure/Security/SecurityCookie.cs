using System;
using System.Linq;
using System.Web;

using UTIL.Utils;

namespace WEB.AppInfrastructure.Security {

    public static class SecurityCookie {

        public static string idOrganizacao {
            get { return GetValue("assixorx"); }
            set { SetValue("assixorx", value, DateTime.Now.AddDays(12)); }
        }

        public static string idOrganizacaoCrypt {
            get { return GetValue("assixorxcr"); }
            set { SetValue("assixorxcr", value, DateTime.Now.AddDays(12)); }
        }

        public static string tokenOrganizacao {
            get { return GetValue("htsixtkxorx"); }
            set { SetValue("htsixtkxorx", value, DateTime.Now.AddDays(12)); }
        }

        public static string tokenOrganizacaoCrypt {
            get { return GetValue("htsixtkxorxcr"); }
            set { SetValue("htsixtkxorxcr", value, DateTime.Now.AddDays(12)); }
        }

        public static string rotaCustomizada {
            get { return GetValue("rtczsixtkxorx"); }
            set { SetValue("rtczsixtkxorx", value, DateTime.Now.AddDays(12)); }
        }

        public static string rotaCustomizadaCrypt {
            get { return GetValue("rtczsixtkxorxcr"); }
            set { SetValue("rtczsixtkxorxcr", value, DateTime.Now.AddDays(12)); }
        }

        public static string linkAreaAssociado {
            get { return GetValue("lkaasixtkxorx"); }
            set { SetValue("lkaasixtkxorx", value, DateTime.Now.AddDays(12)); }
        }

        public static string linkAreaAssociadoCrypt {
            get { return GetValue("lkaasixtkxorxcr"); }
            set { SetValue("lkaasixtkxorxcr", value, DateTime.Now.AddDays(12)); }
        }

        public static string cookieFix {
            get { return GetValue("fixsafari"); }
            set { SetValue("fixsafari", value, DateTime.Now.AddDays(12)); }
        }

        public static string userId {
            get { return GetValue("ptuix"); }
            set { SetValue("ptuix", value, DateTime.Now.AddDays(1)); }
        }

        public static string userIdCrypt {
            get { return GetValue("ptuixcr"); }
            set { SetValue("ptuixcr", value, DateTime.Now.AddDays(1)); }
        }

        public static string idPessoa {
            get { return GetValue("ptuip"); }
            set { SetValue("ptuip", value, DateTime.Now.AddDays(1)); }
        }

        public static string idPessoaCrypt {
            get { return GetValue("ptuipcr"); }
            set { SetValue("ptuipcr", value, DateTime.Now.AddDays(1)); }
        }

        public static string idTipoCadastro {
            get { return GetValue("ptutc"); }
            set { SetValue("ptutc", value, DateTime.Now.AddDays(1)); }
        }

        public static string idTipoCadastroCrypt {
            get { return GetValue("ptutccr"); }
            set { SetValue("ptutccr", value, DateTime.Now.AddDays(1)); }
        }

        public static string userName {
            get { return GetValue("ptunx"); }
            set { SetValue("ptunx", value, DateTime.Now.AddDays(1)); }
        }

        public static string userNameCrypt {
            get { return GetValue("ptunxcr"); }
            set { SetValue("ptunxcr", value, DateTime.Now.AddDays(1)); }
        }

        public static string userEmail {
            get { return GetValue("ptumx"); }
            set { SetValue("ptumx", value, DateTime.Now.AddDays(1)); }
        }

        public static string userEmailCrypt {
            get { return GetValue("ptumxcr"); }
            set { SetValue("ptumxcr", value, DateTime.Now.AddDays(1)); }
        }

        public static string tipoPessoa {
            get { return GetValue("pttipopesamx"); }
            set { SetValue("pttipopesamx", value, DateTime.Now.AddDays(1)); }
        }

        public static string tipoPessoaCrypt {
            get { return GetValue("pttipopesamxcr"); }
            set { SetValue("pttipopesamxcr", value, DateTime.Now.AddDays(1)); }
        }

        public static string status {
            get { return GetValue("ptstamx"); }
            set { SetValue("ptstamx", value, DateTime.Now.AddDays(1)); }
        }

        public static string statusCrypt {
            get { return GetValue("ptstamxcr"); }
            set { SetValue("ptstamxcr", value, DateTime.Now.AddDays(1)); }
        }

        public static string situacaoFinanceira {
            get { return GetValue("ptsituamx"); }
            set { SetValue("ptsituamx", value, DateTime.Now.AddDays(1)); }
        }

        public static string situacaoFinanceiraCrypt {
            get { return GetValue("ptsituamxcr"); }
            set { SetValue("ptsituamxcr", value, DateTime.Now.AddDays(1)); }
        }
        
        public static bool? flagInstructions {
            get { return GetValue("xinsrx").toBool(); }
            set { SetValue("xinsrx", value.ToString(), DateTime.Now.AddDays(12)); }
        }

        private static string GetValue(string key) {

            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(key)) {

                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);

                return cookie?.Value;
            }

            return null;
        }

        private static void SetValue(string key, string value, DateTime expires) {

            var httpCookie = HttpContext.Current.Response.Cookies[key];

            if (httpCookie != null) httpCookie.Value = value;

            var cookie = HttpContext.Current.Response.Cookies[key];

            if (cookie != null) cookie.Expires = expires;
        }
    }

}