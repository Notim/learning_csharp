using System;
using System.Globalization;

namespace UTIL.Utils {

    public static class UtilNumber {
        public static int toInt(this string value) {
            int.TryParse(value, out var ret);
            
            return ret;
        }
        public static int toInt(this int? value) {
            int.TryParse(value.ToString(), out var ret);
            
            return ret;
        }
        public static int toInt(this byte? value) {
            int.TryParse(value.ToString(), out var ret);
            
            return ret;
        }
        public static int toInt(this byte value) {
            int.TryParse(value.ToString(), out var ret);
            
            return ret;
        }
        
        public static int toInt32(this long? value) {
            int.TryParse(value.ToString(), out var ret);
            
            return ret;
        }
        public static int toInt32(this long value) {
            int.TryParse(value.ToString(), out var ret);
            
            return ret;
        }

        public static long toInt64(this string value) {
            long.TryParse(value, out var ret);
            
            return ret;
        }
        public static long toInt64(this long? value) {
            long.TryParse(value.ToString(), out var ret);
            
            return ret;
        }
        public static long toInt64(this int? value) {
            long.TryParse(value.ToString(), out var ret);
            
            return ret;
        }
        public static long toInt64(this int value) {
            long.TryParse(value.ToString(), out var ret);
            
            return ret;
        }
        
        public static double toDouble(this string str) {
            double.TryParse(str, out var result);
            
            return result;
        }

        public static decimal toDecimal(this string str) {

            decimal.TryParse(str, out var result);
            
            return result;
        }
        public static decimal toDecimal(this decimal? valor) {
            decimal result = 0;

            if (valor == null) {
                return result;
            }
            
            return toDecimal(valor.ToString());
        }
        public static decimal toDecimal(this byte? number) {
            var result = new decimal(number ?? 0);

            return result;
        }
        public static decimal toDecimal(this byte number) {
            var result = new decimal(number);

            return result;
        }
        public static decimal toDecimal(this int? number) {
            var result = new decimal(number ?? 0);

            return result;
        }
        public static decimal toDecimal(this int number) {
            var result = new decimal(number);

            return result;
        }
        
        public static short toShort(this string value) {
            short.TryParse(value, out var result);
            
            return result;
        }
        public static short toShort(this short? value) {
            short.TryParse(value.ToString(), out var retorno);

            return retorno;
        }

        public static byte? toByte(this string value) {
            if (value.isEmpty()) {
                return null;
            }

            byte.TryParse(value, out var retorno);

            return retorno;
        }
        public static byte  toByte(this byte? value) {
            byte.TryParse(value.ToString(), out var retorno);

            return retorno;
        }
        public static byte  toByte(this int value) {
            byte.TryParse(value.ToString(), out var retorno);

            return retorno;
        }
        public static byte  toByte(this object objValue) {
            Byte.TryParse(objValue.ToString(), out var retorno);

            return retorno;
        }

        public static decimal toPorcentagem(this decimal valor, decimal valorTotal) {
            if (valorTotal == 0) {
                return 0;
            }

            decimal porcentagem = (valor / valorTotal) * 100;

            return Math.Round(porcentagem, 2);
        }
        public static decimal toPorcentagem(this int valor, int valorTotal) {
            if (valorTotal == 0) {
                return 0;
            }

            decimal valorDecimal      = new Decimal(valor);
            decimal valorTotalDecimal = new Decimal(valorTotal);

            decimal porcentagem = Decimal.Divide(valorDecimal, valorTotalDecimal) * 100;

            return Math.Round(porcentagem, 2);
        }
        
        public static string  toDecimalPoint(this string value, string qtdeCasas = "2") {
            return value.toDecimal().ToString($"F{qtdeCasas}", CultureInfo.InvariantCulture);
        }
        
        public static decimal valorPercentual(this decimal valor, decimal percent) {
            if (percent == 0) {
                return new decimal(0);
            }
            
            var fatorPercent   = decimal.Divide(percent, new decimal(100));
            var valorCalculado = decimal.Multiply(valor, fatorPercent);

            return valorCalculado;
        }
        
        public static decimal toDecimalMod100(string str) {
            str = str.Replace(",", "").Replace(".", "");
            var intValue = str.toInt();
            
            decimal.TryParse(decimal.Divide(intValue, 100).ToString(CultureInfo.InvariantCulture), out var result);

            return result;
        }
        
        public static decimal percentual(decimal total, decimal parcial) {
            if (total == 0) {
                return new decimal(0);
            }

            var fator   = decimal.Divide(100, total);
            var percent = decimal.Multiply(fator, parcial);

            return percent;
        }
        
        public static decimal toDecimalMod100(this int? value) {
            if (value == null)
                return 0;

            var valorRetorno = decimal.Divide(new decimal(value.toInt()), new decimal(100));

            return valorRetorno;
        }
        public static decimal toDecimalMod100(this int value) {
            var valorRetorno = decimal.Divide(new decimal(value), new decimal(100));

            return valorRetorno;
        }
        
        public static int toCents(decimal? valorTotal) {
            var valorCentavos = valorTotal.ToString().onlyNumber();
            
            return valorCentavos.toInt();
        }
        
        public static string toFullNumber(this decimal? valor) {
            return toFullNumber(valor.toDecimal());
        }
        
        public static string toFullNumber(this decimal valor) {
            if (valor <= 0 | valor >= 1000000000000000) {
                return "Valor não suportado pelo sistema.";
            } 
            var strValor        = valor.ToString("000000000000000.00");
            var valorPorExtenso = string.Empty;

            for (var i = 0; i <= 15; i += 3) {
                
                valorPorExtenso += escreveValorExtenso(Convert.ToDecimal(strValor.Substring(i, 3)));

                if (i == 0 & valorPorExtenso != string.Empty) { 
                    if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                        valorPorExtenso += " trilhão" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                    
                    else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                        valorPorExtenso += " trilhões" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                    
                } else if (i == 3 & valorPorExtenso != string.Empty) { 
                    if (Convert.ToInt32(strValor.Substring(3, 3)) == 1) 
                        valorPorExtenso += " bilhão" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                    
                    else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                        valorPorExtenso += " bilhões" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                    
                } else if (i == 6 & valorPorExtenso != string.Empty) {
                    if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                        valorPorExtenso += " milhão" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                    
                    else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1) 
                        valorPorExtenso += " milhões" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                    
                } else if (i == 9 & valorPorExtenso != string.Empty) {
                    if (Convert.ToInt32(strValor.Substring(9, 3)) > 0) 
                        valorPorExtenso += " mil" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " e " : string.Empty);
                }
                
                switch (i) {
                    case 12: {
                        if (valorPorExtenso.Length > 8) {
                            if (valorPorExtenso.Substring(valorPorExtenso.Length - 6, 6) == "bilhão" | valorPorExtenso.Substring(valorPorExtenso.Length - 6, 6) == "milhão")
                                valorPorExtenso += " de";
                            else
                            if (valorPorExtenso.Substring(valorPorExtenso.Length - 7, 7) == "bilhões" | valorPorExtenso.Substring(valorPorExtenso.Length - 7, 7) == "milhões" | valorPorExtenso.Substring(valorPorExtenso.Length - 8, 7) == "trilhões")
                                valorPorExtenso += " de";
                            else {
                                if (valorPorExtenso.Substring(valorPorExtenso.Length - 8, 8) == "trilhões") {
                                    valorPorExtenso += " de";
                                }
                            }
                        }
                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1) {
                            valorPorExtenso += " real";
                        } else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1) {
                            valorPorExtenso += " reais";
                        }
                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valorPorExtenso != string.Empty){
                            valorPorExtenso += " e ";
                        }
                    } break;
                    case 15 when Convert.ToInt32(strValor.Substring(16, 2)) == 1: {
                        valorPorExtenso += " centavo";
                    } break;
                    case 15: {
                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 1) 
                            valorPorExtenso += " centavos";
                    } break;
                }
            }

            return valorPorExtenso;
        }
        
        public static string escreveValorExtenso(decimal valor) {
            if (valor >= 1000) {
                return "Valor não suportado pelo sistema.";
            }
            if (valor <= 0) {
                return string.Empty;
            }
            else {
                var montagem = string.Empty;

                if (valor > 0 & valor < 1) {
                    valor *= 100;
                }

                var strValor = valor.ToString("000");
                var a        = Convert.ToInt32(strValor.Substring(0, 1));
                var b        = Convert.ToInt32(strValor.Substring(1, 1));
                var c        = Convert.ToInt32(strValor.Substring(2, 1));

                switch (a) {
                    case 1: montagem += (b + c == 0) ? "cem" : "cento"; break;
                    case 2: montagem += "duzentos";     break;
                    case 3: montagem += "trezentos";    break;
                    case 4: montagem += "quatrocentos"; break;
                    case 5: montagem += "quinhentos";   break;
                    case 6: montagem += "seiscentos";   break;
                    case 7: montagem += "setecentos";   break;
                    case 8: montagem += "oitocentos";   break;
                    case 9: montagem += "novecentos";   break;
                }

                switch (b) {
                    case 1: {
                        switch (c) {
                            case 0: montagem += ((a > 0) ? " e " : string.Empty) + "dez";       break;
                            case 1: montagem += ((a > 0) ? " e " : string.Empty) + "onze";      break;
                            case 2: montagem += ((a > 0) ? " e " : string.Empty) + "doze";      break;
                            case 3: montagem += ((a > 0) ? " e " : string.Empty) + "treze";     break;
                            case 4: montagem += ((a > 0) ? " e " : string.Empty) + "quatorze";  break;
                            case 5: montagem += ((a > 0) ? " e " : string.Empty) + "quinze";    break;
                            case 6: montagem += ((a > 0) ? " e " : string.Empty) + "dezesseis"; break;
                            case 7: montagem += ((a > 0) ? " e " : string.Empty) + "dezessete"; break;
                            case 8: montagem += ((a > 0) ? " e " : string.Empty) + "dezoito";   break;
                            case 9: montagem += ((a > 0) ? " e " : string.Empty) + "dezenove";  break;
                        }
                    } break;
                    case 2: montagem += ((a > 0) ? " e " : string.Empty) + "vinte";     break;
                    case 3: montagem += ((a > 0) ? " e " : string.Empty) + "trinta";    break;
                    case 4: montagem += ((a > 0) ? " e " : string.Empty) + "quarenta";  break;
                    case 5: montagem += ((a > 0) ? " e " : string.Empty) + "cinquenta"; break;
                    case 6: montagem += ((a > 0) ? " e " : string.Empty) + "sessenta";  break;
                    case 7: montagem += ((a > 0) ? " e " : string.Empty) + "setenta";   break;
                    case 8: montagem += ((a > 0) ? " e " : string.Empty) + "oitenta";   break;
                    case 9: montagem += ((a > 0) ? " e " : string.Empty) + "noventa";   break;
                }
                
                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty)
                    montagem += " e ";

                if (strValor.Substring(1, 1) != "1")
                    switch (c) {
                        case 1: montagem += "um";     break;
                        case 2: montagem += "dois";   break;
                        case 3: montagem += "três";   break;
                        case 4: montagem += "quatro"; break;
                        case 5: montagem += "cinco";  break;
                        case 6: montagem += "seis";   break;
                        case 7: montagem += "sete";   break;
                        case 8: montagem += "oito";   break;
                        case 9: montagem += "nove";   break;
                    }

                return montagem;
            }
        }
    }
}