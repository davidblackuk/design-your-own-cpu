namespace Assembler
{
    public interface IAssemblerFiles
    {
        string SourceFilename { get; }
        string SymbolFilename { get; }
        string BinaryFilename { get; }
    }
}