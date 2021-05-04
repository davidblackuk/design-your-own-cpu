using Shared;

namespace Assembler.Instructions
{
    public class SubtractInstruction: ArithmeticInstruction
    {
        public const string InstructionName = "sub";

        public SubtractInstruction(): base(InstructionName, OpCodes.SubtractRegisterFromRegister, OpCodes.SubtractConstantFromRegister)
        {
            
        }
    }
}