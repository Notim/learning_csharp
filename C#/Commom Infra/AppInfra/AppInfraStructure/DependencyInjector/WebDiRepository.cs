using SimpleInjector;

using WEB.AppInfraStructure.DependencyInjector.DIComponents;

namespace WEB.AppInfraStructure.DependencyInjector {

    public static class WEB_DiRepository {

        public static void mapear(ref Container container) {

            Arquivos_IoC.mapear(ref container);

            Contribuicoes_IoC.mapear(ref container);

            Associados_IoC.mapear(ref container);
        }

    }

}