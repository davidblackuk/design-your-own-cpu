using System.Net.WebSockets;
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
                    return new HaltInstruction(register, high, low);
                case OpCodes.Nop:
                    return new NopInstruction(register, high, low);
                
                case OpCodes.LoadRegisterWithConstant:
                    return new LoadRegisterWithConstantInstruction(register, high, low);
                case OpCodes.LoadRegisterFromMemory:
                    return new LoadRegisterFromMemoryInstruction(register, high, low);
                case OpCodes.LoadRegisterFromRegister:
                    return new LoadRegisterFromRegisterInstruction(register, high, low);
                case OpCodes.StoreRegisterDirect:
                    return new StoreRegisterDirectInstruction(register, high, low);
                case OpCodes.StoreRegisterHiDirect:
                    return new StoreRegisterHighDirectInstruction(register, high, low);
                case OpCodes.StoreRegisterLowDirect:
                    return new StoreRegisterLowDirectInstruction(register, high, low);
                
                default:
                    throw new EmulatorException($"Unknown opcode: {opcode:X2}");
            }
        }
    }
}