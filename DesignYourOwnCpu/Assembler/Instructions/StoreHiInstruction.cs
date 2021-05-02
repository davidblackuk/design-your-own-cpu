using Shared;

namespace Assembler.Instructions
{
    public class StoreHiInstruction : StoreInstructionBase
    {
        public const string InstructionName = "sth";

        public StoreHiInstruction(): base(InstructionName, OpCodes.StoreRegisterHiIndirect, OpCodes.StoreRegisterHiDirect)
        {
        }
    }
}