using Shared;

namespace Emulator.Instructions
{
  
    public class PushInstruction : StackInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Push;

        public PushInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu) => Push(cpu.Registers[Register], cpu);
    }
}