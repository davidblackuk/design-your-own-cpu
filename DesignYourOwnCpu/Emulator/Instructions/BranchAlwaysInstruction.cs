using Shared;

namespace Emulator.Instructions;

public class BranchAlwaysInstruction : EmulatorInstruction, IEmulatorInstruction
{
    public const byte Opcode = OpCodes.Branch;

    public BranchAlwaysInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
    {
    }

    public void Execute(ICpu cpu)
    {
        cpu.Registers.ProgramCounter = Value;
    }
}