using Shared;

namespace Emulator.Instructions;

public class StoreRegisterHighIndirectInstruction : EmulatorInstruction, IEmulatorInstruction
{
    public const byte Opcode = OpCodes.StoreRegisterHiIndirect;

    public StoreRegisterHighIndirectInstruction(byte register, byte high, byte low) : base(Opcode, register, high,
        low)
    {
    }

    public void Execute(ICpu cpu)
    {
        var indirectAddress = cpu.Registers[ByteLow];
        var value = (byte)((cpu.Registers[Register] >> 8) & 0xFF);
        cpu.Memory[indirectAddress] = value;
    }
}