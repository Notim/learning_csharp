using System;

namespace BuiltIn {

    public static partial class BuiltInTypes {
        public static string Version() => "v01";
        public static string Author()  => "Notim 2018";

        public static void ShowVersion() { Console.WriteLine(Version()); }
        public static void ShowAuthor()  { Console.WriteLine(Author()); }
    }

}