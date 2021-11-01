using Shared;

namespace Emulator.Instructions
{
    public class DivideRegisterWithRegisterInstruction : CompareInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.DivideRegisterWithRegister;

        public DivideRegisterWithRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Registers[Register] /= cpu.Registers[ByteLow];
        }
    }
}