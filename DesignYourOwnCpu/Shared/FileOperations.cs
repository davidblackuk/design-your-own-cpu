using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Shared
{
    public interface IFileOperations
    {
        byte[] ReadAllBytes(string path);
    }

    /// <summary>
    /// delegates calls to system read write functions to allow the callers to be tested
    /// </summary>
    [ExcludeFromCodeCoverage] // just a straight delegation, we don't test system functions
    public class FileOperations : IFileOperations
    {
        public byte[] ReadAllBytes(string path) => File.ReadAllBytes(path);
    }
}