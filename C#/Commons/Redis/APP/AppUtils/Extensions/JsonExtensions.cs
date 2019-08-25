using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace APP.AppUtils.Extensions {

    public static class JsonExtensions {

        public static T ToJsonObject<T>(this object obj) {

            var output = JsonConvert.SerializeObject(typeof (T));

            var deserializedProduct = JsonConvert.DeserializeObject<T>(output);

            return deserializedProduct;
        }

        public static IList<T> ToListJsonObject<T>(this object obj) {

            var output = JsonConvert.SerializeObject(typeof (T));

            var deserializedProduct = JsonConvert.DeserializeObject<List<T>>(output);

            return deserializedProduct;
        }

        public static string ToJson<T>(this object obj) {

            return JsonConvert.SerializeObject(typeof (T));
        }
    }

}