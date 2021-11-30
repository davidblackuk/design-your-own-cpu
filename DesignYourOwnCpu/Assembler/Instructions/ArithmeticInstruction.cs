using System.Diagnostics.CodeAnalysis;

namespace Assembler.Instructions
{
    public class ArithmeticInstruction : AssemblerInstruction
    {
        private readonly string instructionName;
        private readonly byte opcodeConstant;
        private readonly byte opcodeRegister;


        public ArithmeticInstruction(string instruction, byte opcodeRegister, byte opcodeConstant)
        {
            this.opcodeRegister = opcodeRegister;
            this.opcodeConstant = opcodeConstant;
            instructionName = instruction;
        }

        public override void Parse(string source)
        {
            var operands = GetOperands(instructionName, source);
            Register = ParseRegister(operands.left);
            if (IsRegister(operands.right))
            {
                ByteLow = ParseRegister(operands.right);
                OpCode = opcodeRegister;
            }
            else // its load a constant
            {
                ParseValue(operands.right);
                OpCode = opcodeConstant;
            }
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            if (OpCode == opcodeConstant)
                return $"{instructionName} r{Register}, 0x{ByteHigh:X2}{ByteLow:X2}";
            return $"{instructionName}  r{Register}, r{ByteLow}";
        }
    }
}