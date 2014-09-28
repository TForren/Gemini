/**
 * Teague Forren
 * Johanna Jan
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiCore
{
    public class CPU
    {
        public Int16 ACC { get; private set; }
        public int A; // test number
        public int B;
        public int Zero;
        public int One;
        public int PC;
        public int MAR;
        public int MDR;
        public int TEMP;
        public int IR;
        public int CC;
        public string nextInst;
        public string dispImm;
        public string dispLab;

        public Int32 dispMem;

        private List<string> binary;
        private List<Int16> binary16;
        private Dictionary<string, int> LabelLocationMap;
        private int index = 0;
        private string instr = "";
        private char imm = ' ';
        private char sign = ' ';
        private string mem = "";
        Boolean finished = false;

        public CPU()
        {
            ACC = 0;
        }

        Dictionary<string, Int16> memoryBook = new Dictionary<string, Int16>();

        public void setLabelLocationMap(Dictionary<string, int> list)
        {
            LabelLocationMap = list;
        }

        public void setBinary(List<string> list)
        {
            binary = list;
        }

        public void setBinary16(List<Int16> list)
        {
            binary16 = list;
        }

        public void runAll() {
            for (int i = 0; i < binary.Count; i++)
            {
                nextInstruction();
            }
        }
        public void nextInstruction()
        {
            Console.WriteLine("index: " + index);
            string temp = binary[index];
            for (int i = 0; i < temp.Length; i++)
            {
                if (i >= 0 && i <= 5)
                {
                    instr = instr + temp[i];
                }
                else if (i == 6)
                {
                    imm = temp[i];
                }
                else if (i == 7)
                {
                    sign = temp[i];
                }
                else
                {
                    mem = mem + temp[i];
                }

            }

            Boolean immediate = false;
            Boolean signed = false;
            if (imm == '1') {
                immediate = true;
                dispImm = "#$";
            }
            else{
                dispImm = "$";
            }
            if (sign == '1')
            {
                signed = true;
            }
            string binInstr = instr;
            #region switch for each instruction
            switch (binInstr)
            {
                case "100000":
                    var labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(mem)));
                    //Console.WriteLine(labelName);  
                    nextInst = labelName.Key + ":";
                    break;
                case "000000":
                    //LDA
                    if (immediate && signed)
                    {
                        //immediates can never be signed
                        //ACC = 0 - convertToBase10(mem);
                    }
                    else if (immediate && !signed)
                    {
                        ACC = Convert.ToInt16(convertToBase10(mem));
                    }
                    else
                    {
                        ACC = memoryBook[mem];
                    }
                    dispMem = Convert.ToInt32(convertToBase10(mem));
                    nextInst = "lda " + dispImm + dispMem;
                    break;
                case "000001":
                    //STA
                    if (immediate)
                    {
                        //throw exception, index is line number
                    }
                    else
                    {
                        memoryBook[mem] = ACC;
                    }

                    dispMem = Convert.ToInt32(convertToBase10(mem));
                    nextInst = "sta " + dispImm + dispMem;
                    break;
                case "000010":
                    //ADD
                    if (immediate && signed)
                    {
                        //immediate can't be negative
                    }
                    else if (immediate && !signed)
                    {
                        ACC += Convert.ToInt16(mem);
                    }
                    else
                    {
                        ACC = (Int16)(ACC + memoryBook[mem]);
                    }
                    dispMem = Convert.ToInt32(convertToBase10(mem));
                    nextInst = "add " + dispImm + dispMem;
                    break;
                case "000011":
                    //SUB
                    if (immediate && signed)
                    {
                        //throw exception
                    }
                    else if (immediate && !signed)
                    {
                        ACC = (Int16) (ACC - Convert.ToInt16(mem));
                    }
                    else
                    {
                        ACC = (Int16) (ACC - memoryBook[mem]);
                    }
                    dispMem = Convert.ToInt32(convertToBase10(mem));
                    nextInst = "sub " + dispImm + dispMem;
                    break;
                case "000100":
                    //mul
                    if (immediate && signed)
                    {
                        //throw exception
                    }
                    else if (immediate && !signed)
                    {
                        ACC = (Int16) (ACC * Convert.ToInt16(mem));
                    }
                    else
                    {
                        ACC = (Int16)(ACC * memoryBook[mem]);
                    }
                    dispMem = Convert.ToInt32(convertToBase10(mem));
                    nextInst = "mul " + dispImm + dispMem;
                    break;
                case "000101":
                    //div
                    if (immediate && convertToBase10(mem) == 0)
                    {
                        //throw exception
                    }
                    else if (immediate && signed)
                    {
                        //throw exception
                    }
                    else if (immediate && !signed)
                    {
                        ACC = (Int16)(ACC / Convert.ToInt16(mem));
                    }
                    else
                    {
                        ACC = (Int16)(ACC / memoryBook[mem]);
                    }
                    dispMem = Convert.ToInt32(convertToBase10(mem));
                    nextInst = "div " + dispImm + dispMem;
                    break;
                case "000110":
                    //and
                    if (immediate && signed)
                    {
                        //exception
                    }
                    else if (immediate && !signed)
                    {
                        ACC = (Int16) (ACC & Convert.ToInt16(mem));
                    }
                    else
                    {
                        ACC = (Int16)(ACC & memoryBook[mem]);
                    }
                    dispMem = Convert.ToInt32(convertToBase10(mem));
                    nextInst = "and " + dispImm + dispMem;
                    break;
                case "000111":
                    //or
                    if (immediate && signed)
                    {
                        //exception
                    }
                    else if (immediate && !signed)
                    {
                        ACC = (Int16)(ACC | Convert.ToInt16(mem));
                    }
                    else
                    {
                        ACC = (Int16)(ACC | memoryBook[mem]);
                    }
                    dispMem = Convert.ToInt32(convertToBase10(mem));
                    nextInst = "or " + dispImm + dispMem;
                    break;
                case "001000":
                    //SHL, if value is negative, should we shift right?
                    if (immediate)
                    {
                        string binAcc = Convert.ToString(ACC);
                        Int16 value = Convert.ToInt16(mem);
                        string afterSHL = "";
                        for (int k = (int)value; k < 8; k++)
                        {
                            afterSHL += binAcc[k];
                        }
                        for (int j = 0; j < (int)value; j++)
                        {
                            afterSHL += '0';
                        }
                        ACC = Convert.ToInt16(convertToBase10(afterSHL));
                        afterSHL = "";
                    }
                    else
                    {
                        //throw exception at "index" line
                    }
                    dispMem = Convert.ToInt32(convertToBase10(mem));
                    nextInst = "shl " + dispImm + dispMem;
                    break;
                case "001001":
                    //NOTA, if acc is negative, should it become postive?
                    string binACC = convertToBinary(ACC);
                    string resultACC = "";
                    double final = 0;
                    for (int k = 0; k < binACC.Length; k++) {
                        if (binACC[k] == '0')
                        {
                            resultACC += '1';
                        }
                        else
                        {
                            resultACC += '0';
                        }
                    }
                    final = convertToBase10(resultACC);
                    ACC = Convert.ToInt16(final);
                    resultACC = "";
                    nextInst = "nota";
                    break;
                //nothing happens to ACC for these 4 cases, it just jumps back to labelled line
                case "001010":
                    //BA
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(mem)));
                    dispLab = labelName.Key;
                    index = labelName.Value - 1;
                    Console.WriteLine("Label Name: " + labelName);
                    nextInst = "ba " + dispLab;
                    break;
                case "001011":
                    //BE, branch if ACC is zero
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(mem)));
                    dispLab = labelName.Key;
                    if (ACC == 0)
                    {
                        index = labelName.Value - 1;
                    }
                    nextInst = "be " + dispLab;
                    break;
                case "001100":
                    //BL
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(mem)));
                    dispLab = labelName.Key;
                    if (ACC < 0)
                    {
                        index = labelName.Value - 1;
                    }
                    nextInst = "bl " + dispLab;
                    break;
                case "001101":
                    //BG
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(mem)));
                    dispLab = labelName.Key;
                    Console.WriteLine("line num: " + labelName.Value);
                    if (ACC > 0)
                    {
                        index = labelName.Value - 1;
                    }
                    nextInst = "bg " + dispLab;
                    break;
                case "001110":
                    //NOP
                    ACC = (Int16) (ACC + 0);
                    nextInst = "nop";
                    break;
                case "001111":
                    //HLT
                    nextInst = "hlt";
                    finished = true;
                    index = 0;
                    break;
            }
            if (index + 1 == binary.Count)
            {
                finished = true;
                index = 0;
            }
            if (finished)
            {
                Console.WriteLine("finished");
            }
            #endregion
            index++;
            instr = "";
            mem = "";
        }

        #region convertToBase10
        public double convertToBase10(string x)
        {
            double num = 0;
            int exp = x.Length - 1;
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == '1')
                {
                    num = num + Math.Pow(2, exp);
                }
                exp--;
            }
                return num;
        }
        #endregion

        //if this function doesn't work, it's because changed parameter from int to double
        #region convertToBinary
        public string convertToBinary(double x)
        {
            ArrayList bin = new ArrayList();
            string result = "";
            double temp = x;
            double remainder = 0;
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
        #endregion

        #region negateBinary
        public string negateBinary(string x)
        {
            string result = "";
            string temp = "";
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == '0')
                {
                    temp += '1';
                }
                else
                {
                    temp += '0';
                }
            }
            double curr = convertToBase10(temp);
            curr++;
            result = convertToBinary(curr);
            return result;
        }
        #endregion


    }
}
