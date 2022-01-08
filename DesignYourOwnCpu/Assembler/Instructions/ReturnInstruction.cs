using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions;

public class ReturnInstruction : AssemblerInstruction
{
    public const string InstructionName = "ret";

    public override void Parse(string source)
    {
        OpCode = OpCodes.Ret;
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        return InstructionName;
    }
}