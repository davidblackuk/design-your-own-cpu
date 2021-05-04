using System.Collections.Generic;
using Assembler.Instructions;

namespace Assembler
{
    public interface ICodeGenerator
    {
        void GenerateCode(IEnumerable<IAssemblerInstruction> instructions);
    }
}