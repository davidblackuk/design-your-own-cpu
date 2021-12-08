using Shared;

namespace Emulator.Instructions
{
    public class BranchGreaterThanInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.BranchGreaterThan;

        public BranchGreaterThanInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            if (cpu.Flags.GreaterThan) cpu.Registers.ProgramCounter = Value;
        }
    }
}