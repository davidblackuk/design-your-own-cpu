using Shared;

namespace Assembler.Instructions
{
    public class AddInstruction : ArithmeticInstruction
    {
        public const string InstructionName = "add";

        public AddInstruction() : base(InstructionName, OpCodes.AddRegisterToRegister, OpCodes.AddConstantToRegister)
        {
        }
    }
}