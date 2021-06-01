using System;
using System.Drawing;
using System.Text;
using Pastel;
using Shared;

namespace Emulator.Extensions
{
    public static class RamExtensions
    {
        public static void ToConsole(this IRandomAccessMemory ram, ushort start, ushort length)
        {
            StringBuilder bytesBuilder = new StringBuilder(16*4);
            StringBuilder characterBuilder = new StringBuilder(16);
            
            ushort width = 16;
            for (ushort address = start; address <= start + length; address += width)
            {
                Console.Write($"{address:X4} ".Pastel(Color.Goldenrod));

                bytesBuilder.Clear();
                characterBuilder.Clear();
                byte [] bytes = new byte[16];
                for (ushort column = 0; column < width; column++)
                {
                    byte value = ram[(ushort) (address + column)];
                    bytes[column] = value;
                    bytesBuilder.Append($"{value:X2} ");
                    characterBuilder.Append(AsciiMapper.ConvertByteToChar(value));
                }
                Console.Write(bytesBuilder.ToString().Pastel(Color.Orchid));
                Console.Write(characterBuilder.ToString().Pastel(Color.Teal));
                
                Console.WriteLine();
            }
        }
    }
}