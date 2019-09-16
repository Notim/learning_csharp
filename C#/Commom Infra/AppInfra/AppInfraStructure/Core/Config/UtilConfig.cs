using System.Configuration;

namespace WEB.AppInfrastructure.Core.Config {

    public static partial class UtilConfig {

        public static string applicationName => ConfigurationManager.AppSettings["applicationName"];

        public static string pathUploadsFiles => ConfigurationManager.AppSettings["pathUploadsFiles"];

        public static string pathTempFiles => ConfigurationManager.AppSettings["pathTempFiles"];

        public static string pathLogFiles => ConfigurationManager.AppSettings["pathLogFiles"];

        public static string linkAreaAssociado => ConfigurationManager.AppSettings["linkAreaAssociado"];

        public static string prefixoCookie => ConfigurationManager.AppSettings["prefixoCookie"];

        public static string pastaOrganizacao => ConfigurationManager.AppSettings["pastaOrganizacao"];

        public static string flagProducao => ConfigurationManager.AppSettings["flagProducao"];

        public static string flagAmbiente => ConfigurationManager.AppSettings["flagAmbiente"];

        public static string versaoSistema => ConfigurationManager.AppSettings["versaoSistema"];

        public static string tokenOrganizacao => ConfigurationManager.AppSettings["tokenOrganizacao"];

        public static string idOrganizacao => ConfigurationManager.AppSettings["idOrganizacao"];

        public static bool emProducao() {
            return flagProducao == "S";
        }

        public static string pathPastaOrganizacao() {
            return $"{pathUploadsFiles}/{pastaOrganizacao}/";
        }

        public static string linkResourses() {
            return $"{pathUploadsFiles}/{pastaOrganizacao}/";
        }

    }

}