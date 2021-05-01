using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions
{
    public class NopInstruction : Instruction, IInstruction
    {
        public const string InstructionName = "nop";

        public void Parse(string source)
        {
            this.OpCode = OpCodes.Nop;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            string bytes = base.ToString();
            return $"{bytes}    # {InstructionName}";
        }
    }
}