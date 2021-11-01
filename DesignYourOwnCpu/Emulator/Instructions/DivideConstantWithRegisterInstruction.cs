﻿using Shared;

namespace Emulator.Instructions
{
    public class DivideConstantWithRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.DivideConstantWithRegister;

        public DivideConstantWithRegisterInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Registers[Register] /= Value;
        }
    }
}