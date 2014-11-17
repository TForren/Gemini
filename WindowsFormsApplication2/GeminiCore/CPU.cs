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
        #region Our glorious wonderful CPU properties
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

        public string dispFetchInstr;
        public string dispDecodeInstr;
        public string dispExecuteInstr;
        public string dispStoreInstr;
        public string currentDisp;

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
        #endregion


        //THREEEEADDDDDSS!!!!!!  (╯°Д°）╯︵ ┻━┻
        Thread fetchThread;
        AutoResetEvent fetchEvent = new AutoResetEvent(false);
        Thread decodeThread;
        AutoResetEvent decodeEvent = new AutoResetEvent(false);
        Thread executeThread;
        AutoResetEvent executeEvent = new AutoResetEvent(false);
        Thread storeThread;
        AutoResetEvent storeEvent = new AutoResetEvent(false);

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

        private string instructionS;
        private Instruction instruction;
        private Storage storage;
        private Boolean storingDone = false;
        private Boolean lastFetch = false;
        private Boolean lastDecode = false;
        private Boolean lastExecute = false;
        private Boolean lastStore = false;

        private Boolean fetchRunning = false;
        private Boolean decodeRunning = false;
        private Boolean executeRunning = false;
        private Boolean storeRunning = false;

        bool areWeDone = false;

        public CPU()
        {
            ACC = 0;
            instructionS = "null";
            instruction = new Instruction("null","null",'n');
            storage = new Storage();
            storage.noValue = true;

            fetchThread = new Thread(new ThreadStart(PerformFetch));
            decodeThread = new Thread(new ThreadStart(PerformDecode));
            executeThread = new Thread(new ThreadStart(Execute));
            storeThread = new Thread(new ThreadStart(Store));

            fetchThread.Start();
            decodeThread.Start();
            executeThread.Start();
            storeThread.Start();
            
  //          decodeThread = new Thread(new ThreadStart(PerformDecode));
            
        }

        #region SetBinary/getMemory/Locationmap/Stuff
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

            instructionS = "";
            instruction = new Instruction();
            storage = new Storage();
            storingDone = false;
            lastFetch = false;
            lastDecode = false;
            lastExecute = false;
            lastStore = false;

            fetchRunning = false;
            decodeRunning = false;
            executeRunning = false;
            storeRunning = false;

            fetchEvent.Set();
            decodeEvent.Set();
            executeEvent.Set();
            storeEvent.Set();

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

            dispFetchInstr = "- - - - - - - - -";
            dispDecodeInstr = "- - - - - - - - -";
            dispExecuteInstr = "- - - - - - - - -";
            dispStoreInstr = "- - - - - - - - -";


        }
        #endregion

        public void nextInstruction()
        {
            //Console.WriteLine("String Instruction: " + instructionS);
            //Console.WriteLine("Struct Instruction " + instruction.opCode.ToString());
        //    Console.WriteLine("storage: " + storage.noValue);

            fetchEvent.Set();
            decodeEvent.Set();
            executeEvent.Set();
            storeEvent.Set();

            /*
            if (PC > 0)
            {
                decodeEvent.Set();
            }
            if (PC > 1)
            {
                executeEvent.Set();
            }
            if (PC > 2)
            {
                storeEvent.Set();
            }
            */
//            if (lastStore == true)
 //           {
  //              storeReset.WaitOne();
   //             finished = true;
    //        }

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

        #region Converting Stuff

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

        //if this function doesn't work, it's because changed parameter from int to double
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
        #endregion


        public string findInstruction(string binaryInstruction)
        {
            string opcode = "";
            string immediate = "";
            string operand = "";
            string instruction = "";
            int dispMem;
            for (int i = 0; i < binaryInstruction.Length; i++)
            {
                if (i >= 0 && i <= 5)
                {
                    opcode += binaryInstruction[i];
                }
                if (i == 6)
                {
                    if (binaryInstruction[i] == '1')
                    {
                        immediate = "#$";
                    }
                    else
                    {
                        immediate = "$";
                    }
                }
                if (i >= 8)
                {
                    operand += binaryInstruction[i];
                }
            }
            #region Switch case for instructions
            switch (opcode)
            {
                case "100000":
                    var labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = labelName.Key + ":";
                    break;
                case "000000":
                    //LDA
                    //Console.WriteLine("in find instruction lda");
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "lda " + immediate + dispMem;
                    break;
                case "000001":
                    //STA
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "sta " + immediate + dispMem;
                    break;
                case "000010":
                    //ADD
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "add " + immediate + dispMem;
                    break;
                case "000011":
                    //SUB
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "sub " + immediate + dispMem;
                    break;
                case "000100":
                    //mul
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "mul " + immediate + dispMem;
                    break;
                case "000101":
                    //div
                    dispMem = Convert.ToInt32(operand);
                    instruction = "div " + immediate + dispMem;
                    break;
                case "000110":
                    //and
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "and " + immediate + dispMem;
                    break;
                case "000111":
                    //or
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "or " + immediate + dispMem;
                    break;
                case "001000":
                    //SHL
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "shl " + immediate + dispMem;
                    break;
                case "001001":
                    //NOTA
                    instruction = "nota";
                    break;
                //never gets here in storage, branches don't need storage
                case "001010":
                    //BA
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = "ba " + labelName;
                    //PC = labelName.Value - 1;
                    break;
                case "001011":
                    //BE, branch if ACC is zero
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == ((int)(convertToBase10(operand))));
                    instruction = "be " + labelName;
                    break;
                case "001100":
                    //BL
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = "bl " + labelName;
                    break;
                case "001101":
                    //BG
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = "bg " + labelName;
                    break;
                case "001110":
                    //NOP
                    instruction = "nop";
                    break;
                case "001111":
                    //HLT
                    instruction = "hlt";
                    break;
            }
            #endregion

            return instruction;
        }

        public string findInstruction(Instruction findIn)
        {
            string instruction = "";
            string opcode = findIn.opCode;
            string operand = findIn.operand;
            string immediate = "";
            if (findIn.immediate == '0')
            {
                immediate = "$";
            }
            else
            {
                immediate = "#$";
            }
            #region Switch case for instructions
            switch (opcode)
            {
                case "100000":
                    var labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = labelName.Key + ":";
                    break;
                case "000000":
                    //LDA
                    //Console.WriteLine("in find instruction lda");
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "lda " + immediate + dispMem;
                    break;
                case "000001":
                    //STA
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "sta " + immediate + dispMem;
                    break;
                case "000010":
                    //ADD
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "add " + immediate + dispMem;
                    break;
                case "000011":
                    //SUB
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "sub " + immediate + dispMem;
                    break;
                case "000100":
                    //mul
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "mul " + immediate + dispMem;
                    break;
                case "000101":
                    //div
                    dispMem = Convert.ToInt32(operand);
                    instruction = "div " + immediate + dispMem;
                    break;
                case "000110":
                    //and
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "and " + immediate + dispMem;
                    break;
                case "000111":
                    //or
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "or " + immediate + dispMem;
                    break;
                case "001000":
                    //SHL
                    dispMem = Convert.ToInt32(convertToBase10(operand));
                    instruction = "shl " + immediate + dispMem;
                    break;
                case "001001":
                    //NOTA
                    instruction = "nota";
                    break;
                //never gets here in storage, branches don't need storage
                case "001010":
                    //BA
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = "ba " + labelName;
                    //PC = labelName.Value - 1;
                    break;
                case "001011":
                    //BE, branch if ACC is zero
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == ((int)(convertToBase10(operand))));
                    instruction = "be " + labelName;
                    break;
                case "001100":
                    //BL
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = "bl " + labelName;
                    break;
                case "001101":
                    //BG
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = "bg " + labelName;
                    break;
                case "001110":
                    //NOP
                    instruction = "nop";
                    break;
                case "001111":
                    //HLT
                    instruction = "hlt";
                    break;
            }
            #endregion

            return instruction;
        }
        public void PerformFetch()
        {
            while (!lastFetch)
            {
                fetchEvent.WaitOne();

                Console.WriteLine("In Fetch");
                currentDisp = findInstruction(binary[PC]);
                dispFetchInstr = currentDisp;
                if (PC == binary.Count - 1)
                {
                    Console.WriteLine("Last fetch!");
                    lastFetch = true;
                }
                instructionS = binary[PC];
                //Console.WriteLine("instructionS " + instructionS);
                //PC++;

                // if (OnFetchDone != null)
                //  {
                //      OnFetchDone(this, new FetchEventArgs(this.IR));
                //  }

                #region Our old Fetch Code

                //    Console.WriteLine("Fetch Started");
                //  while (!lastFetch)
                //{
                //      Console.WriteLine("In Fetched While loop");
                //      fetchReset.WaitOne();
                //     if (PC == binary.Count - 1)
                //     {
                //          Console.WriteLine("Last fetch!");
                //          lastFetch = true;
                //       }
                //      instructionS = binary[PC];
                //  }
                //return temp;
                // fetchReset.WaitOne();

                #endregion
            }
        }

        public void PerformDecode()
        {

            while (!lastDecode)
            {
                executeEvent.WaitOne();
                //IR_D.Decode(this.IR);

                //dispDecodeInstr = findInstruction(instructionS);

                Console.WriteLine("In Decode");
                dispDecodeInstr = findInstruction(instructionS);
                if (lastFetch == true)
                {
                    lastDecode = true;
                }
                // Console.WriteLine("instructionS in decode " + instructionS);

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
                instr = "";
                mem = "";
            }

            #region Our Old Decode Code
            /*
             while (!lastDecode)
             {
                decodeEvent.WaitOne();
                Console.WriteLine("In Decode While loop");
                if (lastFetch == true)
                {
                    lastDecode = true;
                }
               // Console.WriteLine("instructionS in decode " + instructionS);
                if (instructionS == "null")
                {
                    decodeEvent.WaitOne();
                }
                else
                {
                    decodeEvent.Set();
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
                instr = "";
                mem = "";
            }
            Console.WriteLine("Decode Reset");
            //return instruction;
            */
            #endregion
        }

        public void Execute()
        {
            while (!lastExecute)
            {
                executeEvent.WaitOne();

                Console.WriteLine("in Execute");
                dispExecuteInstr = findInstruction(instruction);
                if (lastDecode == true)
                {
                    lastExecute = true;
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
                        //Console.WriteLine("In Execute next instr: " + nextInst);
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
           // return storage;
        }

        public void Store()
        {
            while (!lastStore)
            {
                storeEvent.WaitOne();

                Console.WriteLine("in Store");
                dispStoreInstr = findInstruction(storage.instruction);
                if (lastExecute == true)
                {
                    lastStore = true;
                    finished = true;
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
                            var labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(storage.instruction.operand)));
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
                            labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(storage.instruction.operand)));
                            PC = labelName.Value - 1;

                            break;
                        case "001011":
                            //BE, branch if ACC is zero
                            labelName = LabelLocationMap.FirstOrDefault(x => x.Value == ((int)(convertToBase10(storage.instruction.operand))));
                            int newMem = (int)(convertToBase10(mem));
                            break;
                        case "001100":
                            //BL
                            labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(storage.instruction.operand)));
                            break;
                        case "001101":
                            //BG
                            labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(storage.instruction.operand)));
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
                if (finished)
                {
                    fetchEvent.Reset();
                    decodeEvent.Reset();
                    executeEvent.Reset();
                    storeEvent.Reset();
                }
            }
        }

    }
}