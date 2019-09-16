using System.IO;

namespace WEB.AppInfraStructure.Utils.FileSystem.Interfaces {

    public interface IUtilDirectory {

        DirectoryInfo createDir(string path);

        FileInfo zipDir(string path, string name = null);

        DirectoryInfo unzipDir(FileInfo FileInfo, string extractPath);
    }

}