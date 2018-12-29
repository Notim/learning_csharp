using System;

namespace BuiltIn {

    public static partial class BuiltInTypes {
        public static void ShowBuiltInReferenceTypes() {
            string StringExample = "Author: github.com/Notim"; // that is a reference to char[] type
            object ObjectExample = new object() { };

            Console.WriteLine("Built-in References types");
            Console.WriteLine("object or System.ObjectI: " + ObjectExample + "I cant show this, sorry!");
            Console.WriteLine("string or System.String: "  + StringExample);
        }
    }

}