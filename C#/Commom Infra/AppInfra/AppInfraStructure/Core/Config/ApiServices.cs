using System.Configuration;

namespace WEB.AppInfrastructure.Core.Config {

    public static class ApiServices {

        public static string apiBaseLinkDev => ConfigurationManager.AppSettings["apiBaseLinkDev"];

        public static string apiBaseLinkProd => ConfigurationManager.AppSettings["apiBaseLinkProd"];

        public static string servicoAtendimento => ConfigurationManager.AppSettings["servicoAtendimento"];

        public static string servicoLabelConsulta => ConfigurationManager.AppSettings["servicoLabelConsulta"];

        public static string servicoTextoConsulta => ConfigurationManager.AppSettings["servicoTextoConsulta"];

        public static string servicoListaAssociados => ConfigurationManager.AppSettings["servicoListaAssociados"];

        public static string servicoListaHistoricoContribuicao => ConfigurationManager.AppSettings["servicoListaHistoricoContribuicao"];

        public static string servicoListaAssociadosResumo => ConfigurationManager.AppSettings["servicoListaAssociadosResumo"];
        
        public static string servicoListaContribuicoes => ConfigurationManager.AppSettings["servicoListaContribuicoes"];

    }

}