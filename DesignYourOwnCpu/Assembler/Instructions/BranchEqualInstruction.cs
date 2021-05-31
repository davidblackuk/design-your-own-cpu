using System.Threading.Tasks.Sources;
using Shared;

namespace Assembler.Instructions
{
    public class BranchEqualInstruction: SingleAddressInstruction
    {
        public const string InstructionName = "beq";

        public BranchEqualInstruction(): base(InstructionName, OpCodes.BranchEqual)
        {
            
        }
    }
}