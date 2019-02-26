using System.IO;

using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;

namespace FluentEmail.FluentMailCore {

    public class FluentMailServices {
        
        public SendResponse SendRazorMail(string from          = "", 
                                          string to            = "",
                                          string subject       = "",
                                          string razorTemplate = "",
                                          object razorParam    = null) {
                
            Email.DefaultRenderer = new RazorRenderer();
            
            // string template = "ola @Model.name, agora voce eh @Model.value";

            SendResponse Response = Email.From(from)
                                         .To(to)
                                         .Subject(subject)
                                         .UsingTemplate(razorTemplate, razorParam)
                                         .Send();
                        
            return Response;
        }
        
        public void teste() {
            var email = Email.From("john@email.com") 
                             .To("bob@email.com", "bob")
                             .To("paulino.joaovitor@yahoo.com.br", "bob")
                             .BCC("paulino.joaovitor@yahoo.com.br", "IT")
                             .CC("paulino.joaovitor@yahoo.com.br", "IT")
                             .HighPriority()
                             .Subject("hows it going bob")
                             .Body("yo dawg, sup?")
                             .Send();
        }
    }

}