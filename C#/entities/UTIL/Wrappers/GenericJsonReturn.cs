using System.Collections.Generic;

namespace UTIL.Wrappers {

    public class GenericJsonReturn {
        public bool          error       { get; set; }
        public string        message     { get; set; }
        public IList<string> listMessage { get; set; }
        public object        extraInfo   { get; set; }

        public GenericJsonReturn() {
            this.listMessage = new List<string>();
        }
    }

    public class JsonMessageStatus : GenericJsonReturn {
        public string active { get; set; }
    }

}