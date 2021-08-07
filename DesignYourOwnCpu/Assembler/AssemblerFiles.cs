using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Assembler
{
    public interface IAssemblerFiles
    {
        string SourceFilename { get; }
        string SymbolFilename { get; }
        string BinaryFilename { get; }
    }

    public class AssemblerFiles : IAssemblerFiles
    {
        private readonly IConfigurationRoot config;

        public AssemblerFiles(IConfigurationRoot config)
        {
            this.config = config;
        }

        public string SourceFilename => config["input"];

        public string SymbolFilename => Path.ChangeExtension(SourceFilename, "sym");

        public string BinaryFilename => Path.ChangeExtension(SourceFilename, "bin");
    }
}