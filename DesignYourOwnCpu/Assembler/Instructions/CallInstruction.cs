using Shared;

namespace Assembler.Instructions
{
    public class CallInstruction : SingleAddressInstruction
    {
        public const string InstructionName = "call";
        public CallInstruction(): base(InstructionName, OpCodes.Call)
        {
            
        }
    }
}