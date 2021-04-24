using System;

namespace Shared
{
    public class RandomAccessMemory
    {
        public const uint RamTop = 0xFFFF;
        
        public byte [] RawBytes { get; } = new byte[RamTop+1];
    }
}
