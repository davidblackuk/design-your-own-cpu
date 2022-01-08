namespace Emulator.Instructions.Interrupts;

public interface IInterruptFactory
{
    IInterrupt Create(ushort vector);
}