using Shared;

namespace Emulator.Instructions;

public class DivideRegisterByRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
{
    public const byte Opcode = OpCodes.DivideRegisterByRegister;

    public DivideRegisterByRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
    {
    }

    public void Execute(ICpu cpu)
    {
        cpu.Registers[Register] /= cpu.Registers[ByteLow];
    }
}