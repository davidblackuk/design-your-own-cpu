using System;
using System.Collections.Generic;
using Assembler.Instructions;
using Assembler.LineSources;
using Assembler.Symbols;

namespace Assembler;

public class Parser : IParser
{
    private readonly IAssemblerInstructionFactory assemblerInstructionFactory;
    private readonly IInstructionNameParser nameParser;
    private readonly ISymbolTable symbolTable;

    public Parser(IInstructionNameParser nameParser, IAssemblerInstructionFactory assemblerInstructionFactory,
        ISymbolTable symbolTable)
    {
        this.nameParser = nameParser ?? throw new ArgumentNullException(nameof(nameParser));
        this.assemblerInstructionFactory = assemblerInstructionFactory ??
                                           throw new ArgumentNullException(nameof(assemblerInstructionFactory));
        this.symbolTable = symbolTable ?? throw new ArgumentNullException(nameof(symbolTable));
    }

    public List<IAssemblerInstruction> Instructions { get; } = new();

    public void ParseAllLines(ILineSource lineSource)
    {
        ushort currentAddress = 0;
        foreach (var line in lineSource.Lines())
        {
            var parsedLine = nameParser.Parse(line);

            // defining a label?
            if (parsedLine.instruction.StartsWith("."))
            {
                DefineSymbol(parsedLine.instruction.Substring(1), currentAddress);
            }
            else
            {
                var instruction = assemblerInstructionFactory.Create(parsedLine.instruction);
                instruction.Parse(parsedLine.remainder);
                Instructions.Add(instruction);
                currentAddress += instruction.Size;
            }
        }
        // todo: throw exception with message for any undefined symbols at this point.
    }

    // defines a label at the current address in the assembly process. This is the count of
    // instructions * 4 (at the moment as we have no defw etc so memory is contiguous. However
    // we may need to track the current memory address properly in the future.
    //
    // <para>The label may already exist, not have an address (ie forward reference), or
    // be a new one. </para>
    private void DefineSymbol(string labelName, ushort currentAddress)
    {
        symbolTable.DefineSymbol(labelName, currentAddress);
    }
}