using System.Collections.Specialized;

namespace WEB.AppInfraStructure.Services.Request.Interface {

    public interface IRequestPost {

        string postRequest(string url, byte[] data, NameValueCollection extraHeaders = null);
    }

}