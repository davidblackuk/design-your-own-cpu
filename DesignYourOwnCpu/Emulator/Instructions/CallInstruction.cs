using Shared;

namespace Emulator.Instructions;

public class CallInstruction : StackInstruction, IEmulatorInstruction
{
    public const byte Opcode = OpCodes.Call;

    public CallInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
    {
    }

    public void Execute(ICpu cpu)
    {
        Push(cpu.Registers.ProgramCounter, cpu);
        cpu.Registers.ProgramCounter = Value;
    }
}