using Shared;

namespace Emulator.Instructions.Interrupts
{
    public interface IInterruptFactory
    {
        IInterrupt Create(ushort vector);
    }

    public class InterruptFactory : IInterruptFactory
    {
        public IInterrupt Create(ushort vector)
        {
            switch (vector)
            {
                case InternalSymbols.WriteStringInterrupt:
                    return new WriteStringInterrupt();
                default:
                    throw new EmulatorException($"Unknown interrupt vector {vector}");
            }
        }
    }
}