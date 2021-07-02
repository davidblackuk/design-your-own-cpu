using System.Collections.Generic;

namespace Shared
{
    public class InternalSymbols
    {
        public const ushort WriteStringInterrupt = 0x001;
        public const ushort WriteWordInterrupt = 0x002;
        public const ushort ReadStringInterrupt = 0x003;
        public const ushort ReadWordInterrupt = 0x004;

        public static Dictionary<string, ushort> SystemDefinedSymbols = new()
        {
            ["sys-write-string"] = WriteStringInterrupt,
            ["sys-write-word"] = WriteWordInterrupt,
            ["sys-read-string"] = ReadStringInterrupt,
            ["sys-read-word"] = ReadWordInterrupt
        };
    }
}