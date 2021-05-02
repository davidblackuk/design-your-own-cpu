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
            IInstructionFactory instructionFactory = new InstructionFactory();
            Parser parser = new Parser(nameParser, instructionFactory, symbolTable);
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
@"nop
STL R1, $0xFBFF
CMP R1, 0x39
#ADD R1, 1
#BLT loop
LD R1, 0x39
ST R1, (0xDD07)
LD R3, 0xDD01
ST R1, (R3)
LD R3, 3
LD R2, R3
.loop2
STL R1, $0xFC00
CMP R1, 0x30
#SUB R1, R2
#BGT loop2
halt";
        
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
