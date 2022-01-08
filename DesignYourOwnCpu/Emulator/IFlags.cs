namespace Emulator;

public interface IFlags
{
    bool Equal { get; set; }
    bool LessThan { get; set; }
    bool GreaterThan { get; set; }
    bool Halted { get; set; }
}