using System.IO;
using System.Web;

namespace WEB.AppInfrastructure.Wrappers {

    public class HttpPostedFileCustomWrapper : HttpPostedFileBase {
        
        public override int ContentLength => (int) stream.Length;

        public override string ContentType => contentType;

        public override string FileName => fileName;

        public override Stream InputStream => stream;

        private readonly MemoryStream stream;
        private readonly string       contentType;
        private readonly string       fileName;

        public HttpPostedFileCustomWrapper(MemoryStream stream, 
                                           string contentType, 
                                           string fileName) {
            
            this.contentType = contentType;
            this.fileName = fileName;
            this.stream = stream;
        }
        
        public override void SaveAs(string filename) {

            using (var file = File.Open(filename, FileMode.CreateNew)) {
                stream.WriteTo(file);
            }
        }

    }

}