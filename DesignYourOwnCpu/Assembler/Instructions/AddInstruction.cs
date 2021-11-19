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
    
    public class MultiplyInstruction : ArithmeticInstruction
    {
        public const string InstructionName = "mul";

        public MultiplyInstruction() : base(InstructionName, OpCodes.MultiplyRegisterWithRegister, OpCodes.MultiplyRegisterWithConstant)
        {
        }
    }
    
    public class DivideInstruction : ArithmeticInstruction
    {
        public const string InstructionName = "div";

        public DivideInstruction() : base(InstructionName, OpCodes.DivideRegisterByRegister, OpCodes.DivideRegisterByConstant)
        {
        }
    }
}