using System.Collections.Generic;
namespace System {
    public class UtilRetorno {        
        public bool         flagError { get; set; }
        public List<string> listaErros { get; set; }
        public object       info { get; set; }
        
        private static UtilRetorno _instance;        
        public static UtilRetorno getInstance() 
            => _instance = _instance 
            ?? new UtilRetorno();
        
        public UtilRetorno() {
            this.listaErros = new List<string>();
        }
        
        public static UtilRetorno newInstance(bool flagErro, string message = "", object info = null) {
            var OUtilRetorno = new UtilRetorno();

            OUtilRetorno.flagError = flagErro;

            if (!String.IsNullOrEmpty(message)) {
                OUtilRetorno.listaErros.Add(message);
            }

            if (info != null) {
                OUtilRetorno.info = info;
            }

            return OUtilRetorno;
        }
    }

}
using System.Collections.Generic;

namespace System.Json {

    public class JsonMessage{

        public bool error { get; set; }
        public string message { get; set; }
        public IList<string> listMessage {get; set;}
        public object extraInfo { get; set; }
        
        public JsonMessage(){
            this.listMessage = new List<string>();
        }
    }

    public class JsonMessageStatus: JsonMessage{
        public string active { get; set; }
    }
}
