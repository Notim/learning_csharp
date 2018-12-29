using System.Collections.Generic;

namespace TiposBasicos
{
    static class Start
    {
        static void Main(string[] args)
        {
            var option = new List<string>(args);
            Program.TerminalProgram(option);
        }
    }
}