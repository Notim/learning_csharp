using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UTIL.Utils {

    public static class UtilObject {
        
        /// <summary>
        /// Coleta todas as propriedades que estão nulas 
        /// </summary>
        public static List<PropertyInfo> nullFields(this object obj) {

            var fieldList = typeof (object).GetProperties();

            return fieldList.Where(Field => Field.GetValue(obj, null) == null).ToList();
        }
        
        public static void setDeepProperty(this object instance, string path, object value) {
            var pp = path.Split('.');
            
            var t = instance.GetType();

            PropertyInfo propInfo = null;

            for (var i = 0; i < pp.Length; i++) {
                var prop = pp[i];
                var propName = prop;
                var indexLista = prop.IndexOf('[');

                if (indexLista > -1) {
                    propName = prop.Substring(0, indexLista);
                }

                propInfo = t.GetProperty(propName);

                if (propInfo == null) {
                    continue;
                }

                // Se o loop chegou ao fim, não é necessário buscar os dados abaixo
                if ((i + 1) == pp.Length) {
                    break;
                }
                
                instance = propInfo.GetValue(instance, null);

                t = propInfo.PropertyType;

                if (instance is ICollection) {
                    var posicaoItemLista = prop.Substring(indexLista).onlyNumber().toInt();

                    if (instance is IList list) {
                        instance = list[posicaoItemLista];

                        t = instance.GetType();
                    }
                }
            }

            if (instance == null || propInfo == null) {

                return;
            }

            var paramType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

            if (paramType == typeof(int) && value.isEmpty()) {

                value = "0";
            }

            var valueConverted = Convert.ChangeType(value, paramType);

            propInfo.SetValue(instance, valueConverted);

        }
    }
}
