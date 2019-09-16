using System.Collections.Specialized;

namespace WEB.AppInfraStructure.Services.Request.Interface {

    public interface IRequestGet {

        string doRequest(string url, NameValueCollection extraHeaders = null);
    }

}