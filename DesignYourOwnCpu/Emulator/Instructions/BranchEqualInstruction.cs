using Shared;

namespace Emulator.Instructions
{
    public class BranchEqualInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.BranchEqual;

        public BranchEqualInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            if (cpu.Flags.Equal) cpu.Registers.ProgramCounter = Value;
        }
    }
}