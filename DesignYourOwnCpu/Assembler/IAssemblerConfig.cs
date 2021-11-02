namespace Assembler
{
    public interface IAssemblerConfig
    {
        /// <summary>
        /// Display verbose or quiet console info?
        /// </summary>
        bool QuietOutput { get;  }
        
        /// <summary>
        /// File to assemble
        /// </summary>
        string SourceFilename { get; }
        
        /// <summary>
        /// Output symbol file path 
        /// </summary>
        string SymbolFilename { get; }
        
        /// <summary>
        /// Output binary file
        /// </summary>
        string BinaryFilename { get; }
    }
}