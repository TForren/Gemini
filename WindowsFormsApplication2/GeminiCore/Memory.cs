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

        Dictionary<string, int> labelLocationMap = new Dictionary<string, int>();
        //Dictionary<string,Int16> memoryBook = new Dictionary<string,Int16>();
        Int16[] memoryBook = new Int16[256];

        public int cacheSize = 8;
        public Frame[] cache = new Frame[8];

        public void InitializeCache()
        {
            for (int i = 0; i < cacheSize; i++)
            {
                cache[i].empty = true;
            }
        }

        //read
        public Int16 getValue(int register)
        {
            Int16 value = 0;
            int frameNum = register % cacheSize;
            if (cache[frameNum].empty == false && cache[frameNum].tag == register)
            {
                value = readHit(frameNum);
            }
            else
            {
                value = readMiss(register,frameNum);
            }
            return value;
        }

        //write
        public void setValue(int register, Int16 value)
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

        public void writeMiss(int register, int frameNum, Int16 value)
        {
            memoryBook[register] = value;
        }

        public void writeHit(int register, int frameNum, Int16 value) {
            //Frame frame = cache[FrameNum];
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

        public Int16 readHit(int frameNum)
        {
            return cache[frameNum].value;
        }

        public Int16 readMiss(int register, int frameNum) {
            Int16 value = 0;
            //nothing in cache
            //Frame frame = cache[frameNum];
            if (cache[frameNum].empty == true)
            {
                //Console.WriteLine("Register: " + register);
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

            return value;
        }


        public void printCache(Frame[] cache)
        {
            Console.WriteLine("Frame Tag Value Dirty Empty");
            for (int i = 0; i < cacheSize; i++)
            {
                Console.WriteLine(i + "      " + cache[i].tag + "    " + cache[i].value + "   " + cache[i].dirty + " " + cache[i].empty);
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
