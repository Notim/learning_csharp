using System;

namespace BuiltIn {

    public static partial class BuiltInTypes {
        public static void ShowMoreInfoTypes(string type) {
            switch (type) {
                case "int":
                    Console.WriteLine("int Type:");

                    Console.WriteLine("Equivalent for System.Int32, have 32 bit of size , this can ba valued with negative numbers.");

                    break;
            }
        }
    }

}