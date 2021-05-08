using Shared;

namespace Emulator.Instructions
{
    public class LoadRegisterFromRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.LoadRegisterFromRegister;
        public LoadRegisterFromRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Registers[Register] = cpu.Registers[ByteLow];
        }
    }
}