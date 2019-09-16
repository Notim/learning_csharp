using System.Collections.Generic;
using System.Web;

using Newtonsoft.Json;

using UTIL.Utils;

namespace WEB.AppInfraStructure.Session {

    public static class SessionSistema {

        private const string prefixSession = "sistema_";

        private static void setSession(string key, object value) {
            var definitiveKey = string.Concat(prefixSession, key);
            HttpContext.Current.Session[definitiveKey] = value;
        }

        private static object getSession(string key) {
            var definitiveKey = string.Concat(prefixSession, key);
            return (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session[definitiveKey] == null) ? null : HttpContext.Current.Session[definitiveKey];
        }

        public static void setUser(object User) {
            setSession("user_logged", User);
        }

        public static object getUser() {
            return getSession("user_logged");
        }

        public static void setIdUser(int idUsuario) {
            setSession("idUser_logged", idUsuario);
        }

        public static int getIdUser() {
            return getSession("idUser_logged").ToString().toInt();
        }

        public static void setIdPerfilUsuario(int idPerfil) {
            setSession("idPerfilUser_logged", idPerfil);
        }

        public static int getIdPerfilUsuario() {
            return getSession("idPerfilUser_logged").ToString().toInt();
        }

        public static void setListaGrupos<T>(List<T> listaGrupos) {
            var json = JsonConvert.SerializeObject(listaGrupos);
            
            setSession("listaGrupos", json);
        }

        public static List<T> getListaGrupos<T>() {
            var json = (string) SessionSistema.getSession("listaGrupos");

            return string.IsNullOrEmpty(json) ? new List<T>() : JsonConvert.DeserializeObject<List<T>>(json);
        }

        public static void setListaRecursos<T>(List<T> listaRecursos) {
            var json = JsonConvert.SerializeObject(listaRecursos);
            setSession("listaRecursos", json);
        }

        public static List<T> getListaRecursos<T>() {
            var json = (string) SessionSistema.getSession("listaRecursos");

            return string.IsNullOrEmpty(json) ? new List<T>() : JsonConvert.DeserializeObject<List<T>>(json);
        }

        public static void setListaPermissoes<T>(List<T> listaRecursos) {
            var json = JsonConvert.SerializeObject(listaRecursos);
            
            setSession("listaPermissoes", json);
        }

        public static List<T> getListaPermissoes<T>() {
            var json = (string) SessionSistema.getSession("listaPermissoes");

            return string.IsNullOrEmpty(json) ? new List<T>() : JsonConvert.DeserializeObject<List<T>>(json);
        }

        public static void setListClientes(List<int> listClientes) {
            setSession("listClientes", listClientes);
        }

        public static List<int> getListClientes() {
            var listClientes = new List<int>();

            if (getSession("listClientes") != null) {
                listClientes = getSession("listClientes") as List<int>;
            }

            return listClientes;
        }

        public static void setPermissoes(object permissoes) {
            setSession("listPermissions", permissoes);
        }

        public static object getPermissoes() {
            return SessionSistema.getSession("listPermissions");
        }
    }
}