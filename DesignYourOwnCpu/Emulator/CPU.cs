using System;
using Emulator.Instructions;
using Shared;

namespace Emulator
{
    public class CPU : ICPU
    {
        private readonly IEmulatorInstructionFactory instructionFactory;


        public CPU(IRandomAccessMemory memory, IRegisters registers, IFlags flags,
            IEmulatorInstructionFactory instructionFactory)
        {
            this.Memory = memory ?? throw new ArgumentNullException(nameof(memory));
            this.Registers = registers;
            this.instructionFactory = instructionFactory;
            Flags = flags;
        }

        public IRegisters Registers { get; }

        public IRandomAccessMemory Memory { get; }

        public IFlags Flags { get; set; }

        /// <summary>
        ///     Executes till a halt instruction is hit
        /// </summary>
        public void Run()
        {
            do
            {
                var bytes = Memory.Instruction(Registers.ProgramCounter);
                var instruction =
                    instructionFactory.Create(bytes.opcode, bytes.register, bytes.byteHigh, bytes.byteLow);

                // we increment the program counter here, instructions that directly modify the program counter
                // may do so during execution (BRA, CALL etc)
                Registers.ProgramCounter = (ushort) (Registers.ProgramCounter + instruction.Size);

                instruction.Execute(this);
            } while (!Flags.Halted);

            Console.WriteLine("Halted");
        }
    }
}