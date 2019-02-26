using System;
using System.Linq;

using FluentEmail.FluentMailCore;

namespace FluentEmail {

    public class program {
        public static void Main(string[] args) {
            FluentMailServices Services = new FluentMailServices();

            var s = Services.SendRazorMail(razorTemplate: "ola @Model.name, agora voce eh @Model.value",
                                           razorParam: new {name = "joao", value = "zica"},
                                           to: "paulino.joaovitor@yahoo.com.br",
                                           from: "paulino.joaovitor@yahoo.com.br",
                                           subject: "Teste de envio");
            
            Console.WriteLine(s.Successful + s.ErrorMessages.FirstOrDefault());
        }

    }

}