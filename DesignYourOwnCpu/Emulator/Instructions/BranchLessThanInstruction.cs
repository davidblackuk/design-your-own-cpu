using Shared;

namespace Emulator.Instructions
{
    public class BranchLessThanInstruction: EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.BranchLessThan;
        
        public BranchLessThanInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            if (cpu.Flags.LessThan)
            {
                cpu.Registers.ProgramCounter = Value;
            }
        }
    }
}