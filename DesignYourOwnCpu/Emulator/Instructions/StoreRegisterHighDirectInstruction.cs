using Shared;

namespace Emulator.Instructions
{
    public class StoreRegisterHighDirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.StoreRegisterHiDirect;
        
        public StoreRegisterHighDirectInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Memory[Value] = (byte)((cpu.Registers[Register] >> 8) & 0xFF);
        }
    }
}