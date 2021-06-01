﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Assembler.Exceptions;
using Assembler.Extensions;
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
            string binaryFile = Path.ChangeExtension(args[0], "bin");
            string symbolFile = Path.ChangeExtension(args[0], "sym");

            Console.WriteLine($"\nSource file: {args[0]}");
            Console.WriteLine($"Output file: {binaryFile}");
            Console.WriteLine($"Symbol file: {symbolFile}\n");

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
                DateTime start = DateTime.Now;
                assembler.Assemble(lineSource);
                if (args.Length == 1)
                {
                    ram.Save(binaryFile);
                    Console.WriteLine("\nSymbols\n");
                    symbolTable.Save(symbolFile);
                }
                Console.WriteLine($"\nComplete in {(DateTime.Now - start).TotalMilliseconds} (ms)\n");
            }
            catch (AssemblerException ae)
            {
                ae.ToConsole();
            }
            Console.WriteLine();
        }
    }
}
