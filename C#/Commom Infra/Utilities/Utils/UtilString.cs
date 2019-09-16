using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Newtonsoft.Json.Linq;

namespace UTIL.Utils {

    public static class UtilString {
        
        public static string onlyNumber(this string str) {
            return (str.isEmpty()) ? "" : Regex.Replace(str, @"[^\d]", String.Empty);
        }
        
        public static string onlyAlphaNumber(this string str) {
            return (str.isEmpty()) ? "" : Regex.Replace(str, @"[^\w\s]", String.Empty);
        }

        public static string onlyEmailChars(this string str) {
            return (str.isEmpty())? "": Regex.Replace(str, @"[^a-zA-Z0-9@._]", String.Empty);
        }

        // Devolve uma string sem acentos
        public static string cleanAccents(this string str) {
            if (string.IsNullOrEmpty(str)) {
                return String.Empty;
            }
            var bytes = Encoding.GetEncoding("iso-8859-8").GetBytes(str);
            
            return Encoding.UTF8.GetString(bytes);
        }

        // Devolve uma string sem acentos
        public static string cleanStringToURL(this string str) {
            if (string.IsNullOrEmpty(str)) {
                return String.Empty;
            }
            str = str.cleanAccents();
            str = Regex.Replace(str, "[^0-9a-zA-Z\\s]+", "");
            
            return str.Replace(" ", "-");
        }

        // Devolve um vazio caso a string seja nula
        public static string notNull(this string str) {
            str = str ?? "";
            return str;
        }

        public static string notNull(this object str) {
            str = str ?? "";
            return str.ToString();
        }
        
        public static string formatCPFCNPJ(this string dado) {
            var info = onlyNumber(dado);
            
            return toFormat(info, info.Length > 11 ? "##.###.###/####-##" : "###.###.###-##");
        }
        
        public static string formatPhone(this string dado) {
            var info = onlyNumber(dado);
            
            return toFormat(info, info.Length == 10 ? "## ####-####" : "## #####-####");
        }

        public static string formatCEP(this string dado) {
            if (string.IsNullOrEmpty(dado)) return "";
            
            var info = onlyNumber(dado);
            
            return toFormat(info, "#####-###");
        }

        public static string toFormat(this string valor, string mascara) {
            var dado = new StringBuilder();

            // remove caracteres nao numericos
            foreach (var c in valor.Where(char.IsNumber)) {
                dado.Append(c);
            }

            var indMascara = mascara.Length;
            var indCampo = dado.Length;

            for (;indCampo > 0 && indMascara > 0;) {
                if (mascara[--indMascara] == '#') indCampo--;
            }

            var saida = new StringBuilder();
            for (;indMascara < mascara.Length;indMascara++) {
                saida.Append((mascara[indMascara] == '#') ? dado[indCampo++] : mascara[indMascara]);
            }
            
            return saida.ToString();
        }
        
        public static string reverse(string s) {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            
            return new string(charArray);
        }
        
        public static string abreviarTextos(this string texto){

            texto = texto.ToUpper();

            texto = texto.Replace("RUA", "R");
            texto = texto.Replace("AVENIDA", "AV");
            texto = texto.Replace("RODOVIA", "ROD");
            texto = texto.Replace("ALAMEDA", "AL");
            texto = texto.Replace("PRAÇA", "PC");
            texto = texto.Replace("ENGENHEIRO", "ENG");
            
            return texto;
        }

        public static string randomString(this int strLength) {
            var Random = new Random();
            var seed = Random.Next(1, int.MaxValue);

            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            //const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            //const string specialCharacters = @"!#$%&'()*+,-./:;<=>?@[\]_";

            var chars = new char[strLength];
            var rd = new Random(seed);

            for (var i = 0;i < strLength;i++) {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public static string acertaCasas(this string str, int tamanho, string valorAdicionar = "0"){
            if (str.Length >= tamanho)
                return str;

            for (var x = str.Length; x < tamanho; x++) { 
                str = valorAdicionar + str;
            }

            return str;
        }
        
        public static string removeHtml(this string input) {
            if (input.isEmpty())
                return "";
            
            input = input.Replace("<br>", Environment.NewLine);
            
            return Regex.Replace(input, "<[^>]+>|&nbsp;", "").Trim();
        }
        
        public static string removeHtmlComments(string input) {
            if (input.isEmpty()) 
                return "";
            
            input = input.Replace("<br>", Environment.NewLine);
            
            return  Regex.Replace(input, "<!--(.|\\s)*?-->", "").Trim();
        }

        public static string limparParaCSV(string campo) {

            if (!string.IsNullOrEmpty(campo)) {
                return campo.Replace(Environment.NewLine, "")
                            .Replace("\t", "")
                            .Replace(";", "")
                            .Replace("<strong>", "")
                            .Replace("</strong>", "");
            }
            
            return string.Empty;
        }

        public static string onlyFirstCharUpper(string texto) {
            if (texto.isEmpty()){
                return string.Empty;
            }
            
            var upper = char.ToUpper(texto[0]) + texto.Substring(1).ToLower();
            
            return upper;
        }
        
        public static bool isValidJson(this string strInput) {
            try {
                JObject.Parse(strInput);
            }
            catch (Exception) {
                return false;
            }
            return true;
        }
        
        public static bool EqualsNullable(this object obj, object objComparacao) {
            if (obj == null && objComparacao == null) {
                return true;
            }
            
            if (obj == null && objComparacao != null) {
                return false;
            }
            
            return obj.Equals(objComparacao);
        }
        
        public static string TrimNullable(this string strValue) {
            return strValue == null ? "" : strValue.Trim();
        }
        
        public static string removerAcentuacao(this string value) {
            return value.cleanAccents();
        }
        
        public static string removerCaracteresEspeciais(this string value) {
            return Regex.Replace(value, "[^0-9a-zA-Z ]+", "");
        }
        
        public static string stringOrEmpty(this object str) {
            str = str ?? "";

            return str.ToString().Trim();
        }
        
        public static string stringOrEmptyUpper(this object str) {
            str = str ?? "";

            return str.ToString().Trim().ToUpper();
        }
        
        public static string stringOrEmptyLower(this object str) {
            str = str ?? "";

            return str.ToString().Trim().ToLower();
        }
        
        public static string removeTags(this string inputString) {
            inputString = (inputString ?? "");

            const string HTML_TAG_PATTERN = "<.*?>";

            return Regex.Replace(inputString, HTML_TAG_PATTERN, string.Empty);
        }
        
        public static string abreviar(this string str, int qtdeCaracteres, string strSufixo = "") {

            str = str.TrimNullable();

            if (string.IsNullOrEmpty(str)) {
                return string.Empty;
            }

            if (str.Length > qtdeCaracteres) {
                str = string.Concat(str.Substring(0, qtdeCaracteres), strSufixo);
            }

            return str;
        }
        
        public static bool isEmpty(this object strValue) {
            return string.IsNullOrEmpty(strValue?.ToString().Trim());
        }
        
        public static string toCamelCase(this string s) {
            if (string.IsNullOrEmpty(s)) {
                return string.Empty;
            }

            var a = s.ToCharArray();

            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }
        
        public static string toUppercaseWords(this string value) {

            value = value.stringOrEmpty();
                
            if (value.isEmpty()) {
                return "";
            }

            var array = value.Split(' ');

            var retorno = string.Empty;

            var wordsByPass = new[] {"de", "da", "dos", "do"};
            var wordsIgnore = new[] {"e", "s.a.", "co.", "me", "m.e", "m.e.", "sa", "s.a", "s.a.", "ltda"};

            foreach (var t in array) {
                var word = t.stringOrEmptyLower();
                if (word.isEmpty()) {
                    continue;
                }
                if (wordsByPass.Contains(word)) {
                    retorno = string.Concat(retorno, " ", word);
                    continue;
                }
                if (wordsIgnore.Contains(word)) {
                    retorno = string.Concat(retorno, " ", t);
                    continue;
                }
                retorno = string.Concat(retorno, " ", word.toCamelCase());
            }

            return retorno;
        }
        
        public static string defaultIfEmpty(this string str, string strDefault = "...") {
            str = str.stringOrEmpty();

            return str.isEmpty() ? strDefault : str;
        }
        
        public static List<string> ToOneList(this string strValue) {
            var lista = new List<string> {strValue};

            return lista;
        }
        
        public static List<string> ToEmailList(this string strValue, string separator = "") {
            var lista = new List<string>();

            if (string.IsNullOrEmpty(strValue)) {
                return lista;
            }

            if (!string.IsNullOrEmpty(separator)) {
                lista = strValue.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries).ToList();
            } else {
                lista.Add(strValue);
            }
            return lista;
        }
        
        public static string onlyAlphaSpace(this string value) {
            return Regex.Replace(value, "[^a-zA-Z]+", "");
        }
        
        public static string decodeString(this string value) {
            if (string.IsNullOrEmpty(value)) {
                return "";
            }

            value = value.Replace("&#39;", "'");

            return value;
        }
        
        public static int LengthNullable(this string strValue) {
            return strValue?.Length ?? 0;
        }
        
        public static string ToUpperNullable(this string strValue) {
            return strValue == null ? "" : strValue.ToUpper();
        }
        
        public static string nl2br(this string input){
            return input.nl2br(true);
        }
        
        public static string nl2br(this string input, bool is_xhtml) {
            return input.Replace("\r\n", is_xhtml ? "<br />\r\n" : "<br>\r\n");
        }
        
        public static string soundex(this string word) {
            
            const int MaxSoundexCodeLength = 4;
                
            var soundexCode = new StringBuilder();
            var previousWasHOrW = false;
            
            // Upper case all letters in word and remove any punctuation
            word = Regex.Replace(word == null ? string.Empty : word.ToUpper(), @"[^\w\s]", string.Empty);

            if (string.IsNullOrEmpty(word)) {
                return string.Empty.PadRight(MaxSoundexCodeLength, '0');
            }

            // Retain the first letter
            soundexCode.Append(word.First());

            for (var i = 1; i < word.Length; i++) {
                var numberCharForCurrentLetter = GetCharNumberForLetter(word[i]);

                // Skip this number if it matches the number for the first character
                if (i == 1 &&
                    numberCharForCurrentLetter == GetCharNumberForLetter(soundexCode[0]))
                    continue;

                // Skip this number if the previous letter was a 'H' or a 'W' 
                // and this number matches the number assigned before that
                if (soundexCode.Length > 2 && previousWasHOrW &&
                    numberCharForCurrentLetter == soundexCode[soundexCode.Length - 2])
                    continue;

                // Skip this number if it was the last added
                if (soundexCode.Length > 0 &&
                    numberCharForCurrentLetter == soundexCode[soundexCode.Length - 1])
                    continue;

                soundexCode.Append(numberCharForCurrentLetter);
                
                previousWasHOrW = "HW".Contains(word[i]);
                
            }
            
            return soundexCode
                              .Replace("0", string.Empty).ToString()
                              .PadRight(MaxSoundexCodeLength, '0')
                              .Substring(0, MaxSoundexCodeLength);
        }
        
        /// <summary>
        /// Retrieves the soundex number for a given letter.
        /// </summary>
        /// <param name="letter">Letter to get the soundex number for.</param>
        /// <returns>Soundex number (as a character) for letter.</returns>
        private static char GetCharNumberForLetter(char letter) {
            
            if ("BFPV".Contains(letter)) return '1';
            if ("CGJKQSXZ".Contains(letter)) return '2';
            if ("DT".Contains(letter)) return '3';
            if ('L' == letter) return '4';
            if ("MN".Contains(letter)) return '5';
            if ('R' == letter) return '6';

            return '0'; // i.e. letter is [AEIOUWYH]
        }
        
        public static string showEnumName(this string enumStr) {
            if (enumStr.isEmpty())
                return "";
            
            enumStr = enumStr.ToLower().toCamelCase().Replace("_", " ");

            return enumStr;
        }

        public static IList<string> stringToListString(this string valor){

            if (valor.isEmpty()) {
                return new List<string>();
            }

            string[] separadores = { "\r\n" };
            
            var listaValoresBusca = valor.Split(separadores, StringSplitOptions.None).Where(x => !x.isEmpty()).Distinct().ToListNullable();

            return listaValoresBusca;
        }
    }
}

