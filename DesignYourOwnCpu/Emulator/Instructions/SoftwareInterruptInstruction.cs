using Emulator.Instructions.Interrupts;
using Shared;

namespace Emulator.Instructions;

/// <summary>
///     Handles  software interrupts, each interrupt is actioned by individual interrupt instructions
/// </summary>
public class SoftwareInterruptInstruction : EmulatorInstruction, IEmulatorInstruction
{
    public const byte Opcode = OpCodes.Swi;
    private readonly IInterruptFactory interruptFactory;

    public SoftwareInterruptInstruction(IInterruptFactory interruptFactory, byte register, byte high, byte low) :
        base(Opcode, register, high, low)
    {
        this.interruptFactory = interruptFactory;
    }

    public void Execute(ICpu cpu)
    {
        var interrupt = interruptFactory.Create(Value);
        interrupt.Execute(cpu);
    }
}