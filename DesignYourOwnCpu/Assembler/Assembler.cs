using System;
using Assembler.Instructions;
using Assembler.LineSources;
using Shared;

namespace Assembler
{
    public class Assembler
    {
        private readonly IParser parser;
        private readonly ICodeGenerator codeGenerator;

        public Assembler(IParser parser, ICodeGenerator codeGenerator)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
            this.codeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
        }

        public void Assemble(ILineSource lineSource)
        {
            parser.ParseAllLines(lineSource);
            codeGenerator.GenerateCode(parser.Instructions);
        }
    }
}
