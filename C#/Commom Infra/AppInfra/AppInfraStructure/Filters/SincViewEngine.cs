using System.Web.Mvc;

namespace WEB.AppInfrastructure.Filters {
    public class SincViewEngine : RazorViewEngine {

        public SincViewEngine() {
            ViewLocationFormats = new[] {
                "~/views/{1}/{0}.cshtml"
            };

            PartialViewLocationFormats = new[] {
                "~/views/{1}/{0}.cshtml",
                "~/views/shared/{0}.cshtml",
            };
        }

    }
}