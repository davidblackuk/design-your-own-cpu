namespace Emulator.Instructions;

public interface IEmulatorInstructionFactory
{
    IEmulatorInstruction Create(byte opcode, byte register, byte high, byte low);
}