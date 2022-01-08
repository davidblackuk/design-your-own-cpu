using Shared;

namespace Assembler.Instructions;

public class BranchEqualInstruction : SingleValueInstruction
{
    public const string InstructionName = "beq";

    public BranchEqualInstruction() : base(InstructionName, OpCodes.BranchEqual)
    {
    }
}