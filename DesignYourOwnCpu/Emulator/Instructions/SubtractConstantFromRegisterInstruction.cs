using Shared;

namespace Emulator.Instructions
{
    public class SubtractConstantFromRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.SubtractConstantFromRegister;

        public SubtractConstantFromRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register,
            high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            cpu.Registers[Register] -= Value;
        }
    }
}