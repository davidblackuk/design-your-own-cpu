namespace Shared
{
    public interface IRandomAccessMemory
    {
        byte[] RawBytes { get; }
        
        byte this[ushort address] { get; set; }

        (byte opcode, byte register, byte byteHigh, byte byteLow) Instruction(ushort address);

        ushort GetWord(ushort address);

        void SetWord(ushort address, ushort value);
    }
}