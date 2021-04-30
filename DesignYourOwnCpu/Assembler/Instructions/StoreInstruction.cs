using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions
{
    public class StoreInstruction: Instruction, IInstruction
    {
        public const string InstructionName = "st";


        public void Parse(string source)
        {
            var operands = GetOperands(InstructionName, source);
            Register = ParseRegister(operands.left);
            if (IsIndirectAddress(operands.right))
            {
                ByteLow = ParseIndirectAddress(operands.right);
                
                OpCode = OpCodes.StoreRegisterIndirect;
            }
            else // its load a constant
            {
                ParseAddress(operands.right);
                OpCode = OpCodes.StoreRegisterDirect;
            }
        }
        
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            switch (OpCode)
            {
                case OpCodes.StoreRegisterDirect:
                    return $"{OpCode:X2} {Register:X2} {ByteHigh:X2} {ByteLow:X2}    # {InstructionName} r{Register}, (0x{ByteHigh:X2}{ByteLow:X2})";
                case OpCodes.StoreRegisterIndirect:
                    return $"{OpCode:X2} {Register:X2} {ByteHigh:X2} {ByteLow:X2}    # {InstructionName}  r{Register}, (r{ByteLow})";
                    
            }

            return "ERROR!!";
        }
    }
}