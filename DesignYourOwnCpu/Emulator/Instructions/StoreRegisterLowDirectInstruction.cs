using Shared;

namespace Emulator.Instructions
{
    public class StoreRegisterLowDirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.StoreRegisterLowDirect;

        public StoreRegisterLowDirectInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Memory[Value] = (byte)(cpu.Registers[Register] & 0xFF);
        }
    }
}