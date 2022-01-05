namespace Emulator.Instructions
{
    public class StackInstruction : EmulatorInstruction
    {
        public StackInstruction(byte opcode, byte register, byte high, byte low) : base(opcode, register, high, low)
        {
        }

        public ushort Pop(ICpu cpu)
        {
            var res = cpu.Memory.GetWord(cpu.Registers.StackPointer);
            cpu.Registers.StackPointer += 4;
            return res;
        }

        public void Push(ushort value, ICpu cpu)
        {
            cpu.Registers.StackPointer -= 4;
            cpu.Memory.SetWord(cpu.Registers.StackPointer, value);
        }
    }
}