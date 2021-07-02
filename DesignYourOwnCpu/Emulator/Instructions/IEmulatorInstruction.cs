using Shared;

namespace Emulator.Instructions
{
    public interface IEmulatorInstruction : IInstruction
    {
        void Execute(ICPU cpu);
    }
}