using Shared;

namespace Emulator.Instructions;

public class LoadRegisterDirectInstruction : EmulatorInstruction, IEmulatorInstruction
{
    public const byte Opcode = OpCodes.LoadRegisterFromMemory;

    public LoadRegisterDirectInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
    {
    }

    public void Execute(ICpu cpu)
    {
        cpu.Registers[Register] = cpu.Memory.GetWord(Value);
    }
}