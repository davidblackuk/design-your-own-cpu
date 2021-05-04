using System;

namespace Shared
{
    public interface IRandomAccessMemory
    {
        byte[] RawBytes { get; }
        byte this[ushort address] { get; set; }

        /// <summary>
        /// Gets the instruction data for the instruction at the specified address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        (byte opcode, byte register, byte byteHigh, byte byteLow) Instruction(ushort address);
    }

    public class RandomAccessMemory : IRandomAccessMemory
    {
        public const ushort RamTop = 0xFFFF;
        
        public byte [] RawBytes { get; } = new byte[RamTop+1];

        public byte this[ushort address]
        {
            get => RawBytes[address];
            set => RawBytes[address] = value;
        }

        public (byte opcode, byte register, byte byteHigh, byte byteLow) Instruction(ushort address)
        {
            return (this[address++], this[address++], this[address++], this[address]);
        }
    }
}
