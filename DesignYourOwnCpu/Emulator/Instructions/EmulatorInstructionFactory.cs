using System.Runtime.InteropServices;
using Shared;

namespace Emulator.Instructions
{
    public class EmulatorInstructionFactory : IEmulatorInstructionFactory
    {

        public IEmulatorInstruction Create(byte opcode, byte register, byte high, byte low)
        {
            switch (opcode)
            {
                case OpCodes.Halt:
                    return new HaltInstruction(opcode, register, high, low);
                case OpCodes.Nop:
                    return new NopInstruction(opcode, register, high, low);
                default:
                    throw new EmulatorException($"Unknown opcode: {opcode:X2}");
            }
        }
    }
}