using Shared;

namespace Assembler.Instructions
{
    public class BranchLessThanInstruction: SingleAddressInstruction
    {
        public const string InstructionName = "blt";

        public BranchLessThanInstruction(): base(InstructionName, OpCodes.BranchLessThan)
        {
            
        }
    }
}