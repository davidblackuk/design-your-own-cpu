using Shared;

namespace Emulator.Instructions
{
    public class ReturnInstruction : StackInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Ret;

        public ReturnInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu) => cpu.Registers.ProgramCounter = Pop(cpu);
    }
}