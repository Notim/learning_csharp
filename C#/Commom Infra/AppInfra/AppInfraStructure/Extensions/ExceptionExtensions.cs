using System;
using System.Net;

using WEB.AppInfrastructure.Core.Config;
using WEB.AppInfrastructure.Utils;

namespace WEB.AppInfraStructure.Extensions {

    public static class ExceptionExtensions {
        /// <summary>
        /// M�todo de extens�o para tratamento de erros, geralmente em try/catchs,
        /// A fun��o dele � formatar um erro para alguma sa�da que dependa que o retorno seja mais descritiva,
        /// por�m esses erros n�o podem chegar at� o usu�rio final em produ��o.
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