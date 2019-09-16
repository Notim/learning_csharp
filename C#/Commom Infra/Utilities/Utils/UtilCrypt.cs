using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UTIL.Utils {

    public class UtilCrypt {

        public const string saltSistema   = "SINC_2013_GROUP_BLA*9!##XYZ";
        public const long   fatorNumerico = 10137;
        
        public static string SHA512(string str) {
            
            var cryprStr = string.Concat(saltSistema, str);
            
            var UnicodeEncoding = new UnicodeEncoding();
            var MessageBytes    = UnicodeEncoding.GetBytes(cryprStr);

            var SHhash    = new SHA512Managed();
            var HashValue = SHhash.ComputeHash(MessageBytes);

            return HashValue.Aggregate("", (current, b) => current + $"{b:x2}");
        }
        
        public static string toBase64Encode(int value) {
            
            var strToEncode = (value * fatorNumerico);
            var toEncodeAsBytes = Encoding.Unicode.GetBytes(strToEncode.ToString());
            var returnValue = Convert.ToBase64String(toEncodeAsBytes);
            
            return returnValue.Trim();
        }

        public static string toBase64Decode(string encodedString) {
            if (encodedString.LengthNullable() % 4 != 0) {
                
                return string.Empty;
            }

            var data = Convert.FromBase64String(encodedString);

            var strToDecode = Encoding.Unicode.GetString(data);

            var value = (strToDecode.toInt64() / fatorNumerico);

            return value.ToString();
        }

        public static string signRecipe(string prefix, string value) {
            var sign = $"{prefix}_{value.Replace("=", "$#")}";
            
            return sign;
        }
    }
}
