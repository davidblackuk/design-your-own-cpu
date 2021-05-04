namespace Emulator.Instructions
{
    public class NopInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public NopInstruction(byte opcode, byte register, byte high, byte low) : base(opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            // Do nothing, effortlessly
        }
    }
}