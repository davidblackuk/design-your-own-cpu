using Shared;

namespace Emulator.Instructions
{
    public class MultiplyRegisterWithRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.MultiplyRegisterWithRegister;

        public MultiplyRegisterWithRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            cpu.Registers[Register] *= cpu.Registers[ByteLow];
        }
    }
}