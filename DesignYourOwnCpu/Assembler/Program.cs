using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
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
                        
            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"Could not find input file: {args[0]}");
                return;
            }

            var lineSource =
                new CommentStrippingLineSource(new WhitespaceRemovalLineSource(new FileLineSource(args[0])));

            // poor man's injection for now!
            ISymbolTable symbolTable = new SymbolTable();
            IInstructionNameParser nameParser = new InstructionNameParser();
            IAssemblerInstructionFactory assemblerInstructionFactory = new AssemblerInstructionFactory();
            IRamFactory ramFactory = new RamFactory();
            Parser parser = new Parser(nameParser, assemblerInstructionFactory, symbolTable);
            RandomAccessMemory ram = ramFactory.Create();

            CodeGenerator codeGenerator = new CodeGenerator(symbolTable, ram);

            Assembler assembler = new Assembler(parser, codeGenerator);

            try
            {
                assembler.Assemble(lineSource);
                if (args.Length == 1)
                {
                    ram.Save($"{args[0]}.bin");
                }
            }
            catch (AssemblerException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    }
}
