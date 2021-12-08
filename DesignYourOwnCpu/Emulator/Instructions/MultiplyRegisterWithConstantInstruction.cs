using Shared;

namespace Emulator.Instructions
{
    public class MultiplyRegisterWithConstantInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.MultiplyRegisterWithConstant;

        public MultiplyRegisterWithConstantInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            cpu.Registers[Register] *= Value;
        }
    }
}