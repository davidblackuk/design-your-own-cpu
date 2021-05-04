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

        // has the cpu halted and should we stop executing
        public bool Halted { get; set; }

        public CPU(IRandomAccessMemory memory, IRegisters registers, IEmulatorInstructionFactory instructionFactory)
        {
            this.memory = memory ?? throw new ArgumentNullException(nameof(memory));
            this.registers = registers;
            this.instructionFactory = instructionFactory;
        }

        /// <summary>
        /// Executes till a halt instruction is hit
        /// </summary>
        public void Run()
        {
            do
            {
                var bytes = memory.Instruction(registers.ProgramCounter);
                var instruction = instructionFactory.Create(bytes.opcode, bytes.register, bytes.byteHigh, bytes.byteLow);

                // we increment the program counter here, instructions that directly modify the program counter
                // may do so during execution (BRA, CALL etc)
                registers.ProgramCounter = (ushort) (registers.ProgramCounter + instruction.Size);
                
                instruction.Execute(this);

            } while (!Halted);
            
            Console.WriteLine("Halted");
        }
    }
}