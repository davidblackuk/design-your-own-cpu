using System.IO;
using Microsoft.Extensions.Configuration;

namespace Assembler
{
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