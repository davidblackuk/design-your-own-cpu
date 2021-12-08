using Shared;

namespace Emulator.Instructions
{
    public class AddRegisterToRegisterInstruction : CompareInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.AddRegisterToRegister;

        public AddRegisterToRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            cpu.Registers[Register] += cpu.Registers[ByteLow];
        }
    }
}