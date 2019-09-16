using System.Collections.Generic;

using FluentValidation.Results;

namespace WEB.AppInfraStructure.Extensions {

    public static class ValidationResultExtensions {

        // Capturar os erros 
        public static IList<string> retornarErros(this ValidationResult Validation) {

            IList<string> erros = new List<string>();

            foreach (var err in Validation.Errors) {
                erros.Add(err.ErrorMessage);
            }

            return erros;
        }
    }

}