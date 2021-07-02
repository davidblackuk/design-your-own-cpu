using Shared;

namespace Emulator.Instructions
{
    public class PushInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Push;

        public PushInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            var registerValue = cpu.Registers[Register];
            cpu.Registers.StackPointer -= 4;
            cpu.Memory.SetWord(cpu.Registers.StackPointer, registerValue);
        }
    }
}