using Shared;

namespace Emulator.Instructions
{
    public class PopInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Pop;

        public PopInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            cpu.Registers[Register] = cpu.Memory.GetWord(cpu.Registers.StackPointer);
            cpu.Registers.StackPointer += 4;
        }
    }
}