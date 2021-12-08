using Shared;

namespace Emulator.Instructions
{
    public class UnknownInstruction: EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Halt;

        public UnknownInstruction() : base(OpCodes.Unknown, 0,0, 0)
        {
        }

        public void Execute(ICpu cpu)
        {
           
        }
    }
}