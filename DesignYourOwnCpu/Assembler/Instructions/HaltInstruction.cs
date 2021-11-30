using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions
{
    public class HaltInstruction : AssemblerInstruction
    {
        public const string InstructionName = "halt";

        public override void Parse(string source)
        {
            OpCode = OpCodes.Halt;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return InstructionName;
        }
    }
}