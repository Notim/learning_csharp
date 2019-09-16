using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using WEB.AppInfraStructure.Core;
using WEB.AppInfrastructure.Security;

namespace WEB.AppInfrastructure.Filters {

    public class FilterSecurity : AuthorizeAttribute {

        protected override bool AuthorizeCore(HttpContextBase httpContext) {

            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext) {

            // Se houver filtro de anônimo na action
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof (AllowAnonymousAttribute), true).Any()) {
                return;
            }

            // Se houver filtro de anônimo na controller
            if (filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof (AllowAnonymousAttribute), true).Any()) {
                return;
            }

            var OUser = HttpContextFactory.Current.User;
            if (SecurityCookie.tokenOrganizacao != null) {
                return;
            }

            base.OnAuthorization(filterContext);

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary {
                    {"area", "Erros"},
                    {"controller", "erro"},
                    {"action", "sem-organizacao"},
                    {"ReturnUrl", filterContext.HttpContext.Request.RawUrl}
                }
            );
        }
    }

}