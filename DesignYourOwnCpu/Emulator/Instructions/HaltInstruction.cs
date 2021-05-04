namespace Emulator.Instructions
{
    public class HaltInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public HaltInstruction(byte opcode, byte register, byte high, byte low) : base(opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Halted = true;
        }
    }
}