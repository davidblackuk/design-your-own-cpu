using Shared;

namespace Assembler.Instructions
{
    public class BranchGreaterThanInstruction: BranchInstruction
    {
        public const string InstructionName = "bgt";

        public BranchGreaterThanInstruction(): base(InstructionName, OpCodes.BranchGreaterThan)
        {
            
        }
    }
}