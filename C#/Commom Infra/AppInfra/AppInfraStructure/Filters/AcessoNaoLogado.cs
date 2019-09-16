using System.Web.Mvc;
using System.Web.Routing;

using WEB.AppInfrastructure.Security;

namespace WEB.AppInfrastructure.Filters {

    //Nao permitir que usuarios com login acessem area voltadas para usuarios nao autenticados
    public class AcessoNaoLogado: AuthorizeAttribute{

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext) {
            
            if (httpContext.User.hasLogin()) {
                return false;
            }
            return true;
        }

        //Alteracao de autorizacao
        public override void OnAuthorization(AuthorizationContext filterContext) {

            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult) {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                    { "area", "MinhaConta" },
                    { "controller", "Login" },
                    { "action", "Index" },
                    { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                });
            }
        }
    }
}
