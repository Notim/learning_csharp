using System.Collections.Specialized;
using System.IO;
using System.Net;

using WEB.AppInfraStructure.Services.Request.Interface;

namespace WEB.AppInfraStructure.Services.Request.Services {

    public class RequestPost : IRequestPost {

        public string postRequest(string url, byte[] data, NameValueCollection extraHeaders = null) {

            var ORequest = WebRequest.Create(url);

            ORequest.Method        = "POST";
            ORequest.ContentType   = "application/x-www-form-urlencoded";
            ORequest.ContentLength = data.Length;

            if (extraHeaders != null) {
                ORequest.Headers.Add(extraHeaders);
            }

            using (Stream stream = ORequest.GetRequestStream()) {
                stream.Write(data, 0, data.Length);
            }

            var httpResponse = (HttpWebResponse) ORequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                return streamReader.ReadToEnd();
            }
        }
    }

}