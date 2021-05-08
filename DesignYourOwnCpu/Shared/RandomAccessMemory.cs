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

        ushort GetWord(ushort address);
        
        void SetWord(ushort address, ushort value);
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

        public ushort GetWord(ushort address)
        {
            ushort hi = RawBytes[address];
            ushort low = RawBytes[++address];
            return (ushort)(hi << 8 | low);
        }
        
        public void SetWord(ushort address, ushort value)
        {
            var valueBytes = DecomposeWord(value);
            RawBytes[address] = valueBytes.high;
            RawBytes[address + 1] = valueBytes.low;
        }

        public (byte opcode, byte register, byte byteHigh, byte byteLow) Instruction(ushort address)
        {
            return (this[address++], this[address++], this[address++], this[address]);
        }

        private (byte high, byte low) DecomposeWord(ushort value)
        {
            return ( (byte)((value >> 8) & 0xFF), (byte)(value & 0xFF ));
        }
        
    }
}
