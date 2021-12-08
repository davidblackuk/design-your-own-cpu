using Shared;

namespace Emulator.Instructions
{
    public class StoreRegisterIndirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.StoreRegisterIndirect;

        public StoreRegisterIndirectInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            var indirectAddress = cpu.Registers[ByteLow];
            cpu.Memory.SetWord(indirectAddress, cpu.Registers[Register]);
        }
    }
}