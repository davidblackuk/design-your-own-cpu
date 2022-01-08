using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions.PsuedoInstructions;

/// <summary>
///     Reserves a chunk of memory for storage etc. Only accepts a single size argument
/// </summary>
public class DefineSpaceInstruction : AssemblerInstruction
{
    public const string InstructionName = "defs";

    /// <summary>
    ///     Size of space defined in bytes
    /// </summary>
    public override ushort Size => (ushort)((ByteHigh << 8) | ByteLow);

    /// <summary>
    /// </summary>
    /// <param name="source"></param>
    public override void Parse(string source)
    {
        // TODO: Check if it is a symbol first when we can define constants
        ParseValue(source);
    }

    public override void WriteBytes(IRandomAccessMemory ram, ushort address)
    {
        for (ushort i = 0; i < Size; i++)
        {
            // we might add defs 32, 7 in future to initialize 32 bytes with the value 7
            // for now we zero out the space
            ram[(ushort)(address + i)] = 0;
        }
    }

    [ExcludeFromCodeCoverage]
    public override string BytesString()
    {
        return ".. .. .. ..";
    }

    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        return $"defs 0x{Size:X4}";
    }
}