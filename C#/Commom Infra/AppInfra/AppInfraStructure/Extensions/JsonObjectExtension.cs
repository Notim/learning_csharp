using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using PagedList;

namespace WEB.AppInfraStructure.Extensions {

    public static class JsonObjectExtension {
        /// <summary>
        /// Permite pre selecionar campos de uma entidade e retonar a lista com o tipo de objeto especificado
        /// </summary>
        public static List<T> ToListJsonObject<T>(this IQueryable entity, bool? flagIgnoreObjects = false) {

            var settings = new JsonSerializerSettings();

            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            settings.NullValueHandling = NullValueHandling.Ignore;

            if (flagIgnoreObjects == true) {

                settings.ContractResolver = new IgnoreObjectsResolver();

            }

            settings.Converters.Add(new StringDecimalConverter());

            var json = JsonConvert.SerializeObject(entity, settings);

            return JsonConvert.DeserializeObject<List<T>>(json, settings);
        }

        /// <summary>
        /// Permite pre selecionar campos de uma entidade e retonar a lista com o tipo de objeto especificado
        /// </summary>
        public static List<T> ToListJsonObject<T>(this object list, bool? flagIgnoreObjects = false) {

            var settings = new JsonSerializerSettings();

            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            settings.NullValueHandling = NullValueHandling.Ignore;

            if (flagIgnoreObjects == true) {
                settings.ContractResolver = new IgnoreObjectsResolver();
            }

            settings.Converters.Add(new StringDecimalConverter());

            var json = JsonConvert.SerializeObject(list, settings);

            return JsonConvert.DeserializeObject<List<T>>(json, settings) ?? new List<T>();
        }

        /// <summary>
        /// Permite pre selecionar campos de uma entidade e retonar um objeto do tipo de objeto especificado
        /// </summary>
        public static T ToJsonObject<T>(this object entity, bool? flagIgnoreObjects = false) {

            var settings = new JsonSerializerSettings();

            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            settings.NullValueHandling = NullValueHandling.Ignore;

            if (flagIgnoreObjects == true) {
                settings.ContractResolver = new IgnoreObjectsResolver();
            }

            settings.Converters.Add(new StringDecimalConverter());

            var json = JsonConvert.SerializeObject(entity, settings);

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static IPagedList<T> ToPagedListJsonObject<T>(this IQueryable<dynamic> entity,
                                                             int                      nroPagina,
                                                             int                      nroRegistros      = 20,
                                                             bool?                    flagIgnoreObjects = false) {

            var settings = new JsonSerializerSettings();

            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            settings.NullValueHandling = NullValueHandling.Ignore;

            var PagedList = entity.ToPagedList(nroPagina, nroRegistros);

            var json = JsonConvert.SerializeObject(PagedList, settings);

            var itens = JsonConvert.DeserializeObject<IEnumerable<T>>(json);

            return new StaticPagedList<T>(itens, PagedList.PageNumber, PagedList.PageSize, PagedList.TotalItemCount);
        }
    }

}