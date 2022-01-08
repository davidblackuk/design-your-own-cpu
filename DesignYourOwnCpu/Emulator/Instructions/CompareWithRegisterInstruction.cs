using Shared;

namespace Emulator.Instructions;

public class CompareWithRegisterInstruction : CompareInstruction, IEmulatorInstruction
{
    public const byte Opcode = OpCodes.CompareWithRegister;

    public CompareWithRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
    {
    }

    public void Execute(ICpu cpu)
    {
        var left = cpu.Registers[Register];
        var right = cpu.Registers[ByteLow];
        Compare(left, right, cpu.Flags);
    }
}