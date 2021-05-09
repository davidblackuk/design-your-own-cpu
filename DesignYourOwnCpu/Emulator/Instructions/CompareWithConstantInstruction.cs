using Shared;

namespace Emulator.Instructions
{
    public class CompareWithConstantInstruction : CompareInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.CompareWithConstant;
        
        public CompareWithConstantInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            ushort left = cpu.Registers[Register];
            ushort right = Value;
            Compare(left, right, cpu.Flags);
        }
    }
}