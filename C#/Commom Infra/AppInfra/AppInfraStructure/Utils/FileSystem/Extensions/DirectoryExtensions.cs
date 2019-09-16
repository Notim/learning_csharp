using System;
using System.IO;

namespace WEB.AppInfraStructure.Utils.FileSystem.Extensions {

    public static class DirectoryExtensions {

        public static string virtualPath(this FileInfo OFileInfo) {

            var dirApp = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "");

            var virtualPath = OFileInfo.FullName.Replace(dirApp, "");

            return virtualPath;

        }
    }

}