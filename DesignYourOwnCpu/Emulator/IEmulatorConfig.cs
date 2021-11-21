namespace Emulator
{
    public interface IEmulatorConfig
    {
        string BinaryFilename { get; }
        bool QuietOutput { get; }
    }
}