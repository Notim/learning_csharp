using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using UTIL.Utils;

using WEB.AppInfrastructure.Api;
using WEB.AppInfraStructure.Core;
using WEB.AppInfrastructure.Core.Config;
using WEB.AppInfraStructure.Extensions;
using WEB.AppInfrastructure.Security;
using WEB.AppInfraStructure.Utils.FileSystem.Wrappers;

namespace WEB.AppInfrastructure.Services {

    public class ApiConex : IApiConex {
        
        private static string urlBaseApi => UtilConfig.emProducao() ? ApiServices.apiBaseLinkProd : ApiServices.apiBaseLinkDev;

        private readonly string     token;
        private readonly IPrincipal User;
        
        public ApiConex(string tokenParam = null) {
            
            ServicePointManager.ServerCertificateValidationCallback += validarCertificado;
            
            User  = HttpContextFactory.Current.User;
            token = tokenParam ?? User.tokenOrganizacao() ?? UtilConfig.tokenOrganizacao;
        }
        
        // implementar lógica aqui
        private static bool validarCertificado(object          sender,
                                               X509Certificate cert,
                                               X509Chain       chain,
                                               SslPolicyErrors error) {
            return true;
        }
        
        public string Post(string urlPost, string data, string mimeType = "application/x-www-form-urlencoded") {

            for (var tentativas = 0; tentativas <= 2; tentativas++) {
                var RetornoRequest = this.Request(urlPost, data, RequestMethods.Http.Post);

                if (!RetornoRequest.flagErro) {
                    return RetornoRequest.listaResultados.ToString();
                }
            }
            return "";
        }

        public string Get(string urlGet, params string[] queryArgsParam) {
            var data = queryArgsParam.Aggregate("", (agregated, current) => agregated + ($"{current}&"));

            for (var tentativas = 0; tentativas <= 2; tentativas++) {
                var RetornoRequest = this.Request(urlGet, data, WebRequestMethods.Http.Get);

                if (!RetornoRequest.flagErro) {
                    return RetornoRequest.listaResultados.ToString();
                }
            }
            return "";
        }
        
        public string Get(string urlGet, IEnumerable<string> queryArgsParam) {
            
            var data = queryArgsParam.Aggregate("", (agregated, current) => agregated + ($"{current}&"));

            for (var tentativas = 0; tentativas <= 2; tentativas++) {
                var RetornoRequest = this.Request(urlGet, data, WebRequestMethods.Http.Get);

                if (!RetornoRequest.flagErro) {
                    return RetornoRequest.listaResultados.ToString();
                }
            }
            return "";
        }
        
        public string Get(string urlGet, IDictionary<string, string> queryArgsParam) {
            var queryArgs = queryArgsParam.Select((key, value) => key + "=" + value).ToList();
            
            return Get(urlGet, queryArgs.ToArray());
        }
        
        public string Put(string urlPut, string identify, string data, string mimeType = "application/x-www-form-urlencoded") {
            
            urlPut = string.Concat(urlPut, "?", identify);

            for (var tentativas = 0; tentativas <= 2; tentativas++) {
                var RetornoRequest = this.Request(urlPut, data, RequestMethods.Http.Put);

                if (!RetornoRequest.flagErro) {
                    return RetornoRequest.listaResultados.ToString();
                }
            }
            
            return "";
        }
        
        public string Del(string urlPut, string identify) {
            
            urlPut = string.Concat(urlPut, "?", identify);

            for (var tentativas = 0; tentativas <= 2; tentativas++) {
                var RetornoRequest = this.Request(urlPut, "", RequestMethods.Http.Delete);

                if (!RetornoRequest.flagErro) {
                    return RetornoRequest.listaResultados.ToString();
                }
            }
            
            return "";
        }
        
        public async Task<string> PostAsync(string urlPost, string dados, string mimeType = "application/x-www-form-urlencoded") {
            return await Task.Run(() => Post(urlPost, dados, mimeType));
        }
        
        public async Task<string> GetAsync(string urlGet, params string[] queryArgsParam) {
            return await Task.Run(() => Get(urlGet, queryArgsParam));
        }

        private DefaultDTO Request(string urlService, string content, string method, string mimeType = "application/x-www-form-urlencoded") {
            var RetornoRequest = new DefaultDTO();
            
            var fullUrl = string.Concat(urlBaseApi, urlService);
            switch (method) {
                case RequestMethods.Http.Get: {
                    fullUrl = fullUrl + "?" + content;
                    content = "";
                } break;
                case WebRequestMethods.Http.Put: {
                    fullUrl = fullUrl + "?" + content;
                } break;
                case RequestMethods.Http.Delete : {
                    fullUrl = fullUrl + "?" + content;
                    content  = "";
                } break;
                case RequestMethods.Http.Patch : {
                    fullUrl = fullUrl + "?" + content;
                } break;
            }

            try {
                var Request           = (HttpWebRequest) WebRequest.Create(fullUrl);
                Request.Method        = method;
                Request.Timeout       = 300000;
                Request.ContentType   = content.isEmpty() ? "text/plain" : mimeType;
                Request.ContentLength = content.Length;
                
                Request.Headers.Add("Authorization", string.Concat("Basic ", token));

                if (!content.isEmpty()) {
                    var sentData = Encoding.UTF8.GetBytes(content);
                
                    using (var sendStream = Request.GetRequestStream()) {
                        sendStream.Write(sentData, 0, sentData.Length);
                    }
                }
                
                var WebResponse = (HttpWebResponse) Request.GetResponse();
                if (WebResponse.StatusCode != HttpStatusCode.OK) {
                    RetornoRequest.flagErro = true;
                    RetornoRequest.listaMensagens.Add($"Erro no consumo da api {method}: {fullUrl}\r\n o código de retorno é diferente do esperado: {WebResponse.StatusCode}");
                    RetornoRequest.listaResultados = null;
                }
                
                var Out = "";
                var ReceiveStream = WebResponse.GetResponseStream();
                if (ReceiveStream != null) {
                    using (var reader = new StreamReader(ReceiveStream, Encoding.UTF8)) {
                        Out = reader.ReadToEnd();
                    }
                }
                
                RetornoRequest.flagErro = false;
                RetornoRequest.listaMensagens.Add($"Consumo da api efetuada com sucesso {method}: {fullUrl}");
                RetornoRequest.listaResultados = Out;
            }
            catch (ArgumentException ex) {
                RetornoRequest.flagErro = true;
                RetornoRequest.listaMensagens.Add(ex.getLogError($"Erro no consumo da api {method}: {fullUrl}"));
                RetornoRequest.listaResultados = null;
            }
            catch (WebException ex) {
                RetornoRequest.flagErro = true;
                RetornoRequest.listaMensagens.Add(ex.getLogError($"Erro no consumo da api {method}: {fullUrl}"));
                RetornoRequest.listaResultados = null;
            }
            catch (Exception ex) {
                RetornoRequest.flagErro = true;
                RetornoRequest.listaMensagens.Add(ex.getLogError($"Erro no consumo da api {method}: {fullUrl}"));
                RetornoRequest.listaResultados = null;
            }
            
            return RetornoRequest;
        }
        
        public async Task<string> postWithFiles(string urlPost, string dados, IEnumerable<FileInfo> listFiles) {

            using (var client = new HttpClient()) {
                
                client.BaseAddress = new Uri(urlPost);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new MultipartFormDataContent();

                foreach (var OFile in listFiles) {
                    var filepath = OFile.FullName;
                    var filename = OFile.Name;
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(filepath));

                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = filename };

                    content.Add(fileContent);
                }

                var response = await client.PostAsync(dados, content);
                var returnString = await response.Content.ReadAsStringAsync();

                return returnString;
            }
        }
    }
}