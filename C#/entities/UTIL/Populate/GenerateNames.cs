using System;
using System.Extensions;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace UTIL.methods {

    public static partial class Populate {
        
        private static readonly string[] names    = File.ReadAllLines("/home/joao/Github/learning_csharp/C#/entities/UTIL/Populate/Data/names");
        private static readonly string[] surNames = File.ReadAllLines("/home/joao/Github/learning_csharp/C#/entities/UTIL/Populate/Data/surnames");
        private static readonly string[] mailProviders = File.ReadAllLines("/home/joao/Github/learning_csharp/C#/entities/UTIL/Populate/Data/mailproviders");
        // TODO : fazer esse endereço ficar relativo a lib e nao ao projeto
        
        public static string GenerateNames(int surnamesCount = 0) {
            var random = new Random();
                
            var completeName = names[random.Next(0, names.Length)];

            if (surnamesCount > 0) {
                for (int i = 0; i < surnamesCount; i++) {
                    completeName += " " + surNames[random.Next(0, surNames.Length)];
                }
            }
            return completeName;
        }

        public static string GenerateMail(string nameAfter = "") {
            var random = new Random();
            
            var provider = mailProviders[random.Next(0, mailProviders.Length)];
            
            string completeMail;
            
            if (!nameAfter.IsEmpty()) {
                completeMail = nameAfter + "@" + provider;
                
            } else {
                completeMail = names[random.Next(0, names.Length)];
                completeMail += "@" + provider;
            }

            return completeMail;
        }
        
    }

}