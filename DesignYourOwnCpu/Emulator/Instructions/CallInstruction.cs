using Shared;

namespace Emulator.Instructions
{
    public class CallInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Call;

        public CallInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Registers.StackPointer -= 4;
            cpu.Memory.SetWord(cpu.Registers.StackPointer, cpu.Registers.ProgramCounter);
            cpu.Registers.ProgramCounter = Value;
        }
    }
}