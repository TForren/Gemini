/**
 * Teague Forren
 * Johanna Jan
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeminiCore
{
    public class IPE
    {
        public string FileToParse { get; set; }

        public IPE(string filename)
        {
            this.FileToParse = filename;
        }

        public void ParseFile()
        {
            var lines = File.ReadAllLines(this.FileToParse).ToList<string>();

            foreach (var line in lines)
            {
                Regex labelStmtFormat = new Regex(@"^(?<label>.*)\s*:$");
                Regex opcodeStmtFormat = new Regex(@"^\s*(?<opcode>[a-z]{2,3})\s(?<operand>\S*)");
              //  Regex operandStmtFormat = new Regex(@"\s")
                var labelStmtMatch = labelStmtFormat.Match(line);
                var opcodeStmtMatch = opcodeStmtFormat.Match(line);

                if (labelStmtMatch.Success)
                {
                   var label = labelStmtMatch.Groups["label"].Value;
                   Console.WriteLine("Label: " + label);
                }
                else if (opcodeStmtMatch.Success)
                {
                    var opcode = opcodeStmtMatch.Groups["opcode"].Value;
                    var operand = opcodeStmtMatch.Groups["operand"].Value;
                      Console.WriteLine("Opcode: " + opcode + " Operand: " + operand);
                }



            } 

        }
/*
        public string testing(string x)
        {
            string currInstruction = " ";
            switch (currInstruction)
            {
                case "LDA":
                    Console.WriteLine("LDA");
                    break;
                case "STA":
                    Console.WriteLine("STA");
                    break;
                case "ADD":
                    Console.WriteLine("ADD");
                    break;
                default:
                    Console.WriteLine("You done goofed");
                    break;
            }
            return currInstruction;
        }
 */ 
    }
}
