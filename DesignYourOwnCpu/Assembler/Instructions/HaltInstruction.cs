using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions
{
    public class HaltInstruction : Instruction, IInstruction
    {
        public const string InstructionName = "halt";
        
        public void Parse(string source)
        {
            this.OpCode = OpCodes.Halt;
        }
        
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            string bytes = base.ToString();
            return $"{bytes}    # {InstructionName}";
        }
    }
}