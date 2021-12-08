using Shared;

namespace Emulator.Instructions
{
    public class LoadRegisterWithConstantInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.LoadRegisterWithConstant;

        public LoadRegisterWithConstantInstruction(byte register, byte high, byte low) : base(Opcode, register, high,
            low)
        {
        }

        public void Execute(ICpu cpu)
        {
            cpu.Registers[Register] = Value;
        }
    }
}