using Shared;

namespace Emulator.Instructions
{
    public class StoreRegisterLowIndirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.StoreRegisterLowIndirect;

        public StoreRegisterLowIndirectInstruction(byte register, byte high, byte low) : base(Opcode, register, high,
            low)
        {
        }

        public void Execute(ICPU cpu)
        {
            var indirectAddress = cpu.Registers[ByteLow];
            var value = (byte)(cpu.Registers[Register] & 0xFF);
            cpu.Memory[indirectAddress] = value;
        }
    }
}