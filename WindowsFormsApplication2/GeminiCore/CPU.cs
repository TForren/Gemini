/**
 * Teague Forren
 * Johanna Jan
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace GeminiCore
{
    public class CPU : IDisposable
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
        AutoResetEvent allThreadsDone = new AutoResetEvent(false);
        object allThreadsDoneLock = new object();

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

        private string instructionSOrig;
        private Instruction instructionOrig;
        private Storage storageOrig; 
        
        private Boolean lastFetch = false;
        private Boolean lastDecode = false;
        private Boolean lastExecute = false;
        private Boolean lastStore = false;

        private Boolean lastFetchOrig = false;
        private Boolean lastDecodeOrig = false;
        private Boolean lastExecuteOrig = false;
        private Boolean lastStoreOrig = false;

        public event FetchDone OnFetchDone;
        public event DecodeDone OnDecodeDone;
        public event FetchDone OnExecuteDone;
        public event DecodeDone OnStoreDone;

        public delegate void FetchDone(object sender, OperationEventArgs args);
        public delegate void DecodeDone(object sender, OperationEventArgs args);
        public delegate void ExecuteDone(object sender, OperationEventArgs args);
        public delegate void StoreDone(object sender, OperationEventArgs args);

        private int counter = 0;
        private int numInstLeft;
        public bool hazard = false;
        public bool loadHazard = false;
        private int hazardCount = 0;
        private int loadHazardCount = 0;
        private Int16 ACCforX;
        private bool storeHazard = false;

        private Int16 ACCOrig;

        private bool fetchDone, decodeDone, executeDone, storeDone;
        private bool branching = false;
        private bool branchingFetch = false;
        private bool branchingDecode = false;
        private bool branchingExecute = false;
        private bool branchingStore = false;
        private Boolean fetchRunning = false;
        private Boolean decodeRunning = false;
        private Boolean executeRunning = false;
        private Boolean storeRunning = false;

        bool areWeDone = false;

        public CPU()
        {
            ACC = 0;
            //instructionS = "null";
            //instructionSOrig = "null";
            //instruction = new Instruction("null","null",'n');
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

            this.OnFetchDone += CPU_OnThreadDone;
            this.OnDecodeDone += CPU_OnThreadDone;
            this.OnExecuteDone += CPU_OnThreadDone;
            this.OnStoreDone += CPU_OnThreadDone;

            numInstLeft = 0;
            //          decodeThread = new Thread(new ThreadStart(PerformDecode));

        }

        public void setNumInstLeft(int x)
        {
            numInstLeft = x;
        }

        public int getNumInstLeft()
        {
            return numInstLeft;
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
            //storingDone = false;
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

        void CPU_OnThreadDone(object sender, OperationEventArgs args)
        {
            // if all threads are done (use bools)
            switch (args.CurrentThreadType)
            {
                case ThreadType.Fetch:
                    fetchDone = true;
                    break;
                case ThreadType.Decode:
                    decodeDone = true;
                    break;
                case ThreadType.Execute:
                    executeDone = true;
                    break;
                case ThreadType.Store:
                    storeDone = true;
                    break;


            }

            lock (allThreadsDoneLock)
            {
                if (fetchDone && decodeDone && executeDone && storeDone)
                {
                    allThreadsDone.Set();
                }
            }
        }

        public void Dispose()
        {
            areWeDone = true;
            fetchEvent.Set();
            fetchThread.Join();

            decodeEvent.Set();
            decodeThread.Join();
        }

        public void nextInstruction()
        {
            //Console.WriteLine("String Instruction: " + instructionS);
            //Console.WriteLine("Struct Instruction " + instruction.opCode.ToString());
        //    Console.WriteLine("storage: " + storage.noValue);

            if (loadHazard)
            {
                if (loadHazardCount < 1)
                {
                    loadHazardCount++;
                    dispFetchInstr = "Delayed";
                    dispDecodeInstr = "Delayed";
                    dispExecuteInstr = "Delayed";
                    dispStoreInstr = "Delayed";
                }
                else
                {
                    loadHazard = false;
                    loadHazardCount = 0;
                }
            }
            else if (hazard)
            {
                if (hazardCount < 4)
                {
                    hazardCount++;
                    dispFetchInstr = "Delayed";
                    dispDecodeInstr = "Delayed";
                    dispExecuteInstr = "Delayed";
                    dispStoreInstr = "Delayed";
                }
                else
                {
                    hazard = false;
                    hazardCount = 0;
                }
            }
            else if (branching)
            {
                cleanBranching();
                instructionS = instructionSOrig;
                instruction = instructionOrig;
                storage = storageOrig;
                dispFetchInstr = "Wiping";
                dispDecodeInstr = "Wiping";
                dispExecuteInstr = "Wiping";
                dispStoreInstr = "Wiping";
                branching = false;
                Console.WriteLine("NumInst left " + numInstLeft);
                Console.WriteLine("counter is " + counter);
            }

            else
            {
                fetchEvent.Set();
                decodeEvent.Set();
                executeEvent.Set();
                storeEvent.Set();

                fetchDone = false;
                decodeDone = false;
                executeDone = false;
                storeDone = false;

                allThreadsDone.WaitOne();

                instructionS = instructionSOrig;
                instruction = instructionOrig;
                storage = storageOrig;
                ACC = ACCOrig;

                if (counter >= numInstLeft + 1)
                {
                    finished = true;
                }

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
                counter++;
                instr = "";
                mem = "";
            }

            //PC++;
            //counter++;
            //instr = "";
            //mem = "";
            /*
            Console.WriteLine("count " + PC);
            if (PC < binary.Count - 1)
            {
                fetchEvent.Set();
            }
            if (PC > 0 && PC < binary.Count)
            {
                decodeEvent.Set();
            }
            if (PC > 1 && PC < binary.Count + 1)
            {
                executeEvent.Set();
            }
            if (PC > 2 && PC < binary.Count + 2)
            {
                storeEvent.Set();
            }
            if (PC >= binary.Count + 2)
            {
                finished = true;
            }
            */




            /*
            if (lastStore)
            {
                lastStore = true;
                Console.WriteLine("finished");

                finished = true;
            }
            if (lastExecute)
            {
                lastStore = true;
                Console.WriteLine("last store");

            }
            if (lastDecode)
            {
                lastExecute = true;
                Console.WriteLine("last execute");

            }
            if (lastFetch)
            {
                lastDecode = true;
                Console.WriteLine("last decode");

            }
            if (PC == binary.Count - 1)
            {
                lastFetch = true;
                Console.WriteLine("last fetch");
            }
            */
            /*
            lastFetch = lastFetchOrig;
            lastDecode = lastDecodeOrig;
            lastExecute = lastExecuteOrig;
            lastStore = lastStoreOrig;
            */
            /*
            if (lastStore == true)
            {
                storeEvent.WaitOne();
                finished = true;
            }
            */


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

        public void cleanBranching()
        {
            instructionSOrig = "";
            instructionOrig = new Instruction();
            storageOrig = new Storage();
            //storageOrig.noValue = true;
            setNumInstLeft(binary.Count - PC);
            counter = 0;
        }

        public string findInstruction(string binaryInstruction)
        {
            string opcode = "";
            string immediate = "";
            string operand = "";
            string instruction = "";
            int dispMem;
            //Console.WriteLine(binaryInstruction);
            //Console.WriteLine(binaryInstruction.Length);
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
                    instruction = "ba " + labelName.Key;
                    //PC = labelName.Value - 1;
                    break;
                case "001011":
                    //BE, branch if ACC is zero
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == ((int)(convertToBase10(operand))));
                    instruction = "be " + labelName.Key;
                    break;
                case "001100":
                    //BL
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = "bl " + labelName.Key;
                    break;
                case "001101":
                    //BG
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = "bg " + labelName.Key;
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
                    dispMem = Convert.ToInt32(convertToBase10(operand));
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
                    instruction = "ba " + labelName.Key;
                    //PC = labelName.Value - 1;
                    break;
                case "001011":
                    //BE, branch if ACC is zero
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == ((int)(convertToBase10(operand))));
                    instruction = "be " + labelName.Key;
                    break;
                case "001100":
                    //BL
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = "bl " + labelName.Key;
                    break;
                case "001101":
                    //BG
                    labelName = LabelLocationMap.FirstOrDefault(x => x.Value == (int)(convertToBase10(operand)));
                    instruction = "bg " + labelName.Key;
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
            while (!areWeDone)
            {

                fetchEvent.WaitOne();
                
                //Console.WriteLine("In Fetch");
                if (branchingFetch)
                {
                    instructionSOrig = "";
                    branchingFetch = false;
                }
                else if (counter < numInstLeft && !branchingFetch)
                {
                    
                    if (PC == binary.Count)
                    {
                        Console.WriteLine("Last fetch!");
                        lastFetch = true;
                    }

                    dispFetchInstr = findInstruction(binary[PC]);
                    instructionSOrig = binary[PC];
                }
                if (OnFetchDone != null)
                 {
                    OnFetchDone(this, new OperationEventArgs(ThreadType.Fetch));
                 }
            }
        }

        public void PerformDecode()
        {
            while (!areWeDone)
            {
                decodeEvent.WaitOne();
                //IR_D.Decode(this.IR);

                //dispDecodeInstr = findInstruction(instructionS);
                // Console.WriteLine("In Decode");

                if (branchingDecode)
                {
                    instructionOrig = new Instruction();
                    branchingDecode = false;
                }
                else if (counter > 0 && counter < numInstLeft + 1 && !branchingDecode)
                {
                    if (lastFetch == true)
                    {
                        lastDecode = true;
                    }
                     
                    dispDecodeInstr = findInstruction(instructionS);
                    string instr = "";
                    char imm = ' ';
                    char sign;
                    string mem = "";
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

                    instructionOrig.opCode = instr;
                    instructionOrig.operand = mem;
                    instructionOrig.immediate = imm;
                    instr = "";
                    imm = ' ';
                    mem = "";
                }

                if (OnDecodeDone != null)
                {
                    OnDecodeDone(this, new OperationEventArgs(ThreadType.Decode));
                }

            }

        }

        public void Execute()
        {
            while (!areWeDone)
            {
                executeEvent.WaitOne();

                //Console.WriteLine("in Execute");
                if (branchingExecute)
                {
                    //storageOrig = new Storage();
                    branchingExecute = false;
                }
                else if (counter > 1 && counter < numInstLeft + 2 && !branchingExecute)
                {
                    if (lastDecode == true)
                    {
                        lastExecute = true;
                    }
                    
                    dispExecuteInstr = findInstruction(instruction);
                    storageOrig = new Storage();
                    storageOrig.instruction = instruction;
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
                            storageOrig.noValue = true;
                            var labelName =
                                LabelLocationMap.FirstOrDefault(
                                    x => x.Value == (int) (convertToBase10(instruction.operand)));
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
                                ACCforX = Convert.ToInt16(convertToBase10(instruction.operand));
                            }
                            else
                            {
                                loadHazard = true;
                                ACCforX = mainMemory.getValue(convertToBase10(instruction.operand));

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
                                mainMemory.setValue(convertToBase10(instruction.operand), ACCforX);
                                //storageOrig.value = ACC;
                                //Console.WriteLine("in execute storing value " + ACC);
                                //storage.value = Convert.ToInt16(convertToBase10(instr.operand));
                            }
                            break;
                        case "000010":
                            //ADD
                            if (imm)
                            {
                                Console.WriteLine("adding " + convertToBase10(instruction.operand) + " to " + ACCforX + " in X");
                                //storageOrig.value = (Int16) (ACC + convertToBase10(instruction.operand));
                                ACCforX = (Int16)(ACCforX + convertToBase10(instruction.operand));

                            }
                            else
                            {
                                Console.WriteLine("Execute: Adding value of: " + mainMemory.getValue(convertToBase10(instruction.operand)) + "To " + ACC);
                                //storageOrig.value = (Int16) (ACC + mainMemory.getValue(convertToBase10(instruction.operand)));
                                ACCforX = (Int16)(ACCforX + mainMemory.getValue(convertToBase10(instruction.operand)));

                            }
                            break;
                        case "000011":
                            //SUB
                            if (imm)
                            {
                                Console.WriteLine("subtract " + convertToBase10(instruction.operand) + " from " + ACCforX);
                                //storageOrig.value = (Int16) (ACC - convertToBase10(instruction.operand));
                                ACCforX = (Int16)(ACCforX - convertToBase10(instruction.operand));
                            }
                            else
                            {
                                //storageOrig.value = (Int16) (ACC - mainMemory.getValue(convertToBase10(instruction.operand)));
                               ACCforX = (Int16)(ACCforX - mainMemory.getValue(convertToBase10(instruction.operand)));

                            }
                            break;
                        case "000100":
                            //mul
                            hazard = true;
                            if (imm)
                            {
                                //storageOrig.value = (Int16) (ACC*convertToBase10(instruction.operand));
                                ACCforX = (Int16)(ACCforX * convertToBase10(instruction.operand));

                            }
                            else
                            {
                                //storageOrig.value = (Int16) (ACC*mainMemory.getValue(convertToBase10(instruction.operand)));
                                ACCforX = (Int16)(ACCforX * mainMemory.getValue(convertToBase10(instruction.operand)));

                            }
                            break;
                        case "000101":
                            //div
                            hazard = true;
                            if (imm)
                            {
                                //storageOrig.value = (Int16) (ACC/convertToBase10(instruction.operand));
                                ACCforX = (Int16)(ACCforX / convertToBase10(instruction.operand));
                            }
                            else
                            {
                               // storageOrig.value = (Int16) (ACC/mainMemory.getValue(convertToBase10(instruction.operand)));
                                ACCforX = (Int16)(ACCforX / mainMemory.getValue(convertToBase10(instruction.operand)));
                            }
                            break;
                        case "000110":
                            //and
                            if (imm)
                            {
                                //storageOrig.value = (Int16) (ACC & convertToBase10(instruction.operand));
                               ACCforX = (Int16)(ACCforX & convertToBase10(instruction.operand));
                            }
                            else
                            {
                               // storageOrig.value = (Int16) (ACC & mainMemory.getValue(convertToBase10(instruction.operand)));
                                ACCforX = (Int16)(ACCforX & mainMemory.getValue(convertToBase10(instruction.operand)));
                            }
                            break;
                        case "000111":
                            //or
                            if (imm)
                            {
                                //storageOrig.value = (Int16) (ACC | (Int16) (convertToBase10(instruction.operand)));
                                ACCforX = (Int16)(ACCforX | (Int16)(convertToBase10(instruction.operand)));
                            }
                            else
                            {
                                //storageOrig.value = (Int16) (ACC | mainMemory.getValue(convertToBase10(instruction.operand)));
                                ACCforX = (Int16)(ACCforX | mainMemory.getValue(convertToBase10(instruction.operand)));
                            }
                            break;
                        case "001000":
                            //SHL
                            if (imm)
                            {
                                string binAcc = convertToBin(ACCforX);
                                Int16 value = (Int16) (convertToBase10(instruction.operand));
                                string afterSHL = "";
                                for (int k = (int) value; k < binAcc.Length; k++)
                                {
                                    afterSHL += binAcc[k];
                                }
                                for (int j = 0; j < (int) value; j++)
                                {
                                    afterSHL += '0';
                                }
                                ACCforX = (Int16) (convertToBase10(afterSHL));
                            }
                            break;
                        case "001001":
                            //NOTA
                            ACCforX = (Int16) (~ACCforX);
                            break;

                            //nothing happens to ACC for these 4 cases, it just jumps back to labelled line
                        case "001010":
                            //BA
                            labelName =
                                LabelLocationMap.FirstOrDefault(
                                    x => x.Value == (int) (convertToBase10(instruction.operand)));
                            dispLab = labelName.Key;
                            branching = true;
                            //branchingFetch = true;
                            //branchingDecode = true;
                            //branchingExecute = true;
                            //branchingStore = true;
                            //PC = labelName.Value - 1;

                            //added for branch predictions
                            PC = labelName.Value - 1;
                            setNumInstLeft(binary.Count - PC);
                            counter = 0;
                            //counter --;
                            //

                            storageOrig.noValue = true;

                            nextInst = "ba " + dispLab;
                            MAR = "- - - - - - ";
                            MDR = "- - - - - - ";
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "001011":
                            //BE, branch if ACC is zero
                            labelName =
                                LabelLocationMap.FirstOrDefault(
                                    x => x.Value == ((int) (convertToBase10(instruction.operand))));
                            dispLab = labelName.Key;
                            int newMem = (int) (convertToBase10(instruction.operand));
                            if (ACCforX == 0)
                            {
                                branching = true;
                                //branchingFetch = true;
                                //branchingDecode = true;
                                //branchingExecute = true;
                                //branchingStore = true;
                                //PC = newMem - 1;
                                PC = newMem - 1;
                                setNumInstLeft(binary.Count - PC);
                                counter = 0;
                                //counter--;
                            }
                            storageOrig.noValue = true;

                            nextInst = "be " + dispLab;
                            MAR = "- - - - - - ";
                            MDR = "- - - - - - ";
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "001100":
                            //BL
                            labelName =
                                LabelLocationMap.FirstOrDefault(
                                    x => x.Value == (int) (convertToBase10(instruction.operand)));
                            dispLab = labelName.Key;
                            Console.WriteLine("ACC in BL X is " + ACCforX);
                            if (ACCforX < 0)
                            {
                                branching = true;
                                //branchingFetch = true;
                                //branchingDecode = true;
                                //branchingExecute = true;
                                //branchingStore = true;

                                //PC = labelName.Value - 1;
                                PC = labelName.Value - 1;
                                setNumInstLeft(binary.Count - PC);
                                counter = 0;
                                //counter--;
                            }
                            storageOrig.noValue = true;

                            nextInst = "bl " + dispLab;
                            MAR = "- - - - - - ";
                            MDR = "- - - - - - ";
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "001101":
                            //BG
                            labelName =
                                LabelLocationMap.FirstOrDefault(
                                    x => x.Value == (int) (convertToBase10(instruction.operand)));
                            dispLab = labelName.Key;
                            if (ACCforX > 0)
                            {
                                branching = true;
                                //branchingFetch = true;
                                //branchingDecode = true;
                                //branchingExecute = true;
                                //branchingStore = true;

                                //PC = labelName.Value - 1;
                                PC = labelName.Value - 1;
                                setNumInstLeft(binary.Count - PC);
                                counter = 0;
                                //counter--;
                            }
                            storageOrig.noValue = true;

                            nextInst = "bg " + dispLab;
                            MAR = "- - - - - - ";
                            MDR = "- - - - - - ";
                            mainMemory.HitorMiss = "  --- ";
                            break;
                        case "001110":
                            //NOP
                            ACCforX = (Int16) (ACCforX + 0);
                            break;
                        case "001111":
                            //HLT
                            storageOrig.noValue = true;
                            finished = true;
                            nextInst = "hlt";
                            MAR = "- - - - - - ";
                            MDR = "- - - - - - ";
                            mainMemory.HitorMiss = "";
                            //finished = true;
                            PC = 0;
                            break;
                    }
                    if (!storageOrig.noValue)
                    {
                        storageOrig.value = ACCforX;
                    }

                    #endregion

                    Console.WriteLine(dispExecuteInstr);
                }

                if (OnExecuteDone != null)
                {
                    OnExecuteDone(this, new OperationEventArgs(ThreadType.Execute));
                }

            }
        }

        public void Store()
        {
            while (!areWeDone)
            {
                storeEvent.WaitOne();

                //Console.WriteLine("in Store");
                if (counter > 2 && counter < numInstLeft + 3)
                {
                    if (lastExecute == true)
                    {
                        lastStore = true;
                        //finished = true;
                    }
                     
                    dispStoreInstr = findInstruction(storage.instruction);
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
                                var labelName =
                                    LabelLocationMap.FirstOrDefault(
                                        x => x.Value == (int) (convertToBase10(storage.instruction.operand)));
                                break;
                            case "000000":
                                //LDA
                                if (imm)
                                {
                                    ACCOrig = storage.value;
                                    MAR = "- - - - - - -";
                                    mainMemory.HitorMiss = "  --- ";
                                    MDR = mem;
                                }
                                else
                                {
                                    ACCOrig = storage.value;
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
                                    Console.WriteLine("Execute: Storing ACC value of: " + ACCOrig);
                                    mainMemory.setValue(convertToBase10(storage.instruction.operand), ACCOrig);
                                    //mainMemory.setValue(convertToBase10(storage.instruction.operand), storage.value);
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
                                    Console.WriteLine("storing " + ACCOrig + " ADD");
                                    ACCOrig = storage.value;
                                    MAR = "- - - - - - ";
                                    MDR = storage.instruction.operand;
                                }
                                else
                                {
                                    ACCOrig = storage.value;
                                    MAR = storage.instruction.operand;
                                    MDR =
                                        convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));
                                }
                                dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                                nextInst = "add " + dispImm + dispMem;
                                mainMemory.HitorMiss = "  --- ";
                                Console.WriteLine(mainMemory.getMemoryBook());
                                break;
                            case "000011":
                                //SUB

                                if (imm)
                                {
                                    ACCOrig = storage.value;
                                    MAR = "- - - - - - - ";
                                    MDR = storage.instruction.operand;
                                }
                                else
                                {
                                    ACCOrig = storage.value;
                                    MAR = storage.instruction.operand;
                                    MDR =
                                        convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));
                                }
                                dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                                nextInst = "sub " + dispImm + dispMem;
                                mainMemory.HitorMiss = "  --- ";
                                break;
                            case "000100":
                                //mul
                                if (imm)
                                {
                                    ACCOrig = storage.value;
                                    MAR = "- - - - - - - ";
                                    MDR = storage.instruction.operand;
                                }
                                else
                                {
                                    ACCOrig = storage.value;
                                    MAR = storage.instruction.operand;
                                    MDR =
                                        convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));

                                }
                                dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                                nextInst = "mul " + dispImm + dispMem;
                                mainMemory.HitorMiss = "  --- ";
                                break;
                            case "000101":
                                //div
                                if (imm)
                                {
                                    ACCOrig = storage.value;
                                    MAR = "- - - - - - - ";
                                    MDR = storage.instruction.operand;
                                }
                                else
                                {
                                    ACCOrig = storage.value;
                                    MAR = storage.instruction.operand;
                                    MDR =
                                        convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));
                                }
                                dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                                nextInst = "div " + dispImm + dispMem;
                                mainMemory.HitorMiss = "  --- ";
                                break;
                            case "000110":
                                //and
                                if (imm)
                                {
                                    ACCOrig = storage.value;
                                    MAR = "- - - - - - -";
                                    MDR = storage.instruction.operand;
                                }
                                else
                                {
                                    ACCOrig = storage.value;
                                    MAR = storage.instruction.operand;
                                    //MDR = convertToBinary(mainMemory.getMemoryBook()[convertToBase10(mem)]);
                                    MDR =
                                        convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));
                                }
                                dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                                nextInst = "and " + dispImm + dispMem;
                                mainMemory.HitorMiss = "  --- ";
                                break;
                            case "000111":
                                //or
                                if (imm)
                                {
                                    ACCOrig = storage.value;
                                    MAR = "- - - - - - -";
                                    MDR = storage.instruction.operand;
                                }
                                else
                                {
                                    ACCOrig = storage.value;
                                    MAR = storage.instruction.operand;
                                    MDR =
                                        convertToBinary(mainMemory.getValue(convertToBase10(storage.instruction.operand)));
                                }
                                dispMem = Convert.ToInt32(convertToBase10(storage.instruction.operand));
                                nextInst = "or " + dispImm + dispMem;
                                mainMemory.HitorMiss = "  --- ";
                                break;
                            case "001000":
                                //SHL, if value is negative, should we shift right?
                                if (imm)
                                {
                                    ACCOrig = storage.value;
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
                                ACCOrig = storage.value;
                                MAR = "- - - - - - ";
                                MDR = "- - - - - - ";
                                nextInst = "nota";
                                mainMemory.HitorMiss = "  --- ";
                                break;
                                //never gets here in storage, branches don't need storage
                            case "001010":
                                //BA
                                labelName =
                                    LabelLocationMap.FirstOrDefault(
                                        x => x.Value == (int) (convertToBase10(storage.instruction.operand)));
                                PC = labelName.Value - 1;

                                break;
                            case "001011":
                                //BE, branch if ACC is zero
                                labelName =
                                    LabelLocationMap.FirstOrDefault(
                                        x => x.Value == ((int) (convertToBase10(storage.instruction.operand))));
                                int newMem = (int) (convertToBase10(mem));
                                break;
                            case "001100":
                                //BL
                                labelName =
                                    LabelLocationMap.FirstOrDefault(
                                        x => x.Value == (int) (convertToBase10(storage.instruction.operand)));
                                break;
                            case "001101":
                                //BG
                                labelName =
                                    LabelLocationMap.FirstOrDefault(
                                        x => x.Value == (int) (convertToBase10(storage.instruction.operand)));
                                break;
                            case "001110":
                                //NOP
                                ACCOrig = storage.value;
                                nextInst = "nop";
                                MAR = "- - - - - - ";
                                MDR = "- - - - - - ";
                                mainMemory.HitorMiss = "  --- ";
                                break;
                            case "001111":
                                //HLT
                                PC = 0;
                                finished = true;
                                break;
                        }
                        A = convertToBinary(storage.value);

                        B = storage.instruction.operand;

                        #endregion

                        // Console.WriteLine("storage value in store " + storage.value);
                    }

                    if (finished)
                    {
                        fetchEvent.Reset();
                        decodeEvent.Reset();
                        executeEvent.Reset();
                        storeEvent.Reset();
                    }
                }

                if (!storeHazard)
                {
                    if (OnStoreDone != null)
                    {
                        OnStoreDone(this, new OperationEventArgs(ThreadType.Store));
                    }
                }
                //storeEvent.WaitOne();

                //executeEvent.Set();
            }
            //finished = true;
        }

    }
}