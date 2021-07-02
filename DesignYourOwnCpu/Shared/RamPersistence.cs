using System.IO;

namespace Shared
{
    public static class RamPersistence
    {
        public static void Save(this RandomAccessMemory ram, string path)
        {
            File.WriteAllBytes(path, ram.RawBytes);
        }
    }
}