using System;
using System.Collections.Generic;
using System.IO;

namespace UTIL.Utils {

    public static class UtilFileInfo {
        
        public static IDictionary<string, string> mimeTypes => UtilFileInfoMimeTypes.mimeTypes;

        private static readonly List<string> _allowedExtensionsImages = new List<string> {
            ".png", 
            ".jpg",
            ".jpeg",
            ".gif"
        };
        
        private static readonly List<string> _allowedExtensionsArquives = new List<string> {
            ".png", ".jpg", ".gif", 
            ".doc", ".docx", ".xls",
            ".xlsx", ".pdf", ".txt", 
            ".mp3", ".mp4", ".zip", 
            ".rar", ".msg", ".csv",
        };

        public static string getExtension(this FileInfo File) {
            if (File == null) {
                return "";
            }

            var fileName = File.Name;

            if (string.IsNullOrEmpty(fileName) || !fileName.Contains(".")) {
                return "";
            }

            return File.Name.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
        }

        public static bool isImage(this FileInfo File) {
            return _allowedExtensionsImages.Contains(File.getExtension());
        }

        public static bool validarExtensaoArquivo(this FileInfo File) {
            var extension = File.getExtension();

            return _allowedExtensionsArquives.Contains(extension);
        }

        public static bool validarArquivo(this FileInfo File) {
            if (File == null) {
                return false;
            }
            
            var extension = File.getExtension();
            
            return _allowedExtensionsArquives.Contains(extension.ToLower());
        }

        public static bool validarExcel(this FileInfo File) {
            var extension = getExtension(File).ToLower();

            return extension == ".xls" | extension == ".xlsx";
        }
        
        public static bool validarImagem(this FileInfo File) {
            if (File == null) {
                return false;
            }
            
            var extension = File.getExtension();

            return _allowedExtensionsImages.Contains(extension.ToLower());
        }
        
        public static bool validarCsv(this FileInfo File) {
            var extension = getExtension(File).ToLower();

            return extension == ".csv";
        }
        
        public static bool validatePDF(this FileInfo File) {
            
            var extension = File.getExtension().ToLower();
            
            return extension == ".pdf";
        }
        
        public static bool validateTextFile(this FileInfo File) {
            var extension = File.getExtension().ToLower();

            return extension == ".txt";
        }

        public static bool validarArquivoRetorno(this FileInfo File) {
            if (File == null) { 
                return false;
            }
            
            var extension = getExtension(File).ToLower();
            
            return extension == ".ret";
        }
        
        public static string GetMimeType(this FileInfo fileInfo) {
            if (fileInfo == null) {
                throw new ArgumentNullException(nameof (fileInfo));
            }
            
            return mimeTypes.TryGetValue(fileInfo.Extension, out var mime) ? mime : "application/octet-stream";
        }
    }

}