using Shared;

namespace Assembler.Instructions
{
    public class BrachGreaterThanInstruction: SingleAddressInstruction
    {
        public const string InstructionName = "bgt";

        public BrachGreaterThanInstruction(): base(InstructionName, OpCodes.BranchGreaterThan)
        {
            
        }
    }
}