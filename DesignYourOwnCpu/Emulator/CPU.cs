using System;
using System.ComponentModel.DataAnnotations;
using Shared;

namespace Emulator
{

    public class CPU
    {
        private readonly RandomAccessMemory memory;
        private readonly Registers registers;
        


        public CPU(RandomAccessMemory memory, Registers registers)
        {
            this.memory = memory ?? throw new ArgumentNullException(nameof(memory));
            this.registers = registers;
        }
        
        /// <summary>
        /// Executes till a halt instruction is hit
        /// </summary>
        public void Run()
        {
            
        }
    }
}