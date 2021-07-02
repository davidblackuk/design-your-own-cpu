using Shared;

namespace Assembler.Instructions
{
    public class BranchAlwaysInstruction : SingleValueInstruction
    {
        public const string InstructionName = "bra";

        public BranchAlwaysInstruction() : base(InstructionName, OpCodes.Branch)
        {
        }
    }
}