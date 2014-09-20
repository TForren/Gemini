/**
 * Teague Forren
 * Johanna Jan
 */
using System;
using System.Collections;
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
        ArrayList binary = new ArrayList();
  
        public string FileToParse { get; set; }

        public IPE(string filename)
        {
            this.FileToParse = filename;
        }

        public void ParseFile()
        {
            int lineCounter = 1;
            var lines = File.ReadAllLines(this.FileToParse).ToList<string>();

            foreach (var line in lines)
            {
                Regex labelStmtFormat = new Regex(@"^(?<label>.*)\s*:$");
                Regex opcodeStmtFormat = new Regex(@"^\s*(?<opcode>[a-z]{2,3})\s(?<operand>\S*)");
                Regex onlyOpStmtFormat = new Regex(@"^\s*(?<opcode>[a-z]{2,4})");
                var labelStmtMatch = labelStmtFormat.Match(line);
                var opcodeStmtMatch = opcodeStmtFormat.Match(line);
                var onlyOpStmtMatch = onlyOpStmtFormat.Match(line);

                if (labelStmtMatch.Success)
                {
                   var label = labelStmtMatch.Groups["label"].Value;
                   Console.WriteLine("Label: " + label);
                    
                   string instruction = "1000000";
                   string immediate = "0";
                   string value = convertToBinary(lineCounter);
                   string opCode = instruction + immediate + value;
                   Console.WriteLine(opCode);
                   binary.Add(opCode);
                             
                }
                else if (opcodeStmtMatch.Success)
                {
                    var opcode = opcodeStmtMatch.Groups["opcode"].Value;
                    var operand = opcodeStmtMatch.Groups["operand"].Value;
                    Console.WriteLine("Opcode: " + opcode + " Operand: " + operand);

                    string binInstr = "";
                    string currInst = opcode;
                    switch (currInst)
                    {
                        case "lda":
                            binInstr = "0000000";
                            break;
                        case "sta":
                            binInstr = "0000001";
                            break;
                        case "add":
                            binInstr = "0000010";
                            break;
                        case "sub":
                            binInstr = "0000011";
                            break;
                        case "mul":
                            binInstr = "0000100";
                            break;
                        case "div":
                            binInstr = "0000101";
                            break;
                        case "and":
                            binInstr = "0000110";
                            break;
                        case "or":
                            binInstr = "0000111";
                            break;
                        case "shl":
                            binInstr = "0001000";
                            break;
                            /*
                        case "nota":
                            binInstr = "0001001";
                            break;
                        case "ba":
                            binInstr = "0001010";
                            break;
                        case "be":
                            binInstr = "0001011";
                            break;
                        case "bl":
                            binInstr = "0001100";
                            break;
                        case "bg":
                            binInstr = "0001101";
                            break;
                        case "nop":
                            binInstr = "0001110";
                            break;
                        case "hlt":
                            binInstr = "0001111";
                            break;
                             */

                    }

                    //immediate value
                    string immediate = "";
                    if (operand != null)
                    {
                        if (operand[0] == '#')
                        {
                            immediate = "1";
                        }
                        else
                        {
                            immediate = "0";
                        }
                    }
                    else
                    {
                        immediate = "0";
                    }

                    //memory address
                    if (operand != null)
                    {
                        string op = Convert.ToString(operand);
                        string mem = "";
                        if (immediate == "1")
                        {
                            string value = "";
                            for (int i = 0; i < op.Length; i++)
                            {
                                if (op[i] == '#' || op[i] == '$')
                                {
                                    continue;
                                }
                                else
                                {
                                    value = value + op[i];
                                }
                            }
                            int intVal = Convert.ToInt32(value);
                            mem = convertToBinary(intVal);
                            string opCode = binInstr + immediate + mem;
                            Console.WriteLine(opCode);
                            binary.Add(opCode);
                        }
                        //immediate == 0
                        else
                        {
                            string address = "";
                            if (op[0] == '$')
                            {
                                for (int i = 0; i < op.Length; i++)
                                {
                                    if (op[i] == '$')
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        address = address + op[i];
                                    }
                                }
                                mem = convertToBinary(Convert.ToInt32(address));
                            }
                            //branches, jumps -- need to implement
                            else
                            {
                                //op is the label name (string), need to keep track of each label name when first find it
                                mem = "00000000";
                            }
                            string opCode = binInstr + immediate + mem;
                            Console.WriteLine(opCode);
                            binary.Add(opCode);
                        }

                    }
                    else
                    {
                        //if no operand exists
                        string mem = "00000000";
                        string opCode = binInstr + immediate + mem;
                        Console.WriteLine(opCode);
                        binary.Add(opCode);
                    }
                    
                }
                //expression doesn't detect operations ba, be, bl, bg
                else if (onlyOpStmtMatch.Success)
                {
                    var opcode = onlyOpStmtMatch.Groups["opcode"].Value;
                    Console.WriteLine("onlyOpcode: " + opcode);

                    string currIn = opcode;
                    string binInstr = "";
                    switch (currIn)
                    {
                        case "nota":
                            binInstr = "0001001";
                            break;
                        case "ba":
                            binInstr = "0001010";
                            break;
                        case "be":
                            binInstr = "0001011";
                            break;
                        case "bl":
                            binInstr = "0001100";
                            break;
                        case "bg":
                            binInstr = "0001101";
                            break;
                        case "nop":
                            binInstr = "0001110";
                            break;
                        case "hlt":
                            binInstr = "0001111";
                            break;
                    }
                    string opCode = binInstr + "0" + "00000000";
                    Console.WriteLine(opCode);
                    binary.Add(opCode);
                }

                lineCounter++;
            } 

        }

        public string convertToBinary(int x)
        {
            ArrayList bin = new ArrayList();
            string result = "";
            int temp = x;
            int remainder = 0;
            int length = 0;

            while (temp > 0)
            {
                remainder = temp % 2;
                temp = temp / 2;
                bin.Add(remainder);
                length++;
            }

            bin.Reverse();

            while (length < 8)
            {
                result = result + "0";
                length++;
            }

            foreach (int i in bin)
            {
                result = result + Convert.ToString(i);
            }
            
                return result;
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
