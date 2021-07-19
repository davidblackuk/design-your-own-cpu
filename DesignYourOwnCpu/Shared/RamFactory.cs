using System.IO;

namespace Shared
{
    public class RamFactory : IRamFactory
    {
        // private readonly IFileOperations fileOperations;
        //
        // public RamFactory(IFileOperations fileOperations)
        // {
        //     this.fileOperations = fileOperations;
        //}
        
        public RandomAccessMemory Create(string path = null)
        {
            if (path == null)
            {
                return new RandomAccessMemory();
            }

            var bytes = File.ReadAllBytes(path);
            return new RandomAccessMemory(bytes);
        }
    }
}