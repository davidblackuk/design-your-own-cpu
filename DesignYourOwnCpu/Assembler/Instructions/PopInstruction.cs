using Shared;

namespace Assembler.Instructions;

public class PopInstruction : SingleRegisterInstruction
{
    public const string InstructionName = "pop";

    public PopInstruction() : base(InstructionName, OpCodes.Pop)
    {
    }
}