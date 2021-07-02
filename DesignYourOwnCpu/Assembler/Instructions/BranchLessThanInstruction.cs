using Shared;

namespace Assembler.Instructions
{
    public class BranchLessThanInstruction : SingleValueInstruction
    {
        public const string InstructionName = "blt";

        public BranchLessThanInstruction() : base(InstructionName, OpCodes.BranchLessThan)
        {
        }
    }
}