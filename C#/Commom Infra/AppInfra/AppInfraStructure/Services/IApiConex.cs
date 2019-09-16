using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB.AppInfrastructure.Services {

    public interface IApiConex {

        string Get(string urlGet, IDictionary<string, string> queryArgs);

        string Get(string urlGet, params string[] queryArgsParam);

        string Get(string urlGet, IEnumerable<string> queryArgsParam);

        string Post(string urlPost, string dados, string mimeType = "application/x-www-form-urlencoded");

        string Put(string urlPut,
                   string identify,
                   string data,
                   string mimeType = "application/x-www-form-urlencoded");

        string Del(string urlPost, string dados);

        Task<string> GetAsync(string urlGet, params string[] queryArgsParam);

        Task<string> PostAsync(string urlPost, string dados, string mimeType = "application/x-www-form-urlencoded");
    }

}