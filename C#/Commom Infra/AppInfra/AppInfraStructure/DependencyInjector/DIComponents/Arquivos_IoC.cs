using SimpleInjector;

using WEB.AppInfraStructure.Utils.FileSystem.Interfaces;
using WEB.AppInfraStructure.Utils.FileSystem.Services;

namespace WEB.AppInfraStructure.DependencyInjector.DIComponents {

    public static class Arquivos_IoC {

        public static void mapear(ref Container container) {
            container.Register<IUtilDirectory, ProjectRootDirectory>();
            container.Register<IUtilFile, ProjectRootFile>();
        }
    }

}