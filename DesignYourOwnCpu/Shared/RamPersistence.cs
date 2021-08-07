using System.IO;

namespace Shared
{
    public static class RamPersistence
    {
        public static void Save(this IRandomAccessMemory ram, string path)
        {
            File.WriteAllBytes(path, ram.RawBytes);
        }
    }
}