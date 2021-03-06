using System.Diagnostics.CodeAnalysis;

namespace Assembler.Instructions;

public class StoreInstructionBase : AssemblerInstruction
{
    private readonly byte directOpCode;
    private readonly byte indirectOpCode;
    private readonly string instructionName;

    public StoreInstructionBase(string instructionName, byte indirectOpCode, byte directOpCode)
    {
        this.instructionName = instructionName;
        this.indirectOpCode = indirectOpCode;
        this.directOpCode = directOpCode;
    }

    public override void Parse(string source)
    {
        var operands = GetOperands(instructionName, source);
        Register = ParseRegister(operands.left);
        if (IsIndirectAddress(operands.right))
        {
            ByteLow = ParseIndirectAddress(operands.right);

            OpCode = indirectOpCode;
        }
        else // its load a constant
        {
            ParseAddress(operands.right);
            OpCode = directOpCode;
        }
    }


    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        if (OpCode == directOpCode)
        {
            return $"{instructionName} r{Register}, (0x{ByteHigh:X2}{ByteLow:X2})";
        }

        if (OpCode == indirectOpCode)
        {
            return $"{instructionName} r{Register}, (r{ByteLow})";
        }

        return "!!ERROR!!";
    }
}