using System;
using System.Net;

using WEB.AppInfrastructure.Core.Config;
using WEB.AppInfrastructure.Utils;

namespace WEB.AppInfraStructure.Extensions {

    public static class ExceptionExtensions {
        /// <summary>
        /// Método de extensão para tratamento de erros, geralmente em try/catchs,
        /// A função dele é formatar um erro para alguma saída que dependa que o retorno seja mais descritiva,
        /// porém esses erros não podem chegar até o usuário final em produção.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <returns>Erro mais descritivo caso o aplicativo esteja em ambiente de desenvolvimento.</returns>
        public static string getLogError(this Exception ex, string message) {

            var completeMessage = $"{message} ==> {ex.Source}, {ex.Message}, {ex.StackTrace}";
            
            ex.saveError(completeMessage);

            return UtilConfig.emProducao() ? message : completeMessage;
        }
        
        public static string getLogError(this ArgumentException ex, string message) {
            
            var completeMessage = $"{message} :: {ex.ParamName}, {ex.Source}, {ex.Message}, {ex.StackTrace}";
            
            ex.saveError(completeMessage);

            return UtilConfig.emProducao() ? message : completeMessage;
        }
        
        public static string getLogError(this WebException ex, string message) {

            var completeMessage = $"{message} :: {ex.Status}, {ex.Response}, {ex.Source}, {ex.Message}, {ex.StackTrace}";
            
            ex.saveError(completeMessage);

            return UtilConfig.emProducao() ? message : completeMessage;
        }
    }

}