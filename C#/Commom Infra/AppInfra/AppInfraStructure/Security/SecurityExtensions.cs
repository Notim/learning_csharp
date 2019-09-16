using System;
using System.Security.Principal;
using System.Web;

using UTIL.Utils;

namespace WEB.AppInfrastructure.Security {

    public static class SecurityExtensions {

        public static int idOrganizacao(this IPrincipal User) {
            return SecurityCookie.idOrganizacao.toInt();
        }

        public static string tokenOrganizacao(this IPrincipal User) {
            return SecurityCookie.tokenOrganizacao;
        }

        public static string rotaCustomizada(this IPrincipal User) {
            return SecurityCookie.rotaCustomizada;
        }

        public static string linkAreaAssociado(this IPrincipal User) {
            return SecurityCookie.linkAreaAssociado;
        }

        public static void setOrganizacao(this IPrincipal User, string idOrganizacao) {
            SecurityCookie.idOrganizacao = idOrganizacao;
            if (idOrganizacao != null) {
                SecurityCookie.idOrganizacaoCrypt = UtilCrypt.SHA512(idOrganizacao);
            }
        }

        public static void setTokenOrganizacao(this IPrincipal User, string tokenOrganizacao) {
            SecurityCookie.tokenOrganizacao = tokenOrganizacao;
            if (tokenOrganizacao != null) {
                SecurityCookie.tokenOrganizacaoCrypt = UtilCrypt.SHA512(tokenOrganizacao);
            }
        }

        public static void setRotaCustomizada(this IPrincipal User, string rotaCustomizada) {
            SecurityCookie.rotaCustomizada = rotaCustomizada;
            if (rotaCustomizada != null) {
                SecurityCookie.rotaCustomizadaCrypt = UtilCrypt.SHA512(rotaCustomizada);
            }
        }

        public static void setLinkAreaAssociado(this IPrincipal User, string linkAreaAssociado) {
            SecurityCookie.linkAreaAssociado = linkAreaAssociado;
            if (linkAreaAssociado != null) {
                SecurityCookie.linkAreaAssociadoCrypt = UtilCrypt.SHA512(linkAreaAssociado);
            }
        }
        
        public static bool showInstructions(this IPrincipal User) {
            return SecurityCookie.flagInstructions.toBool();
        }

        public static string pastaOrganizacao(this IPrincipal User) {
            return string.Concat("upload/organizacao_", User.idOrganizacao().ToString().PadLeft(15, '0'));
        }

        // Verificar se há login ativo na pagina
        public static bool hasLogin(this IPrincipal User) {
            var idUsuario      = SecurityCookie.userId;
            var idUsuarioCrypt = SecurityCookie.userIdCrypt;

            var idPessoa      = SecurityCookie.idPessoa;
            var idPessoaCrypt = SecurityCookie.idPessoaCrypt;

            var userName      = SecurityCookie.userName;
            var userNameCrypt = SecurityCookie.userNameCrypt;

            var idTipoCadastro      = SecurityCookie.idTipoCadastro;
            var idTipoCadastroCrypt = SecurityCookie.idTipoCadastroCrypt;

            var status      = SecurityCookie.status;
            var statusCrypt = SecurityCookie.statusCrypt;

            var situacaoFinanceira      = SecurityCookie.situacaoFinanceira;
            var situacaoFinanceiraCrypt = SecurityCookie.situacaoFinanceiraCrypt;

            if (string.IsNullOrEmpty(SecurityCookie.userId) || string.IsNullOrEmpty(SecurityCookie.idPessoa) || string.IsNullOrEmpty(SecurityCookie.userName)) {
                return false;
            }

            if (UtilCrypt.SHA512(idUsuario) != idUsuarioCrypt) {
                return false;
            }

            if (UtilCrypt.SHA512(idPessoa) != idPessoaCrypt) {
                return false;
            }

            if (UtilCrypt.SHA512(userName) != userNameCrypt) {
                return false;
            }

            if (UtilCrypt.SHA512(idTipoCadastro) != idTipoCadastroCrypt) {
                return false;
            }

            if (UtilCrypt.SHA512(status) != statusCrypt) {
                return false;
            }

            if (UtilCrypt.SHA512(situacaoFinanceira) != situacaoFinanceiraCrypt) {
                return false;
            }

            return true;
        }

        // Destruir os cookies de seguranca a partir de um associado
        public static void signOut(this IPrincipal User) {
            var allDomainCookies = HttpContext.Current.Request.Cookies.AllKeys;

            foreach (var domainCookie in allDomainCookies) {
                var expiredCookie = new HttpCookie(domainCookie) {
                    Expires = DateTime.Now.AddDays(-1), Value = null
                };
                HttpContext.Current.Response.Cookies.Add(expiredCookie);
            }

            HttpContext.Current.Request.Cookies.Clear();
        }

        public static int id(this IPrincipal User) {
            return SecurityCookie.userId.toInt();
        }

        public static int idPessoa(this IPrincipal User) {
            return SecurityCookie.idPessoa.toInt();
        }

        public static int idTipoCadastro(this IPrincipal User) {
            return SecurityCookie.idTipoCadastro.toInt();
        }

        public static string name(this IPrincipal User) {
            return SecurityCookie.userName;
        }

        public static string email(this IPrincipal User) {
            return SecurityCookie.userEmail;
        }

        public static string tipoPessoa(this IPrincipal User) {
            return SecurityCookie.tipoPessoa;
        }

        public static string status(this IPrincipal User) {
            return SecurityCookie.status;
        }

        public static string situacaoFinanceira(this IPrincipal User) {
            return SecurityCookie.situacaoFinanceira;
        }
    }
}