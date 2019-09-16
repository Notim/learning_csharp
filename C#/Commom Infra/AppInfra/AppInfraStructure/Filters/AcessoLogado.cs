using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

using WEB.AppInfrastructure.Security;

namespace WEB.AppInfrastructure.Filters {

    public class AcessoLogado : AuthorizeAttribute {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext) {
            if (!httpContext.User.hasLogin()) {
                return false;
            }

            return true;
        }

        //Alteracao de autorizacao
        public override void OnAuthorization(AuthorizationContext filterContext) {
            var OUser = filterContext.HttpContext.User;

            //Se for requisição Ajax, liberar acesso
            if (filterContext.HttpContext.Request.IsAjaxRequest()) {
                return;
            }

            //Se houver filtro de anônimo na action
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()) {
                return;
            }

            //Se houver filtro de anônimo na controller
            if (filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()) {
                return;
            }

            //Caso seja uma action filha liberar o acesso
            if (filterContext.IsChildAction) {
                base.OnAuthorization(filterContext);

                return;
            }

            //Se não houver login, ja direcionar para tela inicial
            if (!OUser.hasLogin()) {
                base.OnAuthorization(filterContext);

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "area"      , "MinhaConta" }, 
                        { "controller", "Login" }, 
                        { "action"    , "index" }, 
                        { "ReturnUrl" , filterContext.HttpContext.Request.RawUrl }
                    }
                );

                return;
            }
        }
    }

}