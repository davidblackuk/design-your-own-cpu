using Shared;

namespace Emulator.Instructions
{
    public class CompareWithRegisterInstruction : CompareInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.CompareWithRegister;
        
        public CompareWithRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            ushort left = cpu.Registers[Register];
            ushort right = cpu.Registers[ByteLow];
            Compare(left, right, cpu.Flags);
        }
    }
    
 
    
}