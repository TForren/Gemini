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

        Dictionary<string, int> labelLocationMap = new Dictionary<string, int>();
        //Dictionary<string,Int16> memoryBook = new Dictionary<string,Int16>();
        Int16[] memoryBook = new Int16[256];

        Boolean DirectCache = true;  // Change to false when dealing with 2-way set associative cache

        public static int cacheSize = 8;
        public static int numOfSets = cacheSize / 2;
        public Frame[] cache = new Frame[cacheSize];

        public Set[] cache2 = new Set[numOfSets];

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
            if (DirectCache)
            {
                for (int i = 0; i < cacheSize; i++)
                {
                    cache[i].empty = true;
                }
            }
            else
            {
                for (int j = 0; j < numOfSets; j++)
                {
                    cache2[j].frame1.empty = true;
                    cache2[j].frame2.empty = true;
                }
            }
        }

        //read
        public Int16 getValue(int register)
        {
            Int16 value = 0;
            if (DirectCache) // We are dealing with Direct Mapped Cache
            {
                int frameNum = register % cacheSize;
                if (cache[frameNum].empty == false && cache[frameNum].tag == register)
                {

                    value = readHit(frameNum,register);
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

            return value;
        }

        //write
        public void setValue(int register, Int16 value)
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
                else {
                    writeHit(register, setNum, value);
                }
            } 
        }

        public void writeMiss(int register, int frameNum, Int16 value)
        {
            MISSCOUNT++; // Counting misses
            HitorMiss = "Miss!";

            if (DirectCache)
            {
                memoryBook[register] = value;
            }
            else  // We are dealing with 2-way associative cache
            {
                memoryBook[register] = value;
            } 
        }

        public void writeHit(int register, int frameNum, Int16 value) {
            HITCOUNT++; // Counting hits
            HitorMiss = " Hit!";

            //Frame frame = cache[FrameNum];
            int setNum = frameNum;
            if (DirectCache) {
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

        public Int16 readHit(int frameNum, int register)
        {
            HITCOUNT++; // Counting hits
            HitorMiss = " Hit!";

            Int16 value = 0;
            int setNum = frameNum;
            if (DirectCache) {
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
            return value;
        }

        public Int16 readMiss(int register, int frameNum) {
            MISSCOUNT++; // Counting misses
            HitorMiss = "Miss!";

            Int16 value = 0;
            int setNum = frameNum;
            //nothing in cache
            //Frame frame = cache[frameNum];
            if (DirectCache) {
                if (cache[frameNum].empty == true)
                {
                    //Console.WriteLine("Register: " + register);
                    //Console.WriteLine("Frame num: " + frameNum);
                    //Console.WriteLine("Value from memB0OOK: " + memoryBook[register]);
                    cache[frameNum].value = memoryBook[register];
                    //Console.WriteLine("Frame value: " + cache[frameNum].value);
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
                int randNum = random.Next(1,3);

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
            return value;
        }


        public void printCache(Frame[] cache)
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
