using System;
using System.IO;
using System.IO.Compression;
using System.Web;

using UTIL.Utils;

using WEB.AppInfraStructure.Utils.FileSystem.Interfaces;
using WEB.AppInfraStructure.Utils.FileSystem.Wrappers;

namespace WEB.AppInfraStructure.Utils.FileSystem.Services {

    public class ProjectRootFile : IUtilFile {
        
        private static string globalFileName => $"{DateTime.Now:yyyy-MM-dd}";
        
        private readonly IUtilDirectory UtilDirectory;
        
        public ProjectRootFile(IUtilDirectory _UtilDirectory) {

            UtilDirectory = _UtilDirectory;
        }

        public FileInfo createEmptyFile(string path, string filename = null) {

            UtilDirectory.createDir(path);
            
            var fileName = filename ?? $"{globalFileName}.txt";

            var fullpath = path + fileName;
            
            using (var writter = File.AppendText(fullpath)) {
                writter.Write("");
            }
            
            return new FileInfo(fullpath);
        }
        
        public FileInfo writeText(string path, string fileContent, string filename = null) {
            
            var FileInfo = this.createEmptyFile(path, filename);

            using (TextWriter Writer = FileInfo.AppendText()) {
                Writer.Write(fileContent);
            }
            
            return FileInfo;
        }
        
        public FileInfo saveUploadedFile(HttpPostedFileBase FileUpload, string path) {
            
            var fileName = $"{globalFileName}-upload{Path.GetExtension(FileUpload.FileName)?.ToLower()}";
            
            UtilDirectory.createDir(path);
            
            var fullPath = string.Concat(path, fileName);
            
            FileUpload.SaveAs(fullPath);
            
            var FileInfo = new FileInfo(fullPath);
            
            return FileInfo;
        }
    }

}
