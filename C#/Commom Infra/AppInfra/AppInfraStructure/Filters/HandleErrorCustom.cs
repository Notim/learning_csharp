using System;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

using WEB.AppInfrastructure.Core.Config;
using WEB.AppInfrastructure.Utils;

namespace WEB.AppInfrastructure.Filters {

    public class HandleErrorCustom : HandleErrorAttribute {

        public override void OnException(ExceptionContext filterContext) {
            try {
                var OErroDTO = new {
                    dominio               = HttpContext.Current.Request.Url.Host,
                    url                   = HttpContext.Current.Request.RawUrl,
                    dtErro                = DateTime.Now,
                    exceptionMessage      = filterContext.Exception.Message,
                    exceptionInnerMessage = filterContext.Exception.InnerException?.Message,
                    exceptionTrace        = filterContext.Exception.StackTrace,
                    metodo                = filterContext.Exception.TargetSite.ToString(),
                    ip                    = HttpContext.Current.Request.UserHostAddress,
                };

                filterContext.Exception.saveError("");
            }
            catch (Exception ex) {
                ex.saveError("erro no capturador de erros");
            }

            base.OnException(filterContext);
        }
    }

}