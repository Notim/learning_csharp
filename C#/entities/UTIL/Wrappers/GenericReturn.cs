using System;
using System.Collections.Generic;

namespace UTIL.Wrappers {

    public class GenericReturn {
        
        public bool         flagError  { get; set; }
        public List<string> listaErros { get; set; }
        public object       info       { get; set; }
        
        private static GenericReturn _instance;        
        public static GenericReturn getInstance() 
            => _instance = _instance 
                           ?? new GenericReturn();
        
        public GenericReturn() {
            this.listaErros = new List<string>();
        }
        
        public static GenericReturn newInstance(bool flagErro, string message = "", object info = null) {
            var OUtilRetorno = new GenericReturn();

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
