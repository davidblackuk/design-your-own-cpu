using Shared;

namespace Emulator.Instructions;

public class DivideRegisterByConstantInstruction : EmulatorInstruction, IEmulatorInstruction
{
    public const byte Opcode = OpCodes.DivideRegisterByConstant;

    public DivideRegisterByConstantInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
    {
    }

    public void Execute(ICpu cpu)
    {
        cpu.Registers[Register] /= Value;
    }
}