using System.Web.Optimization;

namespace WEB {
    public static class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(loadDefaultCSS());
            bundles.Add(loadJSDefaultPlugins());
            bundles.Add(loadJSDefault());
        }
        
        private static StyleBundle loadDefaultCSS() {
            var bundles = new StyleBundle("~/Assets/css/default.css");
            bundles.Include(
                "~/Assets/Js/plugins/bootstrap/bootstrap-4.3.1-dist/css/bootstrap.min.css",
                "~/Assets/css/flash-messages.css",
                "~/Assets/css/project-custom.css",
                "~/Assets/css/mobile.css"
            );

            return bundles;
        }

        private static ScriptBundle loadJSDefaultPlugins() {
            var bundle = new ScriptBundle("~/Assets/js/plugins/jquery/jquery-3.4.1.min.js");
            bundle.Include(
                  "~/Assets/js/plugins/bootstrap/bootstrap-4.3.1-dist/js/bootstrap.bundle.min.js",
                  "~/Assets/js/plugins/jquery/jquery.unobtrusive-ajax.min.js",
                  "~/Assets/js/plugins/jquery/jquery.validate.min.js"
            );
             
             return bundle;
        }

        private static ScriptBundle loadJSDefault() {
            var bundle = new ScriptBundle("~/Assets/js/default_sistema.js");

            return bundle;
        }
    }
}
