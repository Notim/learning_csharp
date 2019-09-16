using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using FluentValidation.Results;

namespace WEB.AppInfraStructure.Extensions {
    
    public static class ModelStateExtensions {

        // Capturar os erros 
        public static string retornarErros(this ModelStateDictionary OModelState) {
            
            string erros = "";
            
            var query = from state in OModelState.Values  
                        from error in state.Errors  
                        select error.ErrorMessage;

            erros = String.Join("<br />", query.ToList());
            
            return erros;
        }
        
        // Capturar os erros 
        public static void carregarErros(this ModelStateDictionary OModelState, IList<ValidationFailure> listaErros, string prefixProperty) {

            if (!listaErros.Any()) {
                return;
            }

            foreach (var ItemErro in listaErros) {

                string property = $"{prefixProperty}{ItemErro.PropertyName}";
                
                OModelState.AddModelError(property, ItemErro.ErrorMessage);
                
            }
        }

    }
}