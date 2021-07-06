using Shared;

namespace Emulator.Instructions.Interrupts
{
    public class InterruptFactory : IInterruptFactory
    {
        public IInterrupt Create(ushort vector)
        {
            switch (vector)
            {
                case InternalSymbols.WriteStringInterrupt:
                    return new WriteStringInterrupt();
                case InternalSymbols.ReadWordInterrupt:
                    return new ReadWordInterrupt();
                case InternalSymbols.WriteWordInterrupt:
                    return new WriteWordInterrupt();
                default:
                    throw new EmulatorException($"Unknown interrupt vector {vector}");
            }
        }
    }
}