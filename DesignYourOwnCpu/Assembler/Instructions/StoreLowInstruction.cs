using Shared;

namespace Assembler.Instructions
{
    public class StoreLowInstruction : StoreInstructionBase
    {
        public const string InstructionName = "stl";

        public StoreLowInstruction(): base(InstructionName, OpCodes.StoreRegisterLowIndirect, OpCodes.StoreRegisterLowDirect)
        {
        }
    }
}