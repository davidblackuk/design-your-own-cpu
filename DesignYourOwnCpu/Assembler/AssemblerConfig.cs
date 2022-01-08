using System.IO;
using Microsoft.Extensions.Configuration;

namespace Assembler;

public class AssemblerConfig : IAssemblerConfig
{
    private readonly IConfigurationRoot config;

    public AssemblerConfig(IConfigurationRoot config)
    {
        this.config = config;
    }

    public bool QuietOutput => config["quiet"] != null && config["quiet"].ToLowerInvariant() == "true";

    public string SourceFilename => config["input"];

    public string SymbolFilename => Path.ChangeExtension(SourceFilename, "sym");

    public string BinaryFilename => Path.ChangeExtension(SourceFilename, "bin");
}