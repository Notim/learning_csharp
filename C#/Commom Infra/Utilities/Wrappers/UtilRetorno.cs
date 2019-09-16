using System.Collections.Generic;

namespace UTIL.Wrappers {

    public class UtilRetorno {

        public bool         flagError  { get; set; }
        public List<string> listaErros { get; set; }
        public object       info       { get; set; }

        public UtilRetorno() {
            this.listaErros = new List<string>();
        }

        public static UtilRetorno newInstance(bool flagErro, string message = "", object info = null) {

            var OUtilRetorno = new UtilRetorno {
                                                   flagError = flagErro
                                               };

            if (!string.IsNullOrEmpty(message)) {
                OUtilRetorno.listaErros.Add(message);
            }

            if (info != null) {
                OUtilRetorno.info = info;
            }

            return OUtilRetorno;
        }

        public UtilRetorno isError(bool erro) {
            this.flagError = erro;

            return this;
        }

        public UtilRetorno addMessage(string message) {
            this.listaErros.Add(message);

            return this;
        }

        public UtilRetorno addMessage(IList<string> messages) {
            foreach (var message in messages) {
                this.addMessage(message);
            }

            return this;
        }

        public UtilRetorno addMessage(params string[] messages) {
            foreach (var message in messages) {
                this.addMessage(message);
            }

            return this;
        }

        public UtilRetorno addInfo(object info) {
            this.info = info;

            return this;
        }
    }

}