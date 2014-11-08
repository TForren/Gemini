/**
 * Teague Forren
 * Johanna Jan
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiCore
{
    public class Memory
    {
        public struct Frame
        {
            public Int16 value;
            public int tag;
            public Boolean dirty;
            public Boolean empty;

            public Frame(Int16 value, int tag, Boolean dirty, Boolean empty)
            {
                this.value = value;
                this.tag = tag;
                this.dirty = false;
                this.empty = true;
            }
        }

        public struct Frame2
        {
            public Int16[] value;
            public int tag;
            public Boolean dirty;
            public Boolean empty;

            public Frame2(Int16[] value, int tag, Boolean dirty, Boolean empty)
            {
                this.value = new Int16[2];
                this.tag = tag;
                this.dirty = false;
                this.empty = true;
            }
        }

        public struct Set
        {
            public Frame frame1;
            public Frame frame2;

            public Set(Frame frame1, Frame frame2)
            {
                this.frame1 = frame1;
                this.frame2 = frame2;
            }
        }

        public struct Set2
        {
            public Frame2 frame1;
            public Frame2 frame2;

            public Set2(Frame2 frame1, Frame2 frame2)
            {
                this.frame1 = frame1;
                this.frame2 = frame2;
            }
        }
        

        Dictionary<string, int> labelLocationMap = new Dictionary<string, int>();
        //Dictionary<string,Int16> memoryBook = new Dictionary<string,Int16>();
        Int16[] memoryBook = new Int16[256];

        Boolean DirectCache = true;  // Change to false when dealing with 2-way set associative cache
        Boolean BlockHolds1 = true;  //Change to false when dealing with 2 mem addresses in 1 block

        public static int cacheSize = 4;
        public static int numOfSets = cacheSize / 2;
        public Frame[] cache = new Frame[cacheSize];
        public Frame2[] cache2B = new Frame2[cacheSize]; // direct, 2 mem addresses per block

        public Set[] cache2 = new Set[numOfSets];
        public Set2[] cache22B = new Set2[numOfSets]; // 2 way, 2 mem addresses per block

        //================Stuff for keeping track of hits and misses==============
        #region Stuff
        public int MISSCOUNT = 0; //How many times we miss
        public int HITCOUNT = 0; //How many times we have hits
        public int SPACIALCOUNT = 0; //How many times a memory block was loaded into cache from spacial locality
        public String HitorMiss = ""; // For displaying what the current instruction results in

        public void printCounts()
        {
            Console.WriteLine("Total Miss Count: " + MISSCOUNT);
            Console.WriteLine("Total Hit Count: " + HITCOUNT);
            //Console.WriteLine("TOTAL Spacial Locality loads: " + "NOT MADE YET LOLL");
        }
        #endregion

        public void InitializeCache()
        {
            if (DirectCache && BlockHolds1)
            {
                for (int i = 0; i < cacheSize; i++)
                {
                    cache[i].empty = true;
                }
            }
            else if (!DirectCache && BlockHolds1)
            {
                for (int j = 0; j < numOfSets; j++)
                {
                    cache2[j].frame1.empty = true;
                    cache2[j].frame2.empty = true;
                }
            }
            else if (DirectCache && !BlockHolds1)
            {
                for (int k = 0; k < cacheSize; k++)
                {
                    cache2B[k].empty = true;
                    cache2B[k].value = new Int16[2];
                }
            }
            else if (!DirectCache && !BlockHolds1)
            {
                for (int l = 0; l < numOfSets; l++)
                {
                    cache22B[l].frame1.empty = true;
                    cache22B[l].frame2.empty = true;
                    cache22B[l].frame1.value = new Int16[2];
                    cache22B[l].frame2.value = new Int16[2];
                }
            }
        }

        //read
        public Int16 getValue(int register)
        {
            Int16 value = 0;
            if (BlockHolds1)
            {
                if (DirectCache) // We are dealing with Direct Mapped Cache
                {
                    int frameNum = register % cacheSize;
                    if (cache[frameNum].empty == false && cache[frameNum].tag == register)
                    {

                        value = readHit(frameNum, register);
                    }
                    else
                    {
                        value = readMiss(register, frameNum);
                    }
                }
                else  // We are dealing with 2-way associative cache
                {
                    int setNum = register % numOfSets;
                    if ((cache2[setNum].frame1.empty == false || cache2[setNum].frame2.empty == false) && (cache2[setNum].frame1.tag == register || cache2[setNum].frame2.tag == register))
                    {
                        value = readHit(setNum, register);
                    }
                    else
                    {
                        value = readMiss(register, setNum);
                    }
                }
            }
            //block holds 2 mem addresses
            else
            {
                if (DirectCache) // We are dealing with Direct Mapped Cache
                {
                    int tag = register / 2; // divide by 2 bc blockSize = 2
                    int frameNum = tag % cacheSize;
                    if (cache2B[frameNum].empty == false && cache2B[frameNum].tag == tag)
                    {

                        value = readHit(frameNum, register);
                    }
                    else
                    {
                        value = readMiss(register, frameNum);
                    }
                }
                else  // We are dealing with 2-way associative cache
                {
                    int tag = register / 2;
                    int setNum = tag % numOfSets;
                    //Console.WriteLine("tag " + tag);
                    //Console.WriteLine("setNum " + setNum);
                    if ((cache22B[setNum].frame1.empty == false && (cache22B[setNum].frame1.tag * 2 == register || cache22B[setNum].frame1.tag * 2+1 == register)) ||
                        (cache22B[setNum].frame2.empty == false && (cache22B[setNum].frame2.tag*2 == register || cache22B[setNum].frame2.tag*2+1 == register)))
                        
                    {
                        //Console.WriteLine("in read hit");
                        value = readHit(setNum, register);
                    }
                    else
                    {
                        value = readMiss(register, setNum);
                    }
                }
            }

            return value;
        }

        //write
        public void setValue(int register, Int16 value)
        {
            if (BlockHolds1)
            {
                if (DirectCache) // Dealing with Direct Map
                {
                    int frameNum = register % cacheSize;
                    if (cache[frameNum].empty)
                    {
                        writeMiss(register, frameNum, value);
                    }
                    else
                    {
                        writeHit(register, frameNum, value);
                    }
                }
                else  // We are dealing with 2-way associative cache
                {
                    int setNum = register % numOfSets;
                    if (cache2[setNum].frame1.empty && cache2[setNum].frame2.empty)
                    {
                        writeMiss(register, setNum, value);
                    }
                    else
                    {
                        writeHit(register, setNum, value);
                    }
                }
            }
            else
            {
                if (DirectCache) // Dealing with Direct Map
                {
                    int tag = register / 2;
                    int frameNum = tag % cacheSize;
                    if (cache2B[frameNum].empty)
                    {
                        writeMiss(register, frameNum, value);
                    }
                    else
                    {
                        writeHit(register, frameNum, value);
                    }
                }
                else  // We are dealing with 2-way associative cache
                {
                    int tag = register / 2;
                    int setNum = tag % numOfSets;
                    if (cache22B[setNum].frame1.empty && cache22B[setNum].frame2.empty)
                    {
                        writeMiss(register, setNum, value);
                    }
                    else
                    {
                        writeHit(register, setNum, value);
                    }
                }
            }
        }

        public void writeMiss(int register, int frameNum, Int16 value)
        {
            MISSCOUNT++; // Counting misses
            HitorMiss = "Miss!";
            if (BlockHolds1)
            {
                if (DirectCache)
                {
                    memoryBook[register] = value;
                }
                else  // We are dealing with 2-way associative cache
                {
                    memoryBook[register] = value;
                }
            }
            else
            {
                if (DirectCache)
                {
                    memoryBook[register] = value;
                }
                else  // We are dealing with 2-way associative cache
                {
                    memoryBook[register] = value;
                }
            }
        }

        public void writeHit(int register, int frameNum, Int16 value) {
            HITCOUNT++; // Counting hits
            HitorMiss = " Hit!";

            //Frame frame = cache[FrameNum];
            int setNum = frameNum;
            if (BlockHolds1)
            {
                if (DirectCache)
                {
                    if (cache[frameNum].tag == register)
                    {
                        cache[frameNum].value = value;
                        cache[frameNum].dirty = true;
                    }
                    else if ((cache[frameNum].tag != register) && !cache[frameNum].dirty)
                    {
                        cache[frameNum].value = value;
                        cache[frameNum].tag = register;
                        cache[frameNum].dirty = true;
                    }
                    else if ((cache[frameNum].tag != register) && cache[frameNum].dirty)
                    {
                        memoryBook[cache[frameNum].tag] = cache[frameNum].value;
                        cache[frameNum].value = value;
                        cache[frameNum].tag = register;
                        cache[frameNum].dirty = true;
                    }
                }
                else  // We are dealing with 2-way associative cache
                {
                    int randNum = 0;
                    Random random = new Random();
                    randNum = random.Next(1, 3);
                    if (cache2[setNum].frame1.tag == register)
                    {
                        cache2[setNum].frame1.value = value;
                        cache2[setNum].frame1.dirty = true;
                    }
                    else if (cache2[setNum].frame2.tag == register)
                    {
                        cache2[setNum].frame2.value = value;
                        cache2[setNum].frame2.dirty = true;
                    }
                    else if (cache2[setNum].frame1.tag != register && cache2[setNum].frame2.tag != register)
                    {
                        if (randNum == 1)
                        {
                            //clean
                            if (!cache2[setNum].frame1.dirty)
                            {
                                cache2[setNum].frame1.value = value;
                                cache2[setNum].frame1.tag = register;
                                cache2[setNum].frame1.dirty = true;
                            }
                            //dirty
                            else if (cache2[setNum].frame1.dirty)
                            {
                                memoryBook[cache2[setNum].frame1.tag] = cache2[setNum].frame1.value;
                                cache2[setNum].frame1.value = value;
                                cache2[setNum].frame1.tag = register;
                                cache2[setNum].frame1.dirty = true;
                            }
                        }
                        else if (randNum == 2)
                        {
                            //clean
                            if (!cache2[setNum].frame2.dirty)
                            {
                                cache2[setNum].frame2.value = value;
                                cache2[setNum].frame2.tag = register;
                                cache2[setNum].frame2.dirty = true;
                            }
                            //dirty
                            else if (cache2[setNum].frame2.dirty)
                            {
                                memoryBook[cache2[setNum].frame2.tag] = cache2[setNum].frame2.value;
                                cache2[setNum].frame2.value = value;
                                cache2[setNum].frame2.tag = register;
                                cache2[setNum].frame2.dirty = true;
                            }
                        }
                    }
                }
            }
                //cache block has 2 mem addresses
            else
            {
                int index = register % 2;
                if (DirectCache)
                {
                    if (cache2B[frameNum].tag == register)
                    {

                        cache2B[frameNum].value[index] = value;
                        cache2B[frameNum].dirty = true;
                    }
                    else if ((cache2B[frameNum].tag != register) && !cache2B[frameNum].dirty)
                    {
                        cache2B[frameNum].tag = register / 2;
                        int memReg = cache2B[frameNum].tag * 2;
                        cache2B[frameNum].value[0] = memoryBook[memReg];
                        cache2B[frameNum].value[1] = memoryBook[memReg+1];
                        cache2B[frameNum].value[index] = value;
                        cache2B[frameNum].dirty = true;
                    }
                    else if ((cache2B[frameNum].tag != register) && cache2B[frameNum].dirty)
                    {
                        int memReg = cache2B[frameNum].tag * 2;
                        memoryBook[memReg] = cache2B[frameNum].value[0];
                        memoryBook[memReg+1] = cache2B[frameNum].value[1];
                        cache2B[frameNum].tag = register / 2;
                        memReg = cache2B[frameNum].tag * 2;
                        cache2B[frameNum].value[0] = memoryBook[memReg];
                        cache2B[frameNum].value[1] = memoryBook[memReg + 1];
                        cache2B[frameNum].value[index] = value;
                        cache2B[frameNum].dirty = true;
                    }
                }
                else  // We are dealing with 2-way associative cache
                {
                    int randNum = 0;
                    Random random = new Random();
                    randNum = random.Next(1, 3);
                    //Console.WriteLine("tag in writehit " + cache22B[setNum].frame1.tag);
                    if ((cache22B[setNum].frame1.tag * 2 == register || cache22B[setNum].frame1.tag * 2+1 == register) && !cache22B[setNum].frame1.empty)
                    {
                        cache22B[setNum].frame1.value[index] = value;
                        cache22B[setNum].frame1.dirty = true;
                    }
                    else if ((cache22B[setNum].frame2.tag * 2 == register || cache22B[setNum].frame2.tag * 2 + 1 == register) && !cache22B[setNum].frame2.empty)
                    {
                        cache22B[setNum].frame2.value[index] = value;
                        cache22B[setNum].frame2.dirty = true;
                    }
                    else if ((cache22B[setNum].frame1.tag * 2 == register || cache22B[setNum].frame1.tag * 2 + 1 == register) && (cache22B[setNum].frame2.tag * 2 == register || cache22B[setNum].frame2.tag * 2 + 1 == register))
                    {
                        if (randNum == 1)
                        {
                            //clean
                            if (!cache22B[setNum].frame1.dirty)
                            {
                                cache22B[setNum].frame1.tag = register / 2;
                                int memReg = cache22B[setNum].frame1.tag * 2;
                                cache22B[setNum].frame1.value[0] = memoryBook[memReg];
                                cache22B[setNum].frame1.value[1] = memoryBook[memReg+1];
                                cache22B[setNum].frame1.value[index] = value;
                                cache22B[setNum].frame1.dirty = true;
                            }
                            //dirty
                            else if (cache22B[setNum].frame1.dirty)
                            {
                                int memReg = cache22B[setNum].frame1.tag * 2;
                                memoryBook[memReg] = cache22B[setNum].frame1.value[0];
                                memoryBook[memReg+1] = cache22B[setNum].frame1.value[1];
                                cache22B[setNum].frame1.tag = register / 2;
                                memReg = cache22B[setNum].frame1.tag * 2;
                                cache22B[setNum].frame1.value[0] = memoryBook[memReg];
                                cache22B[setNum].frame1.value[1] = memoryBook[memReg+1];
                                cache22B[setNum].frame1.value[index] = value;
                                cache22B[setNum].frame1.dirty = true;
                            }
                        }
                        else if (randNum == 2)
                        {
                            //clean
                            if (!cache22B[setNum].frame2.dirty)
                            {
                                cache22B[setNum].frame2.tag = register / 2;
                                int memReg = cache22B[setNum].frame2.tag * 2;
                                cache22B[setNum].frame2.value[0] = memoryBook[memReg];
                                cache22B[setNum].frame2.value[1] = memoryBook[memReg + 1];
                                cache22B[setNum].frame2.value[index] = value;
                                cache22B[setNum].frame2.dirty = true;
                            }
                            //dirty
                            else if (cache22B[setNum].frame2.dirty)
                            {
                                int memReg = cache22B[setNum].frame2.tag * 2;
                                memoryBook[memReg] = cache22B[setNum].frame2.value[0];
                                memoryBook[memReg + 1] = cache22B[setNum].frame2.value[1];
                                cache22B[setNum].frame2.tag = register / 2;
                                memReg = cache22B[setNum].frame2.tag * 2;
                                cache22B[setNum].frame2.value[0] = memoryBook[memReg];
                                cache22B[setNum].frame2.value[1] = memoryBook[memReg + 1];
                                cache22B[setNum].frame2.value[index] = value;
                                cache22B[setNum].frame2.dirty = true;
                            }
                        }
                    }
                }
            }
        }

        public Int16 readHit(int frameNum, int register)
        {
            HITCOUNT++; // Counting hits
            HitorMiss = " Hit!";

            Int16 value = 0;
            int setNum = frameNum;
            if (BlockHolds1)
            {
                if (DirectCache)
                {
                    value = cache[frameNum].value;
                }
                else  // We are dealing with 2-way associative cache
                {
                    if (cache2[setNum].frame1.tag == register)
                    {
                        value = cache2[setNum].frame1.value;
                    }
                    else
                    {
                        value = cache2[setNum].frame2.value;
                    }
                }
            }
                //2 address per block
            else
            {
                int index = register % 2;
                if (DirectCache)
                {
                    value = cache2B[frameNum].value[index];
                }
                else  // We are dealing with 2-way associative cache
                {
                    if (!cache22B[setNum].frame1.empty && (cache22B[setNum].frame1.tag*2 == register||cache22B[setNum].frame1.tag*2+1 == register))
                    {
                        value = cache22B[setNum].frame1.value[index];
                    }
                    else if (!cache22B[setNum].frame2.empty && (cache22B[setNum].frame2.tag*2 == register||cache22B[setNum].frame2.tag*2+1 == register))
                    {
                        value = cache22B[setNum].frame1.value[index];
                    }
                }
            }
            return value;
        }

        public Int16 readMiss(int register, int frameNum) {
            MISSCOUNT++; // Counting misses
            HitorMiss = "Miss!";

            Int16 value = 0;
            int setNum = frameNum;
            if (BlockHolds1)
            {
                if (DirectCache)
                {
                    if (cache[frameNum].empty == true)
                    {
                        cache[frameNum].value = memoryBook[register];
                        cache[frameNum].tag = register;
                        cache[frameNum].empty = false;
                    }
                    //clean bit
                    else if (!cache[frameNum].dirty)
                    {
                        cache[frameNum].value = memoryBook[register];
                        cache[frameNum].tag = register;
                    }
                    //dirty bit
                    else if (cache[frameNum].dirty)
                    {
                        memoryBook[cache[frameNum].tag] = cache[frameNum].value;
                        cache[frameNum].value = memoryBook[register];
                        cache[frameNum].tag = register;
                        cache[frameNum].dirty = false;

                    }
                    value = cache[frameNum].value;
                }
                else  // We are dealing with 2-way associative cache
                {
                    Random random = new Random();
                    int randNum = random.Next(1, 3);

                    //empty frames available
                    if (cache2[setNum].frame1.empty && cache2[setNum].frame2.empty)
                    {
                        if (randNum == 1)
                        {
                            cache2[setNum].frame1.value = memoryBook[register];
                            cache2[setNum].frame1.tag = register;
                            cache2[setNum].frame1.empty = false;
                            value = cache2[setNum].frame1.value;
                        }
                        else if (randNum == 2)
                        {
                            cache2[setNum].frame2.value = memoryBook[register];
                            cache2[setNum].frame2.tag = register;
                            cache2[setNum].frame2.empty = false;
                            value = cache2[setNum].frame2.value;
                        }
                    }
                    else if (cache2[setNum].frame1.empty && !cache2[setNum].frame2.empty)
                    {
                        cache2[setNum].frame1.value = memoryBook[register];
                        cache2[setNum].frame1.tag = register;
                        cache2[setNum].frame1.empty = false;
                        value = cache2[setNum].frame1.value;
                    }
                    else if (cache2[setNum].frame2.empty && !cache2[setNum].frame1.empty)
                    {
                        cache2[setNum].frame2.value = memoryBook[register];
                        cache2[setNum].frame2.tag = register;
                        cache2[setNum].frame2.empty = false;
                        value = cache2[setNum].frame2.value;
                    }
                    //no empty frames
                    else if (!cache2[setNum].frame1.empty && !cache2[setNum].frame2.empty)
                    {
                        if (randNum == 1)
                        {
                            //not dirty
                            if (!cache2[setNum].frame1.dirty)
                            {
                                cache2[setNum].frame1.value = memoryBook[register];
                                cache2[setNum].frame1.tag = register;
                            }
                            //dirty
                            else if (cache2[setNum].frame1.dirty)
                            {
                                memoryBook[cache2[setNum].frame1.tag] = cache2[setNum].frame1.value;
                                cache2[setNum].frame1.value = memoryBook[register];
                                cache2[setNum].frame1.tag = register;
                                cache2[setNum].frame1.dirty = false;
                            }
                            value = cache2[setNum].frame1.value;
                        }
                        else if (randNum == 2)
                        {
                            //not dirty
                            if (!cache2[setNum].frame2.dirty)
                            {
                                cache2[setNum].frame2.value = memoryBook[register];
                                cache2[setNum].frame2.tag = register;
                            }
                            //dirty
                            else if (cache2[setNum].frame2.dirty)
                            {
                                memoryBook[cache2[setNum].frame2.tag] = cache2[setNum].frame2.value;
                                cache2[setNum].frame2.value = memoryBook[register];
                                cache2[setNum].frame2.tag = register;
                                cache2[setNum].frame2.dirty = false;
                            }
                            value = cache2[setNum].frame2.value;
                        }
                    }
                }
            }
                //cache block holds 2 mem addresses
            else
            {
                int index = register % 2;
                int tag = register / 2;
                //Console.WriteLine("index in read miss is " + index);
                //Console.WriteLine("tag in read miss is " + tag);
                if (DirectCache)
                {
                    if (cache2B[frameNum].empty == true)
                    {
                        int memReg = tag * 2;
                        cache2B[frameNum].value[0] = memoryBook[memReg];
                        cache2B[frameNum].value[1] = memoryBook[memReg+1];
                        cache2B[frameNum].tag = tag;
                        cache2B[frameNum].empty = false;
                    }
                    //clean bit
                    else if (!cache2B[frameNum].dirty)
                    {
                        int memReg = tag * 2;
                        cache2B[frameNum].value[0] = memoryBook[memReg];
                        cache2B[frameNum].value[1] = memoryBook[memReg+1];
                        cache[frameNum].tag = tag;
                        cache2B[frameNum].value[index] = memoryBook[register];

                    }
                    //dirty bit
                    else if (cache2B[frameNum].dirty)
                    {
                        int memReg = cache2B[frameNum].tag * 2; //index value 0
                        memoryBook[memReg] = cache2B[frameNum].value[0];
                        memoryBook[memReg+1] = cache2B[frameNum].value[1];

                        cache2B[frameNum].tag = tag;
                        memReg = tag * 2;
                        cache2B[frameNum].value[0] = memoryBook[memReg];
                        cache2B[frameNum].value[1] = memoryBook[memReg + 1];
                        cache2B[frameNum].value[index] = memoryBook[register];
                        cache2B[frameNum].dirty = false;
                    }
                    value = cache2B[frameNum].value[index];
                }
                else  // We are dealing with 2-way associative cache
                {
                    Random random = new Random();
                    int randNum = random.Next(1, 3);

                    //empty frames available
                    if (cache22B[setNum].frame1.empty && cache22B[setNum].frame2.empty)
                    {
                        if (randNum == 1)
                        {
                            int memReg = tag * 2;
                            cache22B[setNum].frame1.value[0] = memoryBook[memReg];
                            cache22B[setNum].frame1.value[1] = memoryBook[memReg+1];
                            cache22B[setNum].frame1.tag = tag;
                            cache22B[setNum].frame1.empty = false;
                            value = cache22B[setNum].frame1.value[index];
                        }
                        else if (randNum == 2)
                        {
                            int memReg = tag * 2;
                            cache22B[setNum].frame2.value[0] = memoryBook[memReg];
                            cache22B[setNum].frame2.value[1] = memoryBook[memReg+1];
                            cache22B[setNum].frame2.tag = tag;
                            cache22B[setNum].frame2.empty = false;
                            value = cache22B[setNum].frame2.value[index];
                        }
                    }
                    else if (cache22B[setNum].frame1.empty && !cache22B[setNum].frame2.empty)
                    {
                        int memReg = tag * 2;
                        cache22B[setNum].frame1.value[0] = memoryBook[memReg];
                        cache22B[setNum].frame1.value[1] = memoryBook[memReg+1];
                        cache22B[setNum].frame1.tag = tag;
                        cache22B[setNum].frame1.empty = false;
                        value = cache22B[setNum].frame1.value[index];
                    }
                    else if (cache22B[setNum].frame2.empty && !cache22B[setNum].frame1.empty)
                    {
                        int memReg = tag * 2;
                        cache22B[setNum].frame2.value[0] = memoryBook[memReg];
                        cache22B[setNum].frame2.value[1] = memoryBook[memReg+1];
                        cache22B[setNum].frame2.tag = tag;
                        cache22B[setNum].frame2.empty = false;
                        value = cache22B[setNum].frame2.value[index];
                    }
                    //no empty frames
                    else if (!cache22B[setNum].frame1.empty && !cache22B[setNum].frame2.empty)
                    {
                        if (randNum == 1)
                        {
                            //not dirty
                            if (!cache22B[setNum].frame1.dirty)
                            {
                                int memReg = tag * 2;
                                cache22B[setNum].frame1.value[0] = memoryBook[memReg];
                                cache22B[setNum].frame1.value[1] = memoryBook[memReg+1];
                                cache22B[setNum].frame1.tag = tag;
                                cache22B[setNum].frame1.value[index] = memoryBook[register];
                            }
                            //dirty
                            else if (cache22B[setNum].frame1.dirty)
                            {
                                int memReg = cache22B[setNum].frame1.tag * 2;
                                memoryBook[memReg] = cache22B[setNum].frame1.value[0];
                                memoryBook[memReg+1] = cache22B[setNum].frame1.value[1];

                                cache22B[setNum].frame1.tag = tag;
                                memReg = tag * 2;
                                cache22B[setNum].frame1.value[0] = memoryBook[memReg];
                                cache22B[setNum].frame1.value[1] = memoryBook[memReg+1];
                                cache22B[setNum].frame1.value[index] = memoryBook[register];
                                cache22B[setNum].frame1.dirty = false;
                            }
                            value = cache22B[setNum].frame1.value[index];
                        }
                        else if (randNum == 2)
                        {
                            //not dirty
                            if (!cache22B[setNum].frame2.dirty)
                            {
                                int memReg = tag * 2;
                                cache22B[setNum].frame2.value[0] = memoryBook[memReg];
                                cache22B[setNum].frame2.value[1] = memoryBook[memReg + 1];
                                cache22B[setNum].frame2.tag = tag;
                                cache22B[setNum].frame2.value[index] = memoryBook[register];
                            }
                            //dirty
                            else if (cache22B[setNum].frame2.dirty)
                            {
                                int memReg = cache22B[setNum].frame2.tag * 2;
                                memoryBook[memReg] = cache22B[setNum].frame2.value[0];
                                memoryBook[memReg + 1] = cache22B[setNum].frame2.value[1];

                                cache22B[setNum].frame2.tag = tag;
                                memReg = tag * 2;
                                cache22B[setNum].frame2.value[0] = memoryBook[memReg];
                                cache22B[setNum].frame2.value[1] = memoryBook[memReg + 1];
                                cache22B[setNum].frame2.value[index] = memoryBook[register];
                                cache22B[setNum].frame2.dirty = false;
                            }
                            value = cache22B[setNum].frame2.value[index];
                        }
                    }
                }
            }
            return value;
        }


        public void printCache(Frame[] cache)
        {
            if (BlockHolds1)
            {
                if (DirectCache)
                {
                    Console.WriteLine("Frame Tag Value Dirty Empty");
                    for (int i = 0; i < cacheSize; i++)
                    {
                        Console.WriteLine(i + "      " + cache[i].tag + "     " + cache[i].value + "    " + cache[i].dirty + "   " + cache[i].empty);
                    }
                }
                else // 2-way set
                {
                    Console.WriteLine("Set Tag    Value   Dirty     Empty");

                    for (int i = 0; i < numOfSets; i++)
                    {
                        Console.WriteLine(i + "    " + cache2[i].frame1.tag + "      " + cache2[i].frame1.value + "      " + cache2[i].frame1.dirty + "      " + cache2[i].frame1.empty);
                        Console.WriteLine(i + "    " + cache2[i].frame2.tag + "      " + cache2[i].frame2.value + "      " + cache2[i].frame2.dirty + "      " + cache2[i].frame2.empty);
                    }
                }
            }
            else
            {
                if (DirectCache)
                {
                    Console.WriteLine("Frame Tag Value1 Value2 Dirty Empty");
                    for (int i = 0; i < cacheSize; i++)
                    {
                        Console.WriteLine(i + "      " + cache2B[i].tag + "     " + cache2B[i].value[0] + "    " + cache2B[i].value[1] + "    " + cache2B[i].dirty + "   " + cache2B[i].empty);
                    }
                }
                else // 2-way set
                {
                    Console.WriteLine("Set Tag    Value1    Value2   Dirty     Empty");

                    for (int i = 0; i < numOfSets; i++)
                    {
                        Console.WriteLine(i + "    " + cache22B[i].frame1.tag + "      " + cache22B[i].frame1.value[0] + "      " + cache22B[i].frame1.value[1] + "       " + cache22B[i].frame1.dirty + "      " + cache22B[i].frame1.empty);
                        Console.WriteLine(i + "    " + cache22B[i].frame2.tag + "      " + cache22B[i].frame2.value[0] + "      " + cache22B[i].frame2.value[1] + "      " + cache22B[i].frame2.dirty + "      " + cache22B[i].frame2.empty);
                    }
                }
            }
        }

        public void printMemory(Int16[] memorybook)
        {
            Console.WriteLine("Block Value");
            for (int i = 0; i < memorybook.Length; i++)
            {
                Console.WriteLine(i + " " + memorybook[i]);
            }
        }


        public void setLabelLocationMap(Dictionary<string, int> labels)
        {
            labelLocationMap = labels;
        }

        public void setMemoryBook(Int16[] memory)
        {
            memoryBook = memory;
        }

        public Int16[] getMemoryBook()
        {
            return memoryBook;
        }

        public Frame[] getCache()
        {
            return cache;
        }

        public void addMem(string register, Int16 value)
        {
            int memPlace = convertToBase10(register);
            memoryBook[memPlace] = value;
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
    }
}
