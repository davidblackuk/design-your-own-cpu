using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions
{
    public class ReturnInstruction : AssemblerInstruction, IAssemblerInstruction
    {
        public const string InstructionName = "ret";
        
        public void Parse(string source)
        {
            this.OpCode = OpCodes.Ret;
        }
        
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return InstructionName;
        }
    }
}