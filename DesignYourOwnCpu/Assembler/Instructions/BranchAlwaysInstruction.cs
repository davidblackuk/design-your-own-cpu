using Shared;

namespace Assembler.Instructions
{
    public class BranchAlwaysInstruction: BranchInstruction
    {
        public const string InstructionName = "bra";

        public BranchAlwaysInstruction(): base(InstructionName, OpCodes.Branch)
        {
            
        }
    }
}