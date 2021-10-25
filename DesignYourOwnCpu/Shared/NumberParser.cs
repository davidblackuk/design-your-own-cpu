using System;

namespace Shared
{
    public interface INumberParser
    {
        ushort Parse(string text);
    }

    public class NumberParser : INumberParser
    {
        public ushort Parse(string text)
        {
            ushort value = 0;
            if (text.ToLowerInvariant().StartsWith("0x"))
                value = Convert.ToUInt16(text.Substring(2), 16);
            else if (text.ToLowerInvariant().StartsWith("0"))
                value = Convert.ToUInt16(text.Substring(1), 8);
            else
                value = Convert.ToUInt16(text, 10);

            return value;
        }
    }
}