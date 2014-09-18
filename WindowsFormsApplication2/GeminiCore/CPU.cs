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
    public class CPU
    {
        public int ACC { get; private set; }
        public int A;
        public int B;
        public int Zero;
        public int One;
        public int PC;
        public int MAR;
        public int MDR;
        public int TEMP;
        public int IR;
        public int CC;

        public CPU()
        {
            ACC = 0;
        }

        public void nextInstruction()
        {
            ACC++;
        }
    }
}
