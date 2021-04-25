using System;

namespace Shared
{
    public class RandomAccessMemory
    {
        public const ushort RamTop = 0xFFFF;
        
        public byte [] RawBytes { get; } = new byte[RamTop+1];

        public byte this[ushort address]
        {
            get => RawBytes[address];
            set => RawBytes[address] = value;
        }
    }
}
