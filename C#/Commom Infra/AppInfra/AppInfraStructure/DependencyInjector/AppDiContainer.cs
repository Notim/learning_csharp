using System.Web.Mvc;

using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace WEB.AppInfraStructure.DependencyInjector {

    public static class AppDiContainer {

        public static void register() {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            WEB_DiRepository.mapear(ref container);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

    }

}