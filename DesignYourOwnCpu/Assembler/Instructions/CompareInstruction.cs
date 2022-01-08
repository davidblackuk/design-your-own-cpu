using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions;

public class CompareInstruction : AssemblerInstruction
{
    public const string InstructionName = "cmp";

    public override void Parse(string source)
    {
        var operands = GetOperands(InstructionName, source);
        Register = ParseRegister(operands.left);
        if (IsRegister(operands.right))
        {
            ByteLow = ParseRegister(operands.right);
            OpCode = OpCodes.CompareWithRegister;
        }
        else // its load a constant
        {
            ParseValue(operands.right);
            OpCode = OpCodes.CompareWithConstant;
        }
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        switch (OpCode)
        {
            case OpCodes.CompareWithConstant:
                return $"{InstructionName} r{Register}, 0x{ByteHigh:X2}{ByteLow:X2}";
            case OpCodes.CompareWithRegister:
                return $"{InstructionName}  r{Register}, r{ByteLow}";
        }

        return "ERROR!!";
    }
}