using Shared;

namespace Emulator.Instructions
{
    public class AddConstantToRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.AddConstantToRegister;

        public AddConstantToRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            cpu.Registers[Register] += Value;
        }
    }
}