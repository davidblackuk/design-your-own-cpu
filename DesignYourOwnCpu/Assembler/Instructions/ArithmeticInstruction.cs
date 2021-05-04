using System.Diagnostics.CodeAnalysis;

namespace Assembler.Instructions
{
    public class ArithmeticInstruction: AssemblerInstruction, IAssemblerInstruction
    {
        private readonly byte opcodeRegister;
        private readonly byte opcodeConstant;
        private readonly string instructionName;


        public ArithmeticInstruction(string instruction, byte opcodeRegister, byte opcodeConstant)
        {
            this.opcodeRegister = opcodeRegister;
            this.opcodeConstant = opcodeConstant;
            this.instructionName = instruction;
        }
        
        public void Parse(string source)
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
            {
                return $"{instructionName} r{Register}, 0x{ByteHigh:X2}{ByteLow:X2}";
            }
            else
            {
                return $"{instructionName}  r{Register}, r{ByteLow}";
            }
        }
    }
}