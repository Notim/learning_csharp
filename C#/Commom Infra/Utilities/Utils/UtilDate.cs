using System;
using System.Globalization;

namespace UTIL.Utils {

    public static class UtilDate {

        public static DateTime? toDateTime(this string value) {
            return DateTime.TryParse(value, out var date) ? date : (DateTime?) null;
        }

        public static DateTime cast(string date) {
            var result = DateTime.MinValue;

            if (string.IsNullOrEmpty(date)) {
                return result;
            }

            DateTime.TryParse(date.Trim(), out result);
            
            return result;
        }

        public static DateTime? getDate(string strDate, string strFormat) {
            DateTime? result = null;

            if (strFormat.ToLower() == "ddmmyy") {
                if (strDate.Length >= 6) {
                    var day   = strDate.Substring(0, 2);
                    var month = strDate.Substring(2, 2);
                    var year  = $"20{strDate.Substring(4, 2)}";

                    result = new DateTime(year.toInt(), month.toInt(), day.toInt());
                }
            }

            return result;
        }

        public static bool isValid(string date) {
            return DateTime.TryParse(date.Trim(), out _);
        }

        public static bool isFutureDate(DateTime? date) {
            if (date == null) return false;
            
            return (date >= DateTime.Today);
        }

        public static string toDisplay(string date) {
            if (string.IsNullOrEmpty(date)) return "";

            DateTime.TryParse(date.Trim(), out var result);
            
            return result.ToShortDateString();
        }

        //Verificar a diferença de dias considerando apenas dias úteis
        public static int diffDaysWeekDays(DateTime initialDate, DateTime finalDate, bool saturdayUtil = false) {
            var daysCount = 0;
            var days = initialDate.Subtract(finalDate).Days;
            
            if (days < 0)
                days *= -1;

            for (var i = 1; i <= days; i++) {
                initialDate = initialDate.AddDays(1);

                if (initialDate.DayOfWeek != DayOfWeek.Sunday) {
                    if (initialDate.DayOfWeek != DayOfWeek.Saturday || saturdayUtil) {
                        daysCount++;
                    }
                }
            }

            return daysCount;
        }
        
        public static string fullMonth(int mes) {
            
            string mesConvertido;
            switch (mes) {
                case 1:  mesConvertido = "Janeiro"; break;
                case 2:  mesConvertido = "Fevereiro"; break;
                case 3:  mesConvertido = "Março"; break;
                case 4:  mesConvertido = "Abril"; break;
                case 5:  mesConvertido = "Maio"; break;
                case 6:  mesConvertido = "Junho"; break;
                case 7:  mesConvertido = "Julho"; break;
                case 8:  mesConvertido = "Agosto"; break;
                case 9:  mesConvertido = "Setembro"; break;
                case 10: mesConvertido = "Outubro"; break;
                case 11: mesConvertido = "Novembro"; break;
                case 12: mesConvertido = "Dezembro"; break;
                default: mesConvertido = "Janeiro"; break;
            }
            return mesConvertido;
        }

        // Exibir uma data por escrito por extenso
        public static string exibirExtenso(DateTime data, string idioma = "pt-BR") {
            // Mês INT
            var mes = data.Month;
            
            var Cultura = new CultureInfo(idioma);

            // Mês abreviado em português também.
            Cultura.DateTimeFormat.GetAbbreviatedMonthName(mes);

            // Mês (int) por extenso com primeira letra maiúscula.
            var month = Cultura.DateTimeFormat.GetMonthName(mes);
            var mesExtenso = char.ToUpper(month[0]) + month.Substring(1);

            // Dia da semana (int) por extenso em português (segunda-feira)
            var diaExtenso = Cultura.DateTimeFormat.GetDayName(data.DayOfWeek);

            // Dia da semana abreviado (seg)
            Cultura.DateTimeFormat.GetAbbreviatedDayName(data.DayOfWeek);

            var retorno = string.Concat(diaExtenso, ", ", data.Day.ToString(), " de ", mesExtenso, " de ", data.Year.ToString());
            
            return retorno;
        }

        //Retorno o Datetime do próximo dia da semana (ex: próxima sexta, próximo sábado, etc)
        public static DateTime getNextDay(DateTime startDate, DayOfWeek day) {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            var daysUntil = ((int) day - (int) startDate.DayOfWeek + 7) % 7;

            var nextDay = startDate.AddDays(daysUntil);

            return nextDay;
        }

        // Retorno o Datetime do próximo dia da semana (ex: próxima sexta, próximo sábado, etc)
        public static DateTime getNextDay(int dayOfWeek) {
            var today     = DateTime.Today;
            var daysUntil = (dayOfWeek - (int) today.DayOfWeek + 7) % 7;
            var nextDay   = today.AddDays(daysUntil);
            
            return nextDay;
        }

        // Retorno o Datetime do próximo dia da semana (ex: próxima sexta, próximo sábado, etc)
        public static DateTime getNextBusinessDay(bool flagIncludeSaturday = false, int qtdeDias = 1) {
            var nextDay = DateTime.Today.AddDays(qtdeDias);

            switch (nextDay.DayOfWeek) {
                case DayOfWeek.Sunday:                               return nextDay.AddDays(1);
                case DayOfWeek.Saturday when (!flagIncludeSaturday): return nextDay.AddDays(2);
                default:                                             return nextDay;
            }
        }
        
        public static string calcularIdade(DateTime? dtNascimento) {

            var dAtual = DateTime.Today;

            var idAnos = 0;
            var ta = "-";

            if (!dtNascimento.HasValue || dAtual < dtNascimento) {
                return ta;
            }

            if (dAtual.Month < dtNascimento.Value.Month) {
                idAnos = -1;
            }

            idAnos = dAtual.Year - dtNascimento.Value.Year + idAnos;

            if (idAnos > 1) {
                ta = idAnos + " anos ";
            }
            else {
                ta = idAnos + "ano";
            }

            return ta;
        }

        /// <summary>
        /// Receber uma string de data e tentar converter para tipo DateTime
        /// </summary>
        public static DateTime? dateFromUTC(string strDate, string strFormat = "yyyy-MM-ddTHHmmsszzz", int fusoHorario = -3) {

            if (DateTimeOffset.TryParse(strDate, out var dtConvert)) {
                return dtConvert.DateTime.AddHours(fusoHorario);
            }

            return null;
        }
        
        public static string exibirData(this DateTime? dtPadrao, bool incluirHorario = false, string dataVazia = "..") {
            var txt = dataVazia;

            if (dtPadrao.isEmpty())
                return txt;

            txt = dtPadrao?.ToShortDateString();

            if (incluirHorario) {
                txt = string.Concat(txt, " ", dtPadrao?.ToShortTimeString());
            }

            return txt;
        }

        /// <summary>
        /// Converter data em padrao timestamp (modo Fluent)
        /// </summary>
        public static double toUnixTimestamp(this DateTime dateTime) {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        /// <summary>
        /// retorna a diferenca entre datas em meses (modo Fluent)
        /// </summary>
        /// <param name="dataFinal">data Final</param>
        /// <param name="dataInicial">data Inicial</param>
        public static int diferencaMeses(this DateTime dataFinal, DateTime dataInicial) {
            var valorEmMeses = dataFinal.diferencaDias(dataInicial) / 30;

            return valorEmMeses;
        }

        /// <summary>
        /// retorna a diferenca entre datas em dias (modo Fluent)
        /// </summary>
        /// <param name="dataFinal">data Final</param>
        /// <param name="dataInicial">data Inicial</param>
        public static int diferencaDias(this DateTime dataFinal, DateTime dataInicial) {
            var valorEmDias = diffDias(dataFinal, dataInicial);

            return valorEmDias;
        }

        /// <summary>
        /// retorna a diferenca entre datas em dias (modo procedural)
        /// </summary>
        /// <param name="dataFinal">data Final</param>
        /// <param name="dataInicial">data Inicial</param>
        public static int diffDias(DateTime dataFinal, DateTime dataInicial) {
            var valorEmDias = (dataFinal - dataInicial).Days;

            return valorEmDias;
        }

        /// <summary>
        /// retorna a diferenca entre datas em meses (modo Fluent)
        /// </summary>
        /// <param name="dataFinal">data Final</param>
        /// <param name="dataInicial">data Inicial</param>
        public static int diferencaMeses(this DateTime? dataFinal, DateTime? dataInicial) {
            var valorEmMeses = dataFinal.diferencaDias(dataInicial) / 30;

            return valorEmMeses;
        }

        /// <summary>
        /// retorna a diferenca entre datas em dias (modo Fluent)
        /// </summary>
        /// <param name="dataFinal">data Final</param>
        /// <param name="dataInicial">data Inicial</param>
        public static int diferencaDias(this DateTime? dataFinal, DateTime? dataInicial) {
            var valorEmDias = diffDias(dataFinal, dataInicial);

            return valorEmDias;
        }

        /// <summary>
        /// retorna a diferenca entre datas em dias (modo procedural)
        /// </summary>
        /// <param name="dataFinal">data Final</param>
        /// <param name="dataInicial">data Inicial</param>
        public static int diffDias(DateTime? dataFinal, DateTime? dataInicial) {
            var valorEmDias = ((dataFinal ?? DateTime.Now) - (dataInicial ?? DateTime.Now)).Days;

            return valorEmDias;
        }

    }

}