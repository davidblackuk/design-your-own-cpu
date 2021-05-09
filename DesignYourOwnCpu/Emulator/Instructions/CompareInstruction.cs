namespace Emulator.Instructions
{
    public class CompareInstruction : EmulatorInstruction
    {

        public CompareInstruction(byte opcode, byte register, byte high, byte low) : base(opcode, register, high, low)
        {
        }

        public void Compare(ushort left, ushort right, IFlags flags)
        {
            flags.Equal = left == right;
            flags.GreaterThan = left > right;
            flags.LessThan = left < right;
        }
    }
}