namespace Emulator.Instructions.Interrupts;

public interface IInterrupt
{
    void Execute(ICpu cpu);
}