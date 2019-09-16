using System;
using System.Collections.Generic;
using System.Linq;

using UTIL.UtilClasses;

namespace UTIL.Utils {
    
    public static class UtilEnums {
        
        public static int ToInt<T>(this T soure) where T : struct, IConvertible {
            
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            try {
                return (int) (IConvertible) soure;
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

            try {
                return ((byte) (IConvertible) soure).toInt();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            
            return 0;
        }
        
        public static int Count<T>(this T soure) where T : struct, IConvertible {
            
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return Enum.GetNames(typeof(T)).Length;
        }
        
        public static IDictionary<int, string> toDictionary<T>(this T soure) where T : struct, IConvertible {
            
            var dict = Enum.GetValues(typeof(T))
                           .Cast<T>()
                           .ToDictionary(t => t.ToInt(), t => t.ToString());

            return dict;
        }
        
        /// <summary>
        /// Transforma um Enum em uma lista, facilita o trabalho de criação de helpers ou classificações de entidades
        /// </summary>
        /// <param name="soure">Enum qualquer</param>
        /// <typeparam name="T">Tipo do Enum qualquer</typeparam>
        /// <returns>lista de Objetos anônimo</returns>
        /// <exception cref="ArgumentException">Caso o parametro T informado não seja Um Enum</exception>
        public static IList<KeyValue> toList<T>(this T soure) where T : struct, IConvertible {
            
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T Precisa ser do Tipo enum");
            
            IList<KeyValue> listaItensEnum = new List<KeyValue>();
            
            foreach (var Item in Enum.GetValues(typeof(T))) {
                KeyValue itemToList = null;

                try {
                    itemToList = new KeyValue {
                        key = ((byte) Item).ToString(), value = Item.ToString().showEnumName()
                    };
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }

                try {
                    itemToList = new KeyValue {
                        key = ((int) Item).ToString(), value = Item.ToString().showEnumName()
                    };
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            
                listaItensEnum.Add(itemToList);
            }
            
            return listaItensEnum;
        }
    }
}