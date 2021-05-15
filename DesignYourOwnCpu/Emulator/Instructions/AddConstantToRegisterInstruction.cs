﻿namespace Emulator.Instructions
{
    using Shared;
    public class AddConstantToRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.AddConstantToRegister;

        public AddConstantToRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Registers[Register] += Value;
        }
    }
}