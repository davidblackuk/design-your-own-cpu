using Emulator.Instructions.Interrupts;
using Shared;

namespace Emulator.Instructions
{
    public class EmulatorInstructionFactory : IEmulatorInstructionFactory
    {
        private readonly IInterruptFactory interruptFactory;

        public EmulatorInstructionFactory(IInterruptFactory interruptFactory)
        {
            this.interruptFactory = interruptFactory;
        }

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

                case OpCodes.StoreRegisterIndirect:
                    return new StoreRegisterIndirectInstruction(register, high, low);
                case OpCodes.StoreRegisterHiIndirect:
                    return new StoreRegisterHighIndirectInstruction(register, high, low);
                case OpCodes.StoreRegisterLowIndirect:
                    return new StoreRegisterLowIndirectInstruction(register, high, low);

                case OpCodes.CompareWithConstant:
                    return new CompareWithConstantInstruction(register, high, low);
                case OpCodes.CompareWithRegister:
                    return new CompareWithRegisterInstruction(register, high, low);

                case OpCodes.Branch:
                    return new BranchAlwaysInstruction(register, high, low);
                case OpCodes.BranchLessThan:
                    return new BranchLessThanInstruction(register, high, low);
                case OpCodes.BranchGreaterThan:
                    return new BranchGreaterThanInstruction(register, high, low);
                case OpCodes.BranchEqual:
                    return new BranchEqualInstruction(register, high, low);

                case OpCodes.AddConstantToRegister:
                    return new AddConstantToRegisterInstruction(register, high, low);
                case OpCodes.AddRegisterToRegister:
                    return new AddRegisterToRegisterInstruction(register, high, low);
                case OpCodes.SubtractConstantFromRegister:
                    return new SubtractConstantFromRegisterInstruction(register, high, low);
                case OpCodes.SubtractRegisterFromRegister:
                    return new SubtractRegisterFromRegisterInstruction(register, high, low);

                case OpCodes.Push:
                    return new PushInstruction(register, high, low);
                case OpCodes.Pop:
                    return new PopInstruction(register, high, low);
                case OpCodes.Call:
                    return new CallInstruction(register, high, low);
                case OpCodes.Ret:
                    return new ReturnInstruction(register, high, low);
                case OpCodes.Swi:
                    return new SoftwareInterruptInstruction(interruptFactory, register, high, low);

                default:
                    throw new EmulatorException($"Unknown opcode: {opcode:X2}");
            }
        }
    }
}