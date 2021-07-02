namespace Shared
{
    public interface IRandomAccessMemory
    {
        byte[] RawBytes { get; }
        byte this[ushort address] { get; set; }


        /// <summary>
        ///     Gets the instruction data for the instruction at the specified address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        (byte opcode, byte register, byte byteHigh, byte byteLow) Instruction(ushort address);

        ushort GetWord(ushort address);

        void SetWord(ushort address, ushort value);
    }
}