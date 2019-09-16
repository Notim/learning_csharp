namespace UTIL.Utils {

    public static class UtilConvert {
        
        public static bool? toBool(this string str) {

            if (string.IsNullOrEmpty(str)) {
                return null;
            }

            if (bool.TryParse(str, out var retorno)) {
                return retorno;
            }

            return null;
        }

        public static bool toBool(this bool? value) {
            var retorno = false;

            if (value != null) {
                bool.TryParse(value.ToString(), out retorno);
            }

            return retorno;
        }
    }

}