using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace UTIL.Utils {

    public static class UtilCollection {
        
        public static string getIfExists(this NameValueCollection dados, string key, string valorPadrao = "") {
            if (dados == null) {
                return "";
            }

            if (!dados[key].isEmpty()) {
                return dados[key];
            }

            if (!valorPadrao.isEmpty()) {
                return valorPadrao;
            }

            return string.Empty;
        }

        public static List<T> ToListNullable<T>(this IEnumerable<T> obj) {
            return obj == null ? new List<T>() : obj.ToList();
        }

        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default) {
            return dict.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public static TValue getValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : class {
            return dict.TryGetValue(key, out var value) ? value : null;
        }
    }

}