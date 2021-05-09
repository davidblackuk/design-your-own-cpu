using Shared;

namespace Emulator.Instructions
{
    public class SubtractRegisterFromRegisterInstruction : CompareInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.SubtractRegisterFromRegister;

        public SubtractRegisterFromRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register,
            high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Registers[Register] -= cpu.Registers[ByteLow];
        }
    }
}