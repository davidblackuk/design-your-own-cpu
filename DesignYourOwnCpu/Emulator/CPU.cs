using System;
using System.ComponentModel.DataAnnotations;
using Emulator.Instructions;
using Shared;

namespace Emulator
{
    public class CPU : ICPU
    {
        private readonly IRandomAccessMemory memory;
        private readonly IRegisters registers;
        private readonly IEmulatorInstructionFactory instructionFactory;
        
        public IRegisters Registers => registers;

        public IRandomAccessMemory Memory => memory;
        
        public IFlags Flags { get; set; }


        public CPU(IRandomAccessMemory memory, IRegisters registers, IFlags flags, IEmulatorInstructionFactory instructionFactory)
        {
            this.memory = memory ?? throw new ArgumentNullException(nameof(memory));
            this.registers = registers;
            this.instructionFactory = instructionFactory;
            this.Flags = flags;
        }

        /// <summary>
        /// Executes till a halt instruction is hit
        /// </summary>
        public void 
            
            Run()
        {
            do
            {
                var bytes = memory.Instruction(registers.ProgramCounter);
                var instruction = instructionFactory.Create(bytes.opcode, bytes.register, bytes.byteHigh, bytes.byteLow);

                // we increment the program counter here, instructions that directly modify the program counter
                // may do so during execution (BRA, CALL etc)
                registers.ProgramCounter = (ushort) (registers.ProgramCounter + instruction.Size);
                
                instruction.Execute(this);

            } while (!Flags.Halted);
            
            Console.WriteLine("Halted");
        }
    }
}