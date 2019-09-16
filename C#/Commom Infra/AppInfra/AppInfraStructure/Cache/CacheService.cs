using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

using UTIL.Utils;

using WEB.AppInfraStructure.Core;
using WEB.AppInfrastructure.Security;

namespace WEB.AppInfraStructure.Cache {

    public class CacheService {
        // Atributos
        private static CacheService _instance;

        // Propriedades
        public static CacheService getInstance => _instance = _instance ?? new CacheService();

        /// <summary>
        /// Definir prefixo das chaves a partir do id da organizacao
        /// </summary>
        private static string prefixoKey(int idOrganizacao) {

            var prefixoKey = $"cache_{idOrganizacao.GetHashCode()}";

            return prefixoKey;
        }

        /// <summary>
        /// Montagem para uma chave de chave
        /// </summary>
        private static string keyCache(int? idOrganizacao, string key) {

            var keyRetorno = $"{prefixoKey(idOrganizacao.toInt())}_{key}";

            return keyRetorno;
        }

        /// <summary>
        /// Carregar um item salvo no cache
        /// </summary>
        public static object carregar(string key, int? idOrganizacaoParam = null) {
            
            int? idOrganizacao = idOrganizacaoParam > 0 ? idOrganizacaoParam : HttpContext.Current.User.idOrganizacao();

            key = keyCache(idOrganizacao, key);

            var cacheData = HttpContextFactory.Current.Cache.Get(key);

            return cacheData;
        }

        /// <summary>
        /// Carregar um registro a partir do cache
        /// </summary>
        public static T carregar<T>(string key, int idOrganizacaoParam = 0) where T : class {

            var idOrganizacao = idOrganizacaoParam > 0 ? idOrganizacaoParam : HttpContext.Current.User.idOrganizacao();

            key = keyCache(idOrganizacao, key);

            var cacheData = HttpContextFactory.Current.Cache.Get(key);

            return cacheData as T;
        }
        

        /// <summary>
        /// Adicionar itens em cache
        /// </summary>
        public static object adicionar(string key, object valor, int idOrganizacaoParam = 0) {
            
            var idOrganizacao = idOrganizacaoParam > 0 ? idOrganizacaoParam : HttpContextFactory.Current.User.idOrganizacao();
            
            key = keyCache(idOrganizacao, key);

            if (HttpContextFactory.Current.Cache.Get(key) != null) {
                HttpContextFactory.Current.Cache.Remove(key);
            }

            HttpContextFactory.Current.Cache.Add(key, valor, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);

            return carregar(key);
        }
        

        /// <summary>
        /// Limpar todas as chaves
        /// </summary>
        public void limparCache() {
            foreach (DictionaryEntry entry in HttpContextFactory.Current.Cache) {
                var cacheKey = (string) entry.Key;
                
                HttpContextFactory.Current.Cache.Remove(cacheKey);
            }
        }

        /// <summary>
        /// Limpar uma chave de cache 
        /// </summary>
        public void remover(string key = "", int idOrganizacaoParam = 0) {
            if (string.IsNullOrEmpty(key)) {
                return;
            }

            var idOrganizacao = idOrganizacaoParam > 0 ? idOrganizacaoParam : HttpContextFactory.Current.User.idOrganizacao();

            key = keyCache(idOrganizacao, key);

            if (HttpContextFactory.Current.Cache.Get(key) != null) {
                HttpContextFactory.Current.Cache.Remove(key);
            }
        }
    }

}