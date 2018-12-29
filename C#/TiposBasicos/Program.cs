using System;
using System.Collections.Generic;
using System.Linq;

using BuiltIn;

namespace TiposBasicos {

    public static class Program {
        public static void TerminalProgram(List<string> option) {
            if (!option.Any()) {
                Console.WriteLine("please write a command!! or use --help for more informations!");

                return;
            }

            var FirstCommand = option[0];

            if (FirstCommand.Contains("-a") || FirstCommand.Contains("--all")) {
                BuiltInTypes.ShowBuiltInValuesTypes();
                BuiltInTypes.ShowBuiltInReferenceTypes();
            }

            if (FirstCommand.Contains("-r") || FirstCommand.Contains("--reference")) {
                BuiltInTypes.ShowBuiltInReferenceTypes();
            }

            if (FirstCommand.Contains("-v") || FirstCommand.Contains("--values")) {
                BuiltInTypes.ShowBuiltInValuesTypes();
            }

            if (FirstCommand.Contains("-m") || FirstCommand.Contains("--more")) {
                try {
                    var SecondCommand = option[1];

                    BuiltInTypes.ShowMoreInfoTypes(SecondCommand);
                }
                catch (Exception e) {
                    Console.Error.WriteLine(e);
                    Console.WriteLine("especify the Type to show more info");

                    return;
                }
            }

            if (FirstCommand.Contains("--version")) {
                BuiltInTypes.ShowVersion();
            }

            if (FirstCommand.Contains("--help")) {
                BuiltInTypes.ShowVersion();
            }
        }
    }

}