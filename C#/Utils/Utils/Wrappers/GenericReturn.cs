using System.Collections.Generic;
using System.Linq;

namespace UTIL.Wrappers {

    public class GenericReturn {

        public bool          error       { get; set; }
        public IList<string> messageList { get; set; }
        public object        info        { get; set; }

        public static GenericReturn NewInstance(bool error = true, List<string> messages = null, object info = null) {
            var GenReturn = new GenericReturn();

            if (messages != null && messages.Any()) {
                GenReturn.messageList = messages;
            }

            GenReturn.error = error;

            if (info != null) {
                GenReturn.info = info;
            }

            return GenReturn;
        }
    }

    internal sealed class example {
        public void main() {
            // a classe GenericReturn tem tres jeitos de usar

            // Criando um novo Obj e atribuindo os valores diretamente
            var GenReturn = new GenericReturn();
            GenReturn.error       = true;
            GenReturn.messageList = new List<string>();
            GenReturn.info        = new object();

            // chamando um metodo NewInstance que eh estatico e retorna um obj prenchido a partir do tu manda por parametro
            GenericReturn.NewInstance(
                true,
                new List<string> {
                    "Erro", 
                    "Acerto"
                },
                new {
                    valor = 1
                }
            );

            // usando metodos extensions fluent
            GenReturn = GenReturn.SetError(true)
                                 .AddMessage("Value1")
                                 .AddMessage(
                                    "Value2", 
                                    "value2",
                                    "value2"
                                 )
                                 .AddInfo(
                                     new {
                                         value1 = 5,
                                         value2 = "str"
                                     }
                                 );

        }
    }

}