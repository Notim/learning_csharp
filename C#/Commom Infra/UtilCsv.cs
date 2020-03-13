using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UTIL.Excel {
    
    /// <summary>
    /// classe de auxilio para criação de CSV de forma mais concisa e padronizada, porém para correto funcionamento precisa seguir umas regras
    /// Tem que criar uma classe DTO POCO com os campos que serão renderizados no CSV, ele irá criar o header do csv com os nome dos campos
    /// > Quando for necessário usar siglas no titulo da coluna tem que usar o padrão camel case, há um tratamento para isso,
    /// > Quando o valor se trata de um valor monetário precisa colocar o sufixo
    /// </summary>
    public static class UtilCsv {
        /// <summary>
        /// Gera uma linha para cada registro
        /// </summary>
        public static string gerarCabecalhoCsv<T>(string[] ignoreProperties = null)  {
            var properties = typeof(T).GetProperties().Select(p => p.Name).ToArray();
            
            if(ignoreProperties != null && ignoreProperties.Any()) {
                properties = properties.Where(field => !ignoreProperties.Contains(field)).ToArray();
            }

            var cabecalho = properties.Aggregate("", (all, current) => all + (current.splitByUpperCase()  + ";"));

            return cabecalho;
        }

        /// <summary>
        /// Gera uma linha para cada registro
        /// </summary>
        public static string gerarLinhaCsv<T>(object registro, string[] ignoreProperties = null) {
            if (!((T) registro).GetType().GetProperties().Any()) {
                throw new ArgumentException($"the type { typeof(T).FullName } doesn't match with argument sent");
            }
            
            var properties = ((T) registro).GetType().GetProperties().Select(p => p.Name).ToArray();
            
            if(ignoreProperties != null && ignoreProperties.Any()) {
                properties = properties.Where(field => !ignoreProperties.Contains(field)).ToArray();
            }

            var valores = properties.Select(propertieName => {
                                                var valorCampo = registro.GetType().GetProperty(propertieName)?.GetValue(registro, null);
                                                if (propertieName.Contains("valor") && valorCampo is decimal money) {
                                                    return money.ToString("C");
                                                }
                                                return valorCampo;
                                            }).ToList();

            var linha = "";
            valores.ForEach(valor => linha += valor + ";");

            return linha;
        }

        /// <summary>
        /// gera um csv a partir de uma lista de um tipo qualquer 
        /// </summary>
        public static string gerarConteudoCsv<T>(IList<T> registros, string[] ignoreProperties = null) {
            var csv = new StringBuilder();

            csv.Append(gerarCabecalhoCsv<T>(ignoreProperties) + "\r\n");

            foreach (var registro in registros) {
                csv.Append(gerarLinhaCsv<T>(registro, ignoreProperties) + "\r\n");
            }

            return csv.ToString();
        }
        
        /// <summary>
        /// searializa qualquer lista para formato CSV
        /// </summary>
        public static string toCsv<T>(this IList<T> registros, string[] ignoreProperties = null) {
            
            var csv = gerarConteudoCsv<T>(registros, ignoreProperties);
            
            return csv;
        }

    }

}