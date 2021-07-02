using System;
using Assembler.LineSources;

namespace Assembler
{
    public class Assembler
    {
        private readonly ICodeGenerator codeGenerator;
        private readonly IParser parser;

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