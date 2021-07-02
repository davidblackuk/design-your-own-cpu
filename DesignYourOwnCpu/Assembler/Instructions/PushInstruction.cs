using Shared;

namespace Assembler.Instructions
{
    public class PushInstruction : SingleRegisterInstruction
    {
        public const string InstructionName = "push";

        public PushInstruction() : base(InstructionName, OpCodes.Push)
        {
        }
    }
}