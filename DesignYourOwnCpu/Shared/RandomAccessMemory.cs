using System;

namespace Shared
{
    public class RandomAccessMemory : IRandomAccessMemory
    {
        public const ushort RamTop = 0xFFFF;

        public RandomAccessMemory()
        {
        }

        internal RandomAccessMemory(byte[] from)
        {
            if (from.Length != RamTop + 1)
            {
                throw new ArgumentException($"Expected exactly {RamTop + 1} bytes to be passed for a RAM image",
                    nameof(@from));
            }

            RawBytes = from;
        }

        public byte[] RawBytes { get; } = new byte[RamTop + 1];

        public byte this[ushort address]
        {
            get => RawBytes[address];
            set => RawBytes[address] = value;
        }

        public ushort GetWord(ushort address)
        {
            ushort hi = RawBytes[address];
            ushort low = RawBytes[++address];
            return (ushort)((hi << 8) | low);
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
            return ((byte)((value >> 8) & 0xFF), (byte)(value & 0xFF));
        }
    }
}