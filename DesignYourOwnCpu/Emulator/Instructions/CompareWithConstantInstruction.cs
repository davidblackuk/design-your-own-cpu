using Shared;

namespace Emulator.Instructions
{
    public class CompareWithConstantInstruction : CompareInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.CompareWithConstant;

        public CompareWithConstantInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            var left = cpu.Registers[Register];
            var right = Value;
            Compare(left, right, cpu.Flags);
        }
    }
}