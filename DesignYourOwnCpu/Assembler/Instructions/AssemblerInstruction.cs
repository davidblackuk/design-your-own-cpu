using System;
using System.Diagnostics.CodeAnalysis;
using Assembler.Exceptions;
using Shared;

namespace Assembler.Instructions;

public class AssemblerInstruction : Instruction, IAssemblerInstruction
{
    /// <summary>
    ///     TODO: We should inject this
    /// </summary>
    private readonly INumberParser numberParser = new NumberParser();

    /// <summary>
    ///     For symbols this contains the name that needs to be resolved
    /// </summary>
    public string Symbol { get; private set; }

    /// <summary>
    ///     Does this instruction require a symbol to be resolved for it to be output
    /// </summary>
    public bool RequresSymbolResolution => !string.IsNullOrWhiteSpace(Symbol);


    public void StoreData(ushort value)
    {
        ByteLow = (byte)(value & 0xff);
        ByteHigh = (byte)((value >> 8) & 0xff);
    }

    [ExcludeFromCodeCoverage]
    public virtual void Parse(string source)
    {
        throw new NotImplementedException();
    }

    public virtual string BytesString()
    {
        return $"{OpCode:X2} {Register:X2} {ByteHigh:X2} {ByteLow:X2}";
    }

    /// <summary>
    ///     For the vast majority of instructions this base method writes the 4 bytes of
    ///     the instruction to memory, but for the storage instructions (defs, defw, defb etc)
    ///     there are a variable number of bytes to output
    /// </summary>
    /// <param name="ram"></param>
    /// <param name="address"></param>
    /// <exception cref="NotImplementedException"></exception>
    public virtual void WriteBytes(IRandomAccessMemory ram, ushort address)
    {
        ram[address++] = OpCode;
        ram[address++] = Register;
        ram[address++] = ByteHigh;
        ram[address++] = ByteLow;
    }

    /// <summary>
    ///     Splits the remaining line into two parts on a comma, returns the left operand and right
    ///     operand as trimmed strings
    /// </summary>
    /// <param name="???"></param>
    /// <param name="instructionName"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    protected (string left, string right) GetOperands(string instructionName, string line)
    {
        var parts = line.Split(",".ToCharArray(),
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length != 2)
        {
            throw new AssemblerException(
                $"Expected two operands for instruction: {instructionName} , but found {parts.Length}");
        }

        return (parts[0], parts[1]);
    }

    protected bool IsRegister(string source)
    {
        source = source.ToLowerInvariant();
        return source.StartsWith("r") &&
               source.Length >= 2 &&
               char.IsDigit(source[1]);
    }

    protected bool IsAddress(string source)
    {
        return source.StartsWith("(") && source.EndsWith(")");
    }

    /// <summary>
    ///     Is this an indirect address of the form (Rx) where x is a register number
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    protected bool IsIndirectAddress(string text)
    {
        return IsAddress(text) &&
               text.Substring(1)
                   .Substring(0, text.Length - 2)
                   .Trim()
                   .ToLowerInvariant()
                   .StartsWith("r");
    }

    protected bool IsLabel(string source)
    {
        source = source.Trim();
        return !string.IsNullOrWhiteSpace(source) &&
               !IsRegister(source) &&
               !char.IsDigit(source[0]);
    }

    protected void ParseConstantValueOrSymbol(string source)
    {
        if (IsLabel(source))
        {
            RecordSymbolForResolution(source);
        }
        else
        {
            ParseValue(source);
        }
    }

    protected byte ParseRegister(string source)
    {
        if (!IsRegister(source))
        {
            throw new AssemblerException("Expected a register but got: " + source);
        }

        var registerNumber = source.Substring(1);

        // no need to try parse as we know it's a valis register so has to be a numeric digit
        var register = byte.Parse(registerNumber);
        if (register > Constants.MaxRegisterNumber)
        {
            throw new AssemblerException($"{source} is an illegal register number, values registers are r0..r7");
        }

        return register;
    }

    protected void ParseAddress(string text)
    {
        ParseValue(text.Substring(1).Substring(0, text.Length - 2));
    }

    /// <summary>
    ///     Parse an indirect address of the form (Rx) where the register holds the address of the memory location to use
    /// </summary>
    /// <param name="text"></param>
    /// <returns>the byte value of the register number</returns>
    protected byte ParseIndirectAddress(string text)
    {
        var registerText = text.Substring(1).Substring(0, text.Length - 2).Trim();
        return ParseRegister(registerText);
    }

    /// <summary>
    ///     Resolve the value associated with a symbol / label.
    /// </summary>
    /// <param name="value"></param>
    public void ResolveSymbol(ushort value)
    {
        StoreData(value);
    }

    protected void ParseValue(string text)
    {
        if (String.IsNullOrWhiteSpace(text))
        {
            throw new AssemblerException("Expected constant value but none found");
        }

        text = text.Trim();
            
        StoreData(numberParser.Parse(text));
    }

    protected void RecordSymbolForResolution(string symbol)
    {
        Symbol = symbol.ToLowerInvariant();
    }
}