using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WEB.AppInfraStructure.Services.Request.Interface {
	
	public interface IRequestAsync {

		Task<TextReader> doRequestAsync(WebRequest OWebRequest);

        Task<TextReader> doRequestAsync(string url);

        Task<TextReader> postRequestAsync(string url, byte[] data);
	}
}
