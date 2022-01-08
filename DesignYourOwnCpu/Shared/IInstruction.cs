namespace Shared;

public interface IInstruction
{
    /// <summary>
    ///     The opcode for this instruction
    /// </summary>
    byte OpCode { get; }

    /// <summary>
    ///     Primary register number for this instruction if one is needed
    /// </summary>
    byte Register { get; }

    /// <summary>
    ///     High byte of the address or value we are using (if required)
    /// </summary>
    byte ByteHigh { get; }

    /// <summary>
    ///     Low byte of the address or value we are using (if required)
    /// </summary>
    byte ByteLow { get; }

    /// <summary>
    ///     The size of the instruction in bytes (currently always 4 bytes)
    /// </summary>
    public ushort Size { get; }
}