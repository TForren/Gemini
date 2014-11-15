/**
 * Teague Forren
 * Johanna Jan
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace GeminiCore
{
    public class CPU
    {
        public Int16 ACC { get; private set; }
        public string A;
        public string B;
        public int Zero;
        public int One;
        public int PC = 0;
        public string MAR;
        public string MDR;
        public string TEMP;
        public string IR;
        public string CC;
        public string nextInst;
        public string dispImm;
        public string dispLab;

        public Int32 dispMem;

        private List<string> binary;
        private List<Int16> binary16;
        private Dictionary<string, int> LabelLocationMap;
        //Dictionary<string, Int16> memoryBook = new Dictionary<string, Int16>();
        private Memory mainMemory = new Memory();
        private string instr = "";
        private char imm = ' ';
        private char sign = ' ';
        private string mem = "";
        private string fetchedInst;
        public Boolean finished = false;
        public Boolean broken = false;

        Thread fetchThread;
        AutoResetEvent fetchReset = new AutoResetEvent(false);
        Thread decodeThread;
        AutoResetEvent decodeReset = new AutoResetEvent(false);
        Thread executeThread;
        AutoResetEvent executeReset = new AutoResetEvent(false);
        Thread storeThread;
        AutoResetEvent storeReset = new AutoResetEvent(false);
        
        string instructionS = "";
        private Instruction instruction;
        private Storage storage;
        private Boolean storingDone = false;
        private Boolean lastFetch = false;
        private Boolean lastDecode = false;
        private Boolean lastExecute = false;
        private Boolean lastStore = false;

        public struct Instruction {
            public string opCode;
            public string operand;
            public char immediate;

            public Instruction(string opCode, string operand, char immediate)
            {
                this.opCode = opCode;
                this.operand = operand;
                this.immediate = immediate;
            }        
        }

        public struct Storage {
            public Instruction instruction;
            public Int16 value;
            public Boolean noValue; //false if there's no value

            public Storage(Instruction instruction, Int16 value, Boolean noValue)
            {
                this.instruction = instruction;
                this.value = value;
                this.noValue = noValue;
            }
        }

        public CPU()
        {
            ACC = 0;
            fetchThread = new Thread(new ThreadStart(Fetched));
            decodeThread = new Thread(new ThreadStart(Decode));
            executeThread = new Thread(new ThreadStart(Execute));
            storeThread = new Thread(new ThreadStart(Store));

            fetchThread.Start();
            decodeThread.Start();
            executeThread.Start();
            storeThread.Start();
            
  //          decodeThread = new Thread(new ThreadStart(PerformDecode));

        }

        public Memory getMainMemory()
        {
            return mainMemory;
        }

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

        public void runAll()
        {
            for (int i = 0; i < binary.Count; i++)
            {
                nextInstruction();
            }
        }

        public void resetFields()
        {
            ACC = 0;
            A = "- - - - - - - - -";
            B = "- - - - - - - - -";
            Zero = 0;
            One = 1;
            PC = 0;
            MAR = "- - - - - - - - -";
            MDR = "- - - - - - - - -";
            TEMP = "- - - - - - - - -";
            IR = "- - - - - - - - -";
            CC = "- - - - - - - - -";
            nextInst = "- - - - - - - - -";
            mainMemory.HitorMiss = "  --- ";
            mainMemory.HITCOUNT = 0;
            mainMemory.MISSCOUNT = 0;
            finished = false;
            broken = false;
        }


        public void nextInstruction()
        {
            fetchReset.Set();
            decodeReset.Set();
            executeReset.Set();
            storeReset.Set();
            /*
            if (lastFetch == true)
            {
                fetchReset.WaitOne();
            }
            if (instructionS == "" || lastDecode == true)
            {
                decodeReset.WaitOne();
            }
            if (instruction.Equals(null) || lastExecute == true)
            {
                executeReset.WaitOne();
            }
            if (storage.Equals(null))
            {
                storeReset.WaitOne();
            }
             */
            if (lastStore == true)
            {
                storeReset.WaitOne();
                finished = true;
            }

           // Fetched();
           // Decode();
           // Execute();
           // Store();

            //if (PC + 1 == binary.Count)
            //{
               // finished = true;
               // PC = 0;
            //}
            //if (finished)
            //{
                //Console.WriteLine("finished");
            //}
            if (ACC > 0)
            {
                CC = "1";
            }
            else if (ACC < 0)
            {
                CC = "-1";
            }
            else
            {
                CC = "0";
            }
            PC++;
            instr = "";
            mem = "";
        }

        #region convertToBase10
        public int convertToBase10(string x)
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
            return (int)(num);
        }
        #endregion

        //if this function doesn't work, it's because changed parameter from int to double
        #region convertToBinary
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

            if (length < 8)
            {
                while (length < 8)
                {
                    result = result + "0";
                    length++;
                }
            }

            foreach (int i in bin)
            {
                result = result + Convert.ToString(i);
            }

            return result;
        }
        #endregion

        public string convertToBin(int x)
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

            foreach (int i in bin)
            {
                result = result + Convert.ToString(i);
            }

            return result;
        }

        #region negateBinary
        /*
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
         * */
        #endregion


        public void Fetched()
        {
            Console.WriteLine("Fetch Started");
            while (!lastFetch)
            {
                Console.WriteLine("In Fetched While loop");
                fetchReset.WaitOne();
                if (PC == binary.Count - 1)
                {
                    Console.WriteLine("Last fetch!");
                    lastFetch = true;
                }
                instructionS = binary[PC];
            }
            //return temp;
           // fetchReset.WaitOne();
            Console.WriteLine("Fetch Reset");
        }

        public void Decode()
        {
            Console.WriteLine("in Decode");
            while (!lastDecode)
            {
                decodeReset.WaitOne();
                Console.WriteLine("In Decode While loop");
                if (lastFetch == true)
                {
                    lastDecode = true;
                }
                if (instructionS == "")
                {
                    decodeReset.WaitOne();
                }
                else
                {
                    decodeReset.Set();
                }

                instruction = new Instruction();

                for (int i = 0; i < instructionS.Length; i++)
                {
                    if (i >= 0 && i <= 5)
                    {
                        instr = instr + instructionS[i];
                    }
                    else if (i == 6)
                    {
                        imm = instructionS[i];
                    }
                    else if (i == 7)
                    {
                        sign = instructionS[i];
                    }
                    else
                    {
                        mem = mem + instructionS[i];
                    }

                }
                instruction.opCode = instr;
                instruction.operand = mem;
                instruction.immediate = imm;
            }
            decodeReset.Reset();
            Console.WriteLine("Decode Reset");
            //return instruction;
        }

        public void Execute()
        {
            Console.WriteLine("in Execute");
            while (!lastExecute)
            {
                executeReset.WaitOne();
                Console.WriteLine("In Execute While loop");
                if (lastDecode == true)
                {
                    lastExecute = true;
                }
                if (instruction.Equals(null))
                {
                    executeReset.WaitOne();
                }
                else
                {
                    executeReset.Set();
                }
                storage = new Storage();
                storage.instruction = instruction;
                Boolean imm = false;
                string binInstr = instruction.opCode;
                if (instruction.immediate == '1')
                {
                    imm = true;
                    dispImm = "#$";
                }
                else
                {
                    dispImm = "$";
                }
                #region Case switch for instructions
                switch (binInstr)
                {
                    case "100000":
                        storage.noValue = true;
                        var labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(instruction.operand)));
                        nextInst = labelName.Key + ":";
                        MAR = "- - - - - - -";
                        MDR = "- - - - - - -";
                        mainMemory.HitorMiss = "  --- ";
                        break;
                    case "000000":
                        //LDA
                        if (imm)
                        {
                            storage.value = Convert.ToInt16(convertToBase10(instruction.operand));
                        }
                        else
                        {
                            storage.value = mainMemory.getValue(convertToBase10(instruction.operand));
                        }
                        break;
                    case "000001":
                        //STA
                        if (imm)
                        {
                            //throw exception, PC is line number
                        }
                        else
                        {
                            storage.value = ACC;
                            //storage.value = Convert.ToInt16(convertToBase10(instr.operand));
                        }
                        break;
                    case "000010":
                        //ADD
                        if (imm)
                        {
                            storage.value = (Int16)(ACC + convertToBase10(instruction.operand));
                        }
                        else
                        {
                            storage.value = (Int16)(ACC + mainMemory.getValue(convertToBase10(instruction.operand)));
                        }
                        break;
                    case "000011":
                        //SUB
                        if (imm)
                        {
                            storage.value = (Int16)(ACC - convertToBase10(instruction.operand));
                        }
                        else
                        {
                            storage.value = (Int16)(ACC - mainMemory.getValue(convertToBase10(instruction.operand)));
                        }
                        break;
                    case "000100":
                        //mul
                        if (imm)
                        {
                            storage.value = (Int16)(ACC * convertToBase10(instruction.operand));
                        }
                        else
                        {
                            storage.value = (Int16)(ACC * mainMemory.getValue(convertToBase10(instruction.operand)));
                        }
                        break;
                    case "000101":
                        //div
                        if (imm)
                        {
                            storage.value = (Int16)(ACC / convertToBase10(instruction.operand));
                        }
                        else
                        {
                            storage.value = (Int16)(ACC / mainMemory.getValue(convertToBase10(instruction.operand)));
                        }
                        break;
                    case "000110":
                        //and
                        if (imm)
                        {
                            storage.value = (Int16)(ACC & convertToBase10(instruction.operand));
                        }
                        else
                        {
                            storage.value = (Int16)(ACC & mainMemory.getValue(convertToBase10(instruction.operand)));
                        }
                        break;
                    case "000111":
                        //or
                        if (imm)
                        {
                            storage.value = (Int16)(ACC | (Int16)(convertToBase10(instruction.operand)));
                        }
                        else
                        {
                            storage.value = (Int16)(ACC | mainMemory.getValue(convertToBase10(instruction.operand)));
                        }
                        break;
                    case "001000":
                        //SHL
                        if (imm)
                        {
                            string binAcc = convertToBin(ACC);
                            Int16 value = (Int16)(convertToBase10(instruction.operand));
                            string afterSHL = "";
                            for (int k = (int)value; k < binAcc.Length; k++)
                            {
                                afterSHL += binAcc[k];
                            }
                            for (int j = 0; j < (int)value; j++)
                            {
                                afterSHL += '0';
                            }
                            storage.value = (Int16)(convertToBase10(afterSHL));
                        }
                        break;
                    case "001001":
                        //NOTA
                        storage.value = (Int16)(~ACC);
                        break;

                    //nothing happens to ACC for these 4 cases, it just jumps back to labelled line
                    case "001010":
                        //BA
                        labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(instruction.operand)));
                        dispLab = labelName.Key;
                        PC = labelName.Value - 1;
                        storage.noValue = true;

                        nextInst = "ba " + dispLab;
                        MAR = "- - - - - - ";
                        MDR = "- - - - - - ";
                        mainMemory.HitorMiss = "  --- ";
                        break;
                    case "001011":
                        //BE, branch if ACC is zero
                        labelName = LabelLocationMap.FirstOrDefault(x => x.Value == ((int)(convertToBase10(instruction.operand))));
                        dispLab = labelName.Key;
                        int newMem = (int)(convertToBase10(instruction.operand));
                        if (ACC == 0)
                        {
                            PC = newMem - 1;
                        }
                        storage.noValue = true;

                        nextInst = "be " + dispLab;
                        MAR = "- - - - - - ";
                        MDR = "- - - - - - ";
                        mainMemory.HitorMiss = "  --- ";
                        break;
                    case "001100":
                        //BL
                        labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(instruction.operand)));
                        dispLab = labelName.Key;
                        if (ACC < 0)
                        {
                            PC = labelName.Value - 1;
                        }
                        storage.noValue = true;

                        nextInst = "bl " + dispLab;
                        MAR = "- - - - - - ";
                        MDR = "- - - - - - ";
                        mainMemory.HitorMiss = "  --- ";
                        break;
                    case "001101":
                        //BG
                        labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(instruction.operand)));
                        dispLab = labelName.Key;
                        if (ACC > 0)
                        {
                            PC = labelName.Value - 1;
                        }
                        storage.noValue = true;

                        nextInst = "bg " + dispLab;
                        MAR = "- - - - - - ";
                        MDR = "- - - - - - ";
                        mainMemory.HitorMiss = "  --- ";
                        break;
                    case "001110":
                        //NOP
                        storage.value = (Int16)(ACC + 0);
                        break;
                    case "001111":
                        //HLT
                        storage.noValue = true;

                        nextInst = "hlt";
                        MAR = "- - - - - - ";
                        MDR = "- - - - - - ";
                        mainMemory.HitorMiss = "";
                        finished = true;
                        PC = 0;
                        break;
                }
                #endregion 
            }
            executeReset.Reset();
           // return storage;
        }

        public void Store()
        {
            Console.WriteLine("in Store");
            while (!lastStore)
            {
                storeReset.WaitOne();
                Console.WriteLine("In Store While loop");
                if (lastExecute == true)
                {
                    lastStore = true;
                    finished = true;
                }
                if (storage.Equals(null))
                {
                    storeReset.WaitOne();
                }
                else
                {
                    storeReset.Set();
                }
                Boolean imm = false;
                if (storage.instruction.immediate == '1')
                {
                    imm = true;
                    dispImm = "#$";
                }
                else
                {
                    dispImm = "$";
                }
                if (!storage.noValue)
                {
                    string binInstr = storage.instruction.opCode;
                    #region Switch case for instructions
                    switch (binInstr)
                    {
                        case "100000":
                            var labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(mem)));
                            break;
                        case "000000":
                            //LDA
                            if (imm)
                            {
                                ACC = storage.value;
                                MAR = "- - - - - - -";
                                mainMemory.HitorMiss = "  --- ";
                                MDR = mem;
                            }
                            else
                            {
                                ACC = storage.value;
                                MAR = mem;
                                MDR = convertToBinary(ACC);
                            }
                            dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                            nextInst = "lda " + dispImm + dispMem;
                            break;
                        case "000001":
                            //STA

                            if (imm)
                            {
                                //exception
                            }
                            else
                            {
                                mainMemory.setValue(convertToBase10(storage.instruction.operand), storage.value);
                                MAR = storage.instruction.operand;
                                MDR = convertToBinary(storage.value);
                            }

                            dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                            nextInst = "sta " + dispImm + dispMem;
                            break;
                        case "000010":
                            //ADD
                            if (imm)
                            {
                                ACC = storage.value;
                                MAR = "- - - - - - ";
                                MDR = storage.instruction.operand;
                            }
                            else
                            {
                                ACC = storage.value;
                                MAR = storage.instruction.operand;
                                MDR = convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));
                            }
                            dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                            nextInst = "add " + dispImm + dispMem;
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "000011":
                            //SUB

                            if (imm)
                            {
                                ACC = storage.value;
                                MAR = "- - - - - - - ";
                                MDR = storage.instruction.operand;
                            }
                            else
                            {
                                ACC = storage.value;
                                MAR = storage.instruction.operand;
                                MDR = convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));
                            }
                            dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                            nextInst = "sub " + dispImm + dispMem;
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "000100":
                            //mul
                            if (imm)
                            {
                                ACC = storage.value;
                                MAR = "- - - - - - - ";
                                MDR = storage.instruction.operand;
                            }
                            else
                            {
                                ACC = storage.value;
                                MAR = storage.instruction.operand;
                                MDR = convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));

                            }
                            dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                            nextInst = "mul " + dispImm + dispMem;
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "000101":
                            //div
                            if (imm)
                            {
                                ACC = storage.value;
                                MAR = "- - - - - - - ";
                                MDR = storage.instruction.operand;
                            }
                            else
                            {
                                ACC = storage.value;
                                MAR = storage.instruction.operand;
                                MDR = convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));
                            }
                            dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                            nextInst = "div " + dispImm + dispMem;
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "000110":
                            //and
                            if (imm)
                            {
                                ACC = storage.value;
                                MAR = "- - - - - - -";
                                MDR = storage.instruction.operand;
                            }
                            else
                            {
                                ACC = storage.value;
                                MAR = storage.instruction.operand;
                                //MDR = convertToBinary(mainMemory.getMemoryBook()[convertToBase10(mem)]);
                                MDR = convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));
                            }
                            dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                            nextInst = "and " + dispImm + dispMem;
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "000111":
                            //or
                            if (imm)
                            {
                                ACC = storage.value;
                                MAR = "- - - - - - -";
                                MDR = storage.instruction.operand;
                            }
                            else
                            {
                                ACC = storage.value;
                                MAR = storage.instruction.operand;
                                MDR = convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));
                            }
                            dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                            nextInst = "or " + dispImm + dispMem;
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "001000":
                            //SHL, if value is negative, should we shift right?
                            if (imm)
                            {
                                ACC = storage.value;
                                MAR = "- - - - - - -";
                                MDR = storage.instruction.operand;
                            }
                            else
                            {
                                //throw exception at "PC" line
                            }
                            dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                            nextInst = "shl " + dispImm + dispMem;
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "001001":
                            //NOTA
                            ACC = storage.value;
                            MAR = "- - - - - - ";
                            MDR = "- - - - - - ";
                            nextInst = "nota";
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        //never gets here in storage, branches don't need storage
                        case "001010":
                            //BA
                            labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(mem)));
                            PC = labelName.Value - 1;

                            break;
                        case "001011":
                            //BE, branch if ACC is zero
                            labelName = LabelLocationMap.FirstOrDefault(x => x.Value == ((int)(convertToBase10(mem))));
                            int newMem = (int)(convertToBase10(mem));
                            break;
                        case "001100":
                            //BL
                            labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(mem)));
                            break;
                        case "001101":
                            //BG
                            labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(mem)));
                            break;
                        case "001110":
                            //NOP
                            ACC = storage.value;
                            nextInst = "nop";
                            MAR = "- - - - - - ";
                            MDR = "- - - - - - ";
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "001111":
                            //HLT
                            PC = 0;
                            break;
                    }
                    #endregion
                }
            }
            storeReset.Reset();
        }
    }
}