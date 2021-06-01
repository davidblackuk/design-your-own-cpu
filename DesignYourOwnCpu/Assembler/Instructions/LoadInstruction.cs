using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions
{
    public class LoadInstruction : AssemblerInstruction, IAssemblerInstruction
    {
        public const string InstructionName = "ld";


        public override void Parse(string source)
        {
            var operands = GetOperands(InstructionName, source);
            Register = ParseRegister(operands.left);
            if (IsRegister(operands.right))
            {
                ByteLow = ParseRegister(operands.right);
                OpCode = OpCodes.LoadRegisterFromRegister;
            }
            else if (IsAddress(operands.right))
            {
                ParseAddress(operands.right);
                OpCode = OpCodes.LoadRegisterFromMemory;
            }
            else // its load a constant
            {
                ParseValue(operands.right);
                OpCode = OpCodes.LoadRegisterWithConstant;
            }
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            switch (OpCode)
            {
                case OpCodes.LoadRegisterWithConstant:
                    return $"{InstructionName}  r{Register}, 0x{ByteHigh:X2}{ByteLow:X2}";
                case OpCodes.LoadRegisterFromRegister:
                    return $"{InstructionName}  r{Register}, r{ByteLow}";
                case OpCodes.LoadRegisterFromMemory:
                    return $"{InstructionName}  r{Register}, (0x{ByteHigh:X2}{ByteLow:X2})";
                    
            }

            return "ERROR!!";
        }
    }
}