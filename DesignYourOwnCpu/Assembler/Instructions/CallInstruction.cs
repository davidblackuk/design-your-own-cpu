using Shared;

namespace Assembler.Instructions;

public class CallInstruction : SingleValueInstruction
{
    public const string InstructionName = "call";

    public CallInstruction() : base(InstructionName, OpCodes.Call)
    {
    }
}