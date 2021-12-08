using Shared;

namespace Emulator.Instructions
{
    public class NopInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Nop;

        public NopInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            // Do nothing, effortlessly
        }
    }
}