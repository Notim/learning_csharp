using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UTIL.Utils {
    
    public static class UtilLog {

        public static void saveError(this Exception ex, string message, string path = "") {
            
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

            txt.AppendLine("\n--------------------------------------------------------------------------").Append("\n\n");

            var pathFile = path;
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
        
        public static void saveLog(string strLog, string path = "", string customFileName = "") {
            var txt = new StringBuilder();
            
            txt.AppendLine("***********************************");
            txt.AppendLine("DATETIME: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            txt.AppendLine("LOG: " + strLog);
            txt.AppendLine("***********************************");
            
            var fileName = $"{customFileName}{DateTime.Now.ToShortDateString().onlyNumber()}.log";
                        
            var fullName = Path.Combine(path, fileName);

            if (!File.Exists(fullName)) {
                UtilIO.createFile(path, fileName, false);
            }

            TextWriter Writer = File.AppendText(fullName);
            Writer.Write(txt.ToString());
            Writer.Close();
        }

        public static void saveSQL(string strLog, string path) {
            var txt = new StringBuilder();
            
            txt.AppendLine("------------------------------------------------------------------------").Append("\n");
            txt.Append("DATETIME: ").Append(DateTime.Now.ToShortDateString()).Append(" ").Append(DateTime.Now.ToShortTimeString()).Append("\n");
            txt.Append("SQL: " + strLog);
            txt.AppendLine("\n----------------------------------------------------------------------").Append("\n\n");
            
            var pathFile = Path.Combine(path, ("sql_" + DateTime.Now.ToShortDateString().Replace("/", "") + ".log"));

            if (!File.Exists(pathFile)) {
                File.Create(pathFile).Close();
            }

            TextWriter Writer = File.AppendText(pathFile);
            Writer.Write(txt.ToString());
            Writer.Close();
        }
    }
}