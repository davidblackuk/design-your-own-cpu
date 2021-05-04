using System;
using System.Diagnostics.CodeAnalysis;
using Assembler.Exceptions;
using Assembler.Instructions;
using Assembler.LineSources;
using Assembler.Symbols;
using Shared;

namespace Assembler
{
    [ExcludeFromCodeCoverage]
    class Program
    {

        static void Main(string[] args)
        {
            var lineSource =
                new CommentStrippingLineSource(new WhitespaceRemovalLineSource(new MemoryLineSource(SourceSupported)));
            
            // poor man's injection for now!
            ISymbolTable symbolTable = new SymbolTable();
            IInstructionNameParser nameParser = new InstructionNameParser();
            IAssemblerInstructionFactory assemblerInstructionFactory = new AssemblerInstructionFactory();
            Parser parser = new Parser(nameParser, assemblerInstructionFactory, symbolTable);
            RandomAccessMemory ram = new RandomAccessMemory();
            CodeGenerator codeGenerator = new CodeGenerator(symbolTable, ram);
            
            Assembler assembler = new Assembler(parser, codeGenerator);

            try
            {
                assembler.Assemble(lineSource);
            }
            catch (AssemblerException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    

        private const string SourceSupported =
@"  NOP
    LD R1, 0x30
.loop
    STL R1, $0xFBFF
    CMP R1, 0x39
    ADD R1, 1
    BLT loop
    LD R1, 0x39
    LD R2, 3
.loop2
    STL R1, $0xFC00
    CMP R1, 0x30
    SUB R1, R2
    BGT loop2
    BRA stopApp
    nop
    nop
    nop

.stopApp
    HALT";
        
        private const string SourceReal =
@"LD R1, 0x30
.loop
STL R1, $0xFBFF
CMP R1, 0x39
ADD R1, 1
BLT loop
LD R1, 0x39
LD R2, 3
.loop2
STL R1, $0xFC00
CMP R1, 0x30
SUB R1, R2
BGT loop2";


    }
}
