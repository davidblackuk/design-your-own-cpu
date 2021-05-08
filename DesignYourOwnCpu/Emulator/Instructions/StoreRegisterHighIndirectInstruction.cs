using Shared;

namespace Emulator.Instructions
{
    public class StoreRegisterHighIndirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.StoreRegisterHiDirect;
        
        public StoreRegisterHighIndirectInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            var indirectAddress = cpu.Registers[ByteLow];
            // todo: Put method in base class for this as it's done a lot (High and Low)
            byte value = (byte)((cpu.Registers[Register] >> 8) & 0xFF);
            cpu.Memory[indirectAddress] = value;
        }
    }
}