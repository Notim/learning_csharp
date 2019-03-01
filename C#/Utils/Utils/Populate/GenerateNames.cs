using System;
using System.Extensions;
using System.IO;
using System.Linq;
using System.Text;

namespace UTIL.methods {

    public static partial class Populate {
        private static Names         Names         => new Names();
        private static SurNames      SurNames      => new SurNames();
        private static MailProviders MailProviders => new MailProviders();

        public static string GenerateNames(int surnamesCount = 0) {
            
            var random = new Random();

            var completeName = Names.listNames.ToArray()[random.Next(0, Names.listNames.ToArray().Length)];

            if (surnamesCount > 0) {
                for (int i = 0; i < surnamesCount; i++) {
                    completeName += " " + SurNames.listSurNames.ToArray()[random.Next(0, SurNames.listSurNames.ToArray().Length)];
                }
            }

            return completeName;
        }

        public static string GenerateMail(string nameAfter = "") {
            nameAfter = nameAfter.Replace(" ", ".");
            
            var random = new Random();

            var provider = MailProviders.listMails.ToArray()[random.Next(0, MailProviders.listMails.Count)];

            string completeMail;

            if (!nameAfter.IsEmpty()) {
                completeMail = nameAfter + "@" + provider;

            } else {
                completeMail =  Names.listNames.ToArray()[random.Next(0, Names.listNames.Count)];
                completeMail += "@" + provider;
            }

            return completeMail.ToLower();
        }

    }

}