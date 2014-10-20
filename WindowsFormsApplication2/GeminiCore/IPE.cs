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
        List<string> binary = new List<string>();

        Dictionary<string, int> labelLocationMap = new Dictionary<string, int>();
        //LabelLocationMap.Add("LabelName", "LineNumber");
        public Boolean invalidComment;
        public Boolean broken = false;

        public string FileToParse { get; set; }

        public IPE(string filename)
        {
            this.FileToParse = filename;
        }

        public Dictionary<string, int> getLabelLocationMap()
        {
            return labelLocationMap;
        }

        public void ParseForLabels()
        {
            int lineCounter = 1;
            var lineExceptionCounter = 1;
            var lines = File.ReadAllLines(this.FileToParse).ToList<string>();
            foreach (var line in lines)
            {
                Regex labelStmtFormat = new Regex(@"^\s*(?<label>.*)\s*:\s*(?<comments>\S*)");
               // Regex stuffStmtFormat = new Regex(@"^\s*(?<stuff>[^!]\S*)");
                //var stuffStmtMatch = stuffStmtFormat.Match(line);
                var labelStmtMatch = labelStmtFormat.Match(line);
                Regex opcodeStmtFormat = new Regex(@"^\s*(?<opcode>[a-z]{2,3})\s(?<operand>\S*)\s*(?<comments>\S*)");
                Regex onlyOpStmtFormat = new Regex(@"^\s*(?<opcode>\b(nota|nop|hlt))\s*(?<comments>\S*)");
                var opcodeStmtMatch = opcodeStmtFormat.Match(line);
                var onlyOpStmtMatch = onlyOpStmtFormat.Match(line);

                if (labelStmtMatch.Success)
                {
                    if (labelStmtMatch.Groups["comments"].Value != "")
                    {
                        if (labelStmtMatch.Groups["comments"].Value[0] != '!')
                        {
                            throw new Exception("InvalidCommentException at line " + lineExceptionCounter);
                            Console.WriteLine("InvalidCommentException at line " + lineExceptionCounter);
                            broken = true;
                        }
                    }

                    var label = labelStmtMatch.Groups["label"].Value;
                    //Console.WriteLine("Label: " + label);

                    string instruction = "100000";
                    string immediate = "0";
                    string sign = "0";
                    string lineNum = convertToBinary(lineCounter);
                    labelLocationMap.Add(label, lineCounter);
                    string opCode = instruction + immediate + sign + lineNum;
                    //Console.WriteLine(opCode);
                    //binary.Add(opCode);
                    lineCounter++;
                    
                }
                else if (opcodeStmtMatch.Success)
                {
                    lineCounter++;
                }
                else if (onlyOpStmtMatch.Success)
                {
                    lineCounter++;
                }
                lineExceptionCounter++;
                //lineCounter++;
            }

        }

        public List<string> ParseFile()
        {
            ParseForLabels();
            //foreach (KeyValuePair<string, int> kvp in labelLocationMap)
           // {
               // Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
          //  }
            int lineCounter = 1;
            int lineExceptionCounter = 1;
            var lines = File.ReadAllLines(this.FileToParse).ToList<string>();

            foreach (var line in lines)
            {
                #region Regex stuff. Puts capture groups in variables
                Regex labelStmtFormat = new Regex(@"^\s*(?<label>.*)\s*:\s*(?<comments>\S*)");
                Regex opcodeStmtFormat = new Regex(@"^\s*(?<opcode>[a-z]{2,3})\s(?<operand>\S*)\s*(?<comments>\S*)");
                Regex onlyOpStmtFormat = new Regex(@"^\s*(?<opcode>\b(nota|nop|hlt))\s*(?<comments>\S*)");
                var labelStmtMatch = labelStmtFormat.Match(line);
                var opcodeStmtMatch = opcodeStmtFormat.Match(line);
                var onlyOpStmtMatch = onlyOpStmtFormat.Match(line);
                #endregion

                if (labelStmtMatch.Success)
                {
                    if (labelStmtMatch.Groups["comments"].Value != "")
                    {
                        if (labelStmtMatch.Groups["comments"].Value[0] != '!')
                        {
                            throw new Exception("InvalidCommentException at line " + lineExceptionCounter);
                            Console.WriteLine("InvalidCommentException at line " + lineExceptionCounter) ;
                            broken = true;
                        }
                    }

                   var label = labelStmtMatch.Groups["label"].Value;
                   //Console.WriteLine("Label: " + label);
                    
                   string instruction = "100000";
                   string immediate = "0";
                   string sign = "0";
                   string lineNum = convertToBinary(lineCounter);
                   labelLocationMap[label] = lineCounter;
                  // Console.WriteLine("label: " + label);
                   //Console.WriteLine("key: " + labelLocationMap[label]);
                   string opCode = instruction + immediate + sign + lineNum;
                   //Console.WriteLine(opCode);
                   binary.Add(opCode);
                   lineCounter++;
                }
            

                if (opcodeStmtMatch.Success)
                {
                    if (opcodeStmtMatch.Groups["comments"].Value != "")
                    {
                        if (opcodeStmtMatch.Groups["comments"].Value[0] != '!')
                        {
                            throw new Exception("InvalidCommentException at line " + lineExceptionCounter);
                            Console.WriteLine("InvalidCommentException at line " + lineExceptionCounter);
                            broken = true;
                        }
                    }
                    var opcode = opcodeStmtMatch.Groups["opcode"].Value;
                    var operand = opcodeStmtMatch.Groups["operand"].Value;
                    //Console.WriteLine("Opcode: " + opcode + " Operand: " + operand);

                    string binInstr = "";
                    string immediate = "";
                    string currInst = opcode;

                    //Instruction
                    binInstr = identifyInstr(currInst); //Takes in the opcode found by regex and returns custom binary designation

                    //immediate value
                    if (operand != null) // Is there something after the opcode?
                    {
                        if (operand[0] == '#') // Is it immediate?
                        {
                            immediate = "1";
                        }
                        else
                        {
                            immediate = "0";
                        }
                    }
                    else // There is nothing after the opcode
                    {
                        immediate = "0";
                    }

                    //memory address and sign bit
                    if (operand != null)
                    {
                        string sign = "";
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
                          
                            Int16 intVal = Convert.ToInt16(value);
                            if (intVal > 255)
                            {
                                throw new Exception("Segmentation Fault at line " + lineExceptionCounter);
                                Console.WriteLine("Segmentation Fault at line " + lineExceptionCounter);
                                broken = true;
                            }
                            else if (intVal < 0)
                            {
                                Console.WriteLine("Negative immediate Value at line " + lineExceptionCounter);
                                broken = true;
                                sign = "1";
                                //intVal = Math.Abs(intVal);
                            }
                            else
                            {
                                sign = "0";
                            }
                            mem = convertToBinary(intVal);
                            string opCode = binInstr + immediate + sign + mem;
                            //Console.WriteLine(opCode);
                            binary.Add(opCode);
                        }
                        //immediate == 0
                        else
                        {
                            sign = "0";
                            string address = "";
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
                            if (op[0] == '$')
                            {
                                if (Convert.ToInt16(address) > 255)
                                {
                                    throw new Exception("Segmentation Fault at line " + lineExceptionCounter);
                                    Console.WriteLine("Segmentation Fault at line " + lineExceptionCounter);
                                    broken = true;
                                }
                                else
                                {
                                    mem = convertToBinary(Convert.ToInt16(address));
                                }
                            }
                            //branches, jumps -- need to implement
                            else
                            {
                                
                                if (labelLocationMap.ContainsKey(address))
                                {
                                    int memVal = labelLocationMap[address];
                                    //Console.WriteLine("memVal: " + memVal);
                                    mem = convertToBinary(memVal);
                                }
                                //op is the label name (string), need to keep track of each label name when first find it
                                else
                                {
                                    //label doesn't exist
                                    mem = "00000000";
                                }
                            }
                            string opCode = binInstr + immediate + sign + mem;
                            //Console.WriteLine(opCode);
                            binary.Add(opCode);
                        }

                    }
                    else 
                    {
                        //if no operand exists
                        string sign = "0";
                        string mem = "00000000";
                        string opCode = binInstr + immediate + sign + mem;
                       // Console.WriteLine(opCode);
                        binary.Add(opCode);
                    }
                    lineCounter++;
                    
                }
                else if (onlyOpStmtMatch.Success)
                {
                    if (onlyOpStmtMatch.Groups["comments"].Value != "")
                    {
                        if (onlyOpStmtMatch.Groups["comments"].Value[0] != '!')
                        {
                            throw new Exception("InvalidCommentException at line " + lineExceptionCounter);
                            Console.WriteLine("InvalidCommentException at line " + lineExceptionCounter);
                            broken = true;
                        }
                    }
                    var opcode = onlyOpStmtMatch.Groups["opcode"].Value;
                    //Console.WriteLine("onlyOpcode: " + opcode);

                    string currIn = opcode;
                    string binInstr = "";
                    switch (currIn)
                    {
                        case "nota":
                            binInstr = "001001";
                            break;
                        case "nop":
                            binInstr = "001110";
                            break;
                        case "hlt":
                            binInstr = "001111";
                            break;
                    }
                    string opCode = binInstr + "00" + "00000000";
                   // Console.WriteLine(opCode);
                    binary.Add(opCode);
                    lineCounter++;
                }

                lineExceptionCounter++;
                //lineCounter++;
            }
            return binary;
        }

        public void createBinaryTextFile(List<string> list) {
            using (TextWriter writer = File.CreateText(@"E:\Important Stuff\udel\CISC360\stuuuff\Binary.txt"))
            {
                foreach (string actor in list)
                {
                    writer.WriteLine(actor);
                }
            }
        }

        #region convertToBase10
        public int convertToBase10(string x)
        {
            int num = 0;
            int exp = x.Length - 1;
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == '1')
                {
                    num = (int)(num + Math.Pow(2, exp));
                }
                exp--;
            }
            return num;
        }
        #endregion

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

        #region identifyInstr (Takes in opcode from regex and returns binary desigation)
        public string identifyInstr(string inst)
        {
            string foundInst = "";
            switch (inst)
            {
                case "lda":
                    foundInst = "000000";
                    break;
                case "sta":
                    foundInst = "000001";
                    break;
                case "add":
                    foundInst = "000010";
                    break;
                case "sub":
                    foundInst = "000011";
                    break;
                case "mul":
                    foundInst = "000100";
                    break;
                case "div":
                    foundInst = "000101";
                    break;
                case "and":
                    foundInst = "000110";
                    break;
                case "or":
                    foundInst = "000111";
                    break;
                case "shl":
                    foundInst = "001000";
                    break;
                case "nota":
                    foundInst = "001001";
                    break;
                case "ba":
                    foundInst = "001010";
                    break;
                case "be":
                    foundInst = "001011";
                    break;
                case "bl":
                    foundInst = "001100";
                    break;
                case "bg":
                    foundInst = "001101";
                    break;
                /*
            case "nop":
                binInstr = "001110";
                break;
            case "hlt":
                binInstr = "001111";
                break;
                 */
            }

            return foundInst;
        }
        #endregion
    }
}
