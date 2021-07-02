using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions
{
    public class NopInstruction : AssemblerInstruction, IAssemblerInstruction
    {
        public const string InstructionName = "nop";

        public override void Parse(string source)
        {
            OpCode = OpCodes.Nop;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"{InstructionName}";
        }
    }
}