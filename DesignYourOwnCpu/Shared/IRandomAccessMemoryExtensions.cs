using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Shared;

[ExcludeFromCodeCoverage]
public static class RandomAccessMemoryExtensions
{
    public static void Load(this IRandomAccessMemory ram, string filename)
    {
        var bytes = File.ReadAllBytes(filename);

        for (int idx = 0; idx < bytes.Length; idx++)
        {
            ram[(ushort)idx] = bytes[idx];
        }
    }
        
    public static void Save(this IRandomAccessMemory ram, string path)
    {
        File.WriteAllBytes(path, ram.RawBytes);
    }
}