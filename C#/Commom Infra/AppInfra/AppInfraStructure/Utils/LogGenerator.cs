using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;

using UTIL.Utils;

using WEB.AppInfrastructure.Core.Config;
using WEB.AppInfraStructure.Session;

using WebGrease.Configuration;

namespace WEB.AppInfrastructure.Utils {
    
    public static class LogGenerator {

        public static void saveError(this Exception ex, string message, string subPath = "") {
            
            var txt = new StringBuilder();
            txt.AppendLine("DATETIME: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            txt.AppendLine("EXTRAS: " + message).Append("\n");
            txt.AppendLine("EXCEPTON: " + ex.Message).Append("\n");
            txt.AppendLine("TRACE: " + ex.StackTrace).Append("\n");
            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(0);
            if (frame != null) {
                var line = frame.GetFileLineNumber();
                txt.AppendLine("FILE: " + frame.GetFileName()).Append("\n");
                txt.AppendLine("LINE: " + line).Append("\n");
            }
            if (ex.InnerException != null) {
                if (ex.InnerException.InnerException != null) {
                    txt.AppendLine("INNER EXCEPTON: " + ex.InnerException.InnerException.Message).Append("\n");
                    txt.AppendLine("TRACE: " + ex.InnerException.InnerException.StackTrace).Append("\n");
                } else {
                    txt.AppendLine("INNER EXCEPTON: " + ex.InnerException.Message).Append("\n");
                    txt.AppendLine("TRACE: " + ex.InnerException.StackTrace).Append("\n");
                }
            }

            if (HttpContext.Current != null) {
                var paramsPost = "";
                foreach (string name in HttpContext.Current.Request.Form) {
                    paramsPost += name + ": " + HttpContext.Current.Request.Unvalidated.Form[name] + " | ";
                }
                txt.AppendLine("POST: ").Append(paramsPost).Append("\n");
                txt.AppendLine("URL: " + HttpContext.Current.Request.Url.AbsoluteUri).Append("\n");
            }

            txt.AppendLine("\n--------------------------------------------------------------------------").Append("\n\n");

            var pathFile = UtilConfig.pathLogFiles;

            if (!subPath.isEmpty()) {
                pathFile = Path.Combine(pathFile, subPath);
            }

            if (!Directory.Exists(pathFile)) {
                Directory.CreateDirectory(pathFile);
            }

            pathFile = Path.Combine(pathFile, ("error_" + DateTime.Now.ToShortDateString().onlyNumber() + ".log"));
            
            if (!File.Exists(pathFile)) {
                File.Create(pathFile).Close();
            }

            TextWriter Writer = File.AppendText(pathFile);
            Writer.Write(txt.ToString());
            Writer.Close();
            
        }
        
        public static void saveLog(string strLog, string subFolder = "", string customFileName = "") {
            var txt = new StringBuilder();
            
            txt.AppendLine("***********************************");
            txt.AppendLine("DATETIME: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            txt.AppendLine("LOG: " + strLog);
            txt.AppendLine("***********************************");

            subFolder = string.IsNullOrEmpty(subFolder) ? "" : $"/{subFolder}";
            
            var fileName = $"{customFileName}{DateTime.Now.ToShortDateString().onlyNumber()}.log";
            var pathFile = Path.Combine(UtilConfig.pathLogFiles, subFolder);
            var fullName = Path.Combine(pathFile, fileName);

            if (!File.Exists(fullName)) {
                UtilIO.createFile(pathFile, fileName, false);
            }

            TextWriter Writer = File.AppendText(fullName);
            Writer.Write(txt.ToString());
            Writer.Close();
        }

        public static void saveSQL(string strLog) {
            var txt = new StringBuilder();
            
            txt.AppendLine("------------------------------------------------------------------------").Append("\n");
            txt.Append("DATETIME: ").Append(DateTime.Now.ToShortDateString()).Append(" ").Append(DateTime.Now.ToShortTimeString()).Append("\n");
            txt.Append("SQL: " + strLog);
            txt.AppendLine("\n----------------------------------------------------------------------").Append("\n\n");

            var pathFile = Path.Combine(UtilConfig.pathLogFiles, ("sql_" + DateTime.Now.ToShortDateString().Replace("/", "") + ".log"));

            if (!File.Exists(pathFile)) {
                File.Create(pathFile).Close();
            }

            TextWriter Writer = File.AppendText(pathFile);
            Writer.Write(txt.ToString());
            Writer.Close();
        }

        public static void accessDenied(string area, string controller, string action) {
            var txt = new StringBuilder();
            
            txt.AppendLine("-----------------------------------------------------------------------------").Append("\n");
            txt.Append("DATETIME: ").Append(DateTime.Now.ToShortDateString()).Append(" ").Append(DateTime.Now.ToShortTimeString()).Append("\n");
            txt.Append("USER: " + SessionSistema.getIdUser().ToString()).Append("\n");
            txt.Append("Area: " + area).Append("\n");
            txt.Append("CONTROLLER: " + controller).Append("\n");
            txt.Append("ACTION: " + action);
            txt.AppendLine("\n--------------------------------------------------------------------------").Append("\n\n");
            
            var pathFile = UtilConfig.pathLogFiles + ("accessdenied_" + DateTime.Now.ToShortDateString().Replace("/", "") + ".txt");

            if (!File.Exists(pathFile)) {
                File.Create(pathFile).Close();
            }

            TextWriter Writer = File.AppendText(pathFile);
            Writer.Write(txt.ToString());
            Writer.Close();
        }
    }
}