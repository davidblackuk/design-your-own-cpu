using Shared;

namespace Emulator.Instructions
{
    public class LoadRegisterFromMemoryInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.LoadRegisterFromMemory;
        
        public LoadRegisterFromMemoryInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Registers[Register] = cpu.Memory.GetWord(Value);
        }
    }
}