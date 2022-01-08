using Shared;

namespace Emulator.Instructions;

public class EmulatorInstruction : Instruction
{
    public EmulatorInstruction(byte opcode, byte register, byte high, byte low)
    {
        OpCode = opcode;
        Register = register;
        ByteHigh = high;
        ByteLow = low;
    }

    protected ushort Value => (ushort)((ByteHigh << 8) | ByteLow);
}