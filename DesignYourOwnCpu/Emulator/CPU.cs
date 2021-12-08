using System;
using System.Drawing;
using Emulator.Instructions;
using Pastel;
using Shared;

namespace Emulator
{
    public class Cpu : ICpu
    {
        private readonly IEmulatorInstructionFactory instructionFactory;


        public Cpu(IRandomAccessMemory memory, IRegisters registers, IFlags flags,
            IEmulatorInstructionFactory instructionFactory)
        {
            Memory = memory ?? throw new ArgumentNullException(nameof(memory));
            Registers = registers;
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

                if (instruction.OpCode == OpCodes.Unknown)
                {
                    throw new EmulatorException($"Unknown opcode 0x{bytes.opcode:X2} found at address 0x{Registers.ProgramCounter:X4}");
                }
                
                // we increment the program counter here, instructions that directly modify the program counter
                // may do so during execution (BRA, CALL etc)
                Registers.ProgramCounter = (ushort)(Registers.ProgramCounter + instruction.Size);

                instruction.Execute(this);
            } while (!Flags.Halted);

            Console.WriteLine("Halted".Pastel(Color.Gray));
        }
    }
}