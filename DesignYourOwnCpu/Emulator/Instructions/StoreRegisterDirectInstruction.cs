using Shared;

namespace Emulator.Instructions;

public class StoreRegisterDirectInstruction : EmulatorInstruction, IEmulatorInstruction
{
    public const byte Opcode = OpCodes.StoreRegisterDirect;

    public StoreRegisterDirectInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
    {
    }

    public void Execute(ICpu cpu)
    {
        cpu.Memory.SetWord(Value, cpu.Registers[Register]);
    }
}