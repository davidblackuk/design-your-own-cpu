using Shared;

namespace Emulator.Instructions
{
    public class ReturnInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Ret;

        public ReturnInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            cpu.Registers.ProgramCounter = cpu.Memory.GetWord(cpu.Registers.StackPointer);
            cpu.Registers.StackPointer += 4;
        }
    }
}