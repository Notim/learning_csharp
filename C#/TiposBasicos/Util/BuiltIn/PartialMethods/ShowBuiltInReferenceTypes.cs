using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltIn {

    public static partial class BuiltInTypes {
        public static void ShowBuiltInValuesTypes() {
            // explicit built-in values data types
            short int16Example = -1;
            int   int32Example = -2;
            long  int64Example = -3;

            ushort unsignednt16Example = 1;
            uint   unsignednt32Example = 2;
            ulong  unsignednt64Example = 3;

            sbyte sbyteExample = 127;
            byte  byteExample  = 255;
            bool  boolExample  = true;

            char charExample      = 'a';
            char charExampleCoded = '\u1696'; //char with unicode

            char[] notim = {
                '\u16A4'
                , '\u16F0'
                , '\u16C5'
                , '\u16D9'
                , '\u16D7'
            }; // string ksjkkjsksk

            float   floatExample   = 5.1f;
            double  doubleExample  = 5.1;
            decimal decimalExample = 5.1m;

            var listNotim = new List<char>(notim); //ignore this

            var TerminalOut = new StringBuilder();

            TerminalOut.Append("Built-in values types"                           + "\n");
            TerminalOut.Append("----integer types-----"                          + "\n");
            TerminalOut.Append("short or System.Int16: " + int16Example          + "\n");
            TerminalOut.Append("int or System.Int32: " + int32Example            + "\n");
            TerminalOut.Append("long or System.Int64: " + int64Example           + "\n");
            TerminalOut.Append("ushort or System.UInt16: " + unsignednt16Example + "\n");
            TerminalOut.Append("uint or System.UInt32: " + unsignednt32Example   + "\n");
            TerminalOut.Append("ulong or System.UInt64: " + unsignednt64Example  + "\n");
            TerminalOut.Append("\n");
            TerminalOut.Append("----Real types-----"                        + "\n");
            TerminalOut.Append("float or System.Single: " + floatExample    + "\n");
            TerminalOut.Append("Double or System.Double: " + doubleExample  + "\n");
            TerminalOut.Append("Double or System.Double: " + decimalExample + "\n");
            TerminalOut.Append("\n");
            TerminalOut.Append("----Computional types-----"                    + "\n");
            TerminalOut.Append("byte or System.Byte: " + byteExample           + "\n");
            TerminalOut.Append("sbyte or System.SByte: " + sbyteExample        + "\n");
            TerminalOut.Append("bool or System.Bollean: " + boolExample        + "\n");
            TerminalOut.Append("char or System.Char: " + charExample           + "\n");
            TerminalOut.Append("char With direct unicode: " + charExampleCoded + "\n");
            TerminalOut.Append("char[] the first way to make strings: ");
            listNotim.ForEach(x => TerminalOut.Append(x));
            TerminalOut.Append("\n");

            Console.WriteLine(TerminalOut.ToString());
        }
    }

}