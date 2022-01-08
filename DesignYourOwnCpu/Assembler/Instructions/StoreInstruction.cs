using Shared;

namespace Assembler.Instructions;

public class StoreInstruction : StoreInstructionBase
{
    public const string InstructionName = "st";

    public StoreInstruction() : base(InstructionName, OpCodes.StoreRegisterIndirect, OpCodes.StoreRegisterDirect)
    {
    }
}