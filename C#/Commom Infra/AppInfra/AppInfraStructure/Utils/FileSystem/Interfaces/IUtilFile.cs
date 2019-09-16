using System.IO;
using System.Web;

using WEB.AppInfraStructure.Utils.FileSystem.Wrappers;

namespace WEB.AppInfraStructure.Utils.FileSystem.Interfaces {

    public interface IUtilFile {

        FileInfo createEmptyFile(string path, string filename = null);

        FileInfo writeText(string path, string fileContent, string filename = null);

        FileInfo saveUploadedFile(HttpPostedFileBase FileUpload, string path);

    }

}