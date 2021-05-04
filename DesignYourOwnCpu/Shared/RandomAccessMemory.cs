using System;

namespace Shared
{
    public interface IRandomAccessMemory
    {
        byte[] RawBytes { get; }
        byte this[ushort address] { get; set; }
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
    }
}
