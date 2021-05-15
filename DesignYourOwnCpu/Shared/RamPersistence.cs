using System.IO;
using System.Net;

namespace Shared
{
    public static class RamPersistence
    {
        public static void Save(this RandomAccessMemory ram, string path)
        {
            File.WriteAllBytes(path, ram.RawBytes);
        }
    }

    public interface IRamFactory
    {
        RandomAccessMemory Create(string path = null);
    }

    public class RamFactory : IRamFactory
    {

        public RandomAccessMemory Create(string path = null)
        {
            if (path == null)
            {
                return new RandomAccessMemory();
            }
            else
            {
                var bytes = File.ReadAllBytes(path);
                return new RandomAccessMemory(bytes);
            }
        }
    }
    
}