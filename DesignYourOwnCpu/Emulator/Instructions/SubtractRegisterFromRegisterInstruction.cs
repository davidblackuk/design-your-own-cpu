using Shared;

namespace Emulator.Instructions
{
    public class SubtractRegisterFromRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.SubtractRegisterFromRegister;

        public SubtractRegisterFromRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register,
            high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            cpu.Registers[Register] -= cpu.Registers[ByteLow];
        }
    }
}