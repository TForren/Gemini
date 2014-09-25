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
        public double ACC { get; private set; }
        public int A = 546456; // test number
        public int B;
        public int Zero;
        public int One;
        public int PC;
        public int MAR;
        public int MDR;
        public int TEMP;
        public int IR;
        public int CC;

        private List<string> binary;
        private List<Int16> binary16;
        private int index = 0;
        private string instr = "";
        private char imm = ' ';
        private char sign = ' ';
        private string mem = "";

        public CPU()
        {
            ACC = 0;
        }

        Dictionary<string, double> memoryBook = new Dictionary<string, double>();

        public void setBinary(List<string> list)
        {
            binary = list;
        }
        public void setBinary16(List<Int16> list)
        {
            binary16 = list;
        }

        public void nextInstruction()
        {
            string temp = binary[index];
            Console.WriteLine("temp: " + temp);
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
            Console.WriteLine(instr + " " + imm + " " + sign + " " + mem);
            Boolean immediate = false;
            Boolean signed = false;
            if (imm == '1') {
                immediate = true;
            }
            if (sign == '1')
            {
                signed = true;
            }
            string binInstr = instr;
            switch (binInstr)
            {
                case "000000":
                    //LDA
                    if (immediate && signed)
                    {
                        ACC = 0 - convertToBase10(mem);
                    }
                    else if (immediate && !signed)
                    {
                        ACC = convertToBase10(mem);
                    }
                    else
                    {
                        ACC = memoryBook[mem];
                    }
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
                    break;
                case "000010":
                    //ADD
                    if (immediate && signed)
                    {
                        ACC = ACC - convertToBase10(mem);
                    }
                    else if (immediate && !signed)
                    {
                        ACC += convertToBase10(mem);
                    }
                    else
                    {
                        ACC = ACC + memoryBook[mem];
                    }
                    break;
                case "000011":
                    //SUB
                    if (immediate && signed)
                    {
                        ACC = ACC + convertToBase10(mem);
                    }
                    else if (immediate && !signed)
                    {
                        ACC = ACC - convertToBase10(mem);
                    }
                    else
                    {
                        ACC = ACC - memoryBook[mem];
                    }
                    break;
                case "000100":
                    //mul
                    if (immediate && signed)
                    {
                        ACC = -ACC * convertToBase10(mem);
                    }
                    else if (immediate && !signed)
                    {
                        ACC = ACC * convertToBase10(mem);
                    }
                    else
                    {
                        ACC = ACC * memoryBook[mem];
                    }
                    break;
                case "000101":
                    //div
                    //if ACC is a decimal number?
                    if (convertToBase10(mem) == 0)
                    {
                        //throw exception
                    }
                    else if (immediate && signed)
                    {
                        ACC = -ACC / convertToBase10(mem);
                    }
                    else if (immediate && !signed)
                    {
                        ACC = ACC / convertToBase10(mem);
                    }
                    else
                    {
                        ACC = ACC / memoryBook[mem];
                    }
                    break;
                case "000110":
                    //And, if both acc and mem are negative, is output neg?
                    string binACC = convertToBinary(Math.Abs(ACC));
                    if (ACC < 0)
                    {
                        binACC = negateBinary(binACC);
                    }
                    int i = 8;
                    string result = "";
                    if (immediate)
                    {
                        while (i > 0)
                        {
                            if (binACC[i] == 1 && mem[i] == 1)
                            {
                                result += "1";
                            }
                            else
                            {
                                result += "0";
                            }
                            i--;
                        }

                        ACC = convertToBase10(result);
                        i = 8;
                        result = "";
                    }
                    else
                    {
                        double value = memoryBook[mem];
                        string binVal = convertToBinary(value);
                        while (i > 0)
                        {
                            if (binACC[i] == 1 && binVal[i] == 1)
                            {
                                result += "1";
                            }
                            else
                            {
                                result += "0";
                            }
                            i--;
                        }

                        ACC = convertToBase10(result);
                        i = 8;
                        result = "";
                    }
                    break;
                case "000111":
                    //or, same question as and
                    string binACCOR = convertToBinary(Math.Abs(ACC));
                    if (ACC < 0)
                    {
                        binACCOR = negateBinary(binACCOR);
                    }
                    int j = 8;
                    string final = "";
                    if (immediate)
                    {
                        while (j > 0)
                        {
                            if (binACCOR[j] == 0 && mem[j] == 0)
                            {
                                final += "0";
                            }
                            else
                            {
                                final += "1";
                            }
                            j--;
                        }

                        ACC = convertToBase10(final);
                        j = 8;
                        final = "";
                    }
                    else
                    {
                        double value = memoryBook[mem];
                        string binVal = convertToBinary(value);
                        while (j > 0)
                        {
                            if (binACCOR[j] == 0 && binVal[j] == 0)
                            {
                                final += "0";
                            }
                            else
                            {
                                final += "1";
                            }
                            j--;
                        }

                        ACC = convertToBase10(final);
                        j = 8;
                        final = "";
                    }
                    break;
                case "001000":
                    //SHL, if value is negative, should we shift right?
                    if (immediate)
                    {
                        string binaryAcc = convertToBinary(ACC);
                        double value = convertToBase10(mem);
                        string afterSHL = "";
                        for (int k = (int)value; k < 8; k++)
                        {
                            afterSHL += binaryAcc[k];
                        }
                        for (int l = 0; l < (int)value; l++)
                        {
                            afterSHL += '0';
                        }
                            ACC = convertToBase10(afterSHL);
                    }
                    else
                    {
                        //throw exception at "index" line
                    }
                    break;
                case "001001":
                    //NOTA, if acc is negative, should it become postive?
                    string binACC2 = convertToBinary(ACC);
                    string resultACC = "";
                    for (int k = 0; k < binACC2.Length; k++) {
                        if (binACC2[k] == '0')
                        {
                            resultACC += '1';
                        }
                        else
                        {
                            resultACC += '0';
                        }
                    }
                    ACC = convertToBase10(resultACC);
                    resultACC = "";
                    break;
                //nothing happens to ACC for these 4 cases, it just jumps back to labelled line
                case "001010":
                    //BA

                    break;
                case "001011":
                    //BE

                    break;
                case "001100":
                    //BL

                    break;
                case "001101":
                    //BG

                    break;
                case "001110":
                    //NOP
                    ACC = ACC + 0;
                    break;
                case "001111":
                    //HLT
                    if (immediate)
                    {
                        ACC = convertToBase10(mem);
                    }
                    else
                    {
                        ACC = memoryBook[mem];
                    }
                    break;
            }
            index++;
            instr = "";
            mem = "";
        }

        public double convertToBase10(string x)
        {
            double num = 0;
            int exp = x.Length - 1;
            Console.WriteLine("x: " + x);
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == '1')
                {
                    Console.WriteLine("x[" + i +"] = " + x[i]);
                    num = num + Math.Pow(2, exp);
                    Console.WriteLine("exp: " + exp);
                    Console.WriteLine("num: " + num);
                }
                exp--;
            }
                return num;
        }

        //if this function doesn't work, it's because changed parameter from int to double
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
    }
}
