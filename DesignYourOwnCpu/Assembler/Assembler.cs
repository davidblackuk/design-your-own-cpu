using System;
using Assembler.LineSources;
using Assembler.Symbols;
using Shared;

namespace Assembler
{
    public class Assembler : IAssembler
    {
        public ICodeGenerator CodeGenerator { get; }
        public IRandomAccessMemory Ram => CodeGenerator.Ram;

        public ISymbolTable SymbolTable => CodeGenerator.SymbolTable;
        
        private readonly IParser parser;

        public Assembler(IParser parser, ICodeGenerator codeGenerator)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
            this.CodeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
        }

        public void Assemble(ILineSource lineSource)
        {
            parser.ParseAllLines(lineSource);
            CodeGenerator.GenerateCode(parser.Instructions);
        }
    }
}