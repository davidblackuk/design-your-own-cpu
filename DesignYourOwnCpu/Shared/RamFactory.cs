using System.IO;

namespace Shared
{
    public class RamFactory : IRamFactory
    {
        public RandomAccessMemory Create(string path = null)
        {
            if (path == null) return new RandomAccessMemory();

            var bytes = File.ReadAllBytes(path);
            return new RandomAccessMemory(bytes);
        }
    }
}