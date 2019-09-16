using System;
using System.IO;
using System.IO.Compression;

using WEB.AppInfrastructure.Core.Config;
using WEB.AppInfraStructure.Utils.FileSystem.Interfaces;

namespace WEB.AppInfraStructure.Utils.FileSystem.Services {

    public class ProjectRootDirectory : IUtilDirectory {
        
        private static string globalFileName => $"{DateTime.Now:yyyyMMddhhmmss}";
        
        private string obterDiretorioAplicacao() {
            return AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "");
        }
        
        public DirectoryInfo createDir(string path) {
            var Directory = new DirectoryInfo(path);
            
            if (Directory.Exists) { 
                return Directory;
            }
            Directory.Create();
            
            return Directory;
        }

        public FileInfo zipDir(string path, string name = null) {
            
            var fileName = name ?? $"{globalFileName}-zipped.zip";

            var fullPath = UtilConfig.pathTempFiles + fileName;
            
            if (File.Exists(fullPath)) {
                File.Delete(fullPath);
            }
            
            ZipFile.CreateFromDirectory(path, fullPath);
            
            return new FileInfo(fullPath);
        }
        
        public DirectoryInfo unzipDir(FileInfo FileInfo, string extractPath) {
            
            if (File.Exists(extractPath)) {
                File.Delete(extractPath);
            }
            
            ZipFile.ExtractToDirectory(FileInfo.FullName, extractPath);
            
            return new DirectoryInfo(extractPath);
        }
    }

}