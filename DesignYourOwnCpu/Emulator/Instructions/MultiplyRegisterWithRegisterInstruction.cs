using Shared;

namespace Emulator.Instructions
{
    public class MultiplyRegisterWithRegisterInstruction : CompareInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.MultiplyRegisterWithRegister;

        public MultiplyRegisterWithRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Registers[Register] *= cpu.Registers[ByteLow];
        }
    }
}