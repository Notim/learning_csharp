using System.Linq;

namespace UTIL.Wrappers {

    public static class GenericReturnExtensions {
        public static GenericReturn SetError(this GenericReturn GenRet, bool error) {
            GenRet.error = error;

            return GenRet;
        }

        public static GenericReturn AddMessage(this GenericReturn GenRet, string message) {
            GenRet.messageList.Add(message);

            return GenRet;
        }
        
        public static GenericReturn AddMessage(this GenericReturn GenRet, params string[] messages) {
            foreach (var message in messages) {
             
                GenRet.messageList.Add(message);
            }

            return GenRet;
        }

        public static GenericReturn AddInfo(this GenericReturn GenRet, object info) {
            GenRet.info = info;

            return GenRet;
        }
    }

}