/**
 * Seth Morecraft
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
