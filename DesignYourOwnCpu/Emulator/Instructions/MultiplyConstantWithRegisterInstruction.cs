﻿using Shared;

namespace Emulator.Instructions
{
    public class MultiplyConstantWithRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.MultiplyConstantWithRegister;

        public MultiplyConstantWithRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Registers[Register] *= Value;
        }
    }
}