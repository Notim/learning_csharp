using SimpleInjector;

using WEB.Areas.Associados.ConsultaAssociadoHistoricoContribuicoes.Services.Repository;
using WEB.Areas.Associados.ConsultaAssociados.Services.Repository;

namespace WEB.AppInfraStructure.DependencyInjector.DIComponents {

    public static class Associados_IoC {

        public static void mapear(ref Container container) {

            container.Register<IConsultaAssociadosApi, ConsultaAssociadosApi>();
            container.Register<IConsultaAssociadoHistoricoContribuicoesApi, ConsultaAssociadoHistoricoContribuicoesApi>();

        }
    }

}