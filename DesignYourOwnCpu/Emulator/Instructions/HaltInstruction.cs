﻿using Shared;

namespace Emulator.Instructions
{
    public class HaltInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Halt;

        public HaltInstruction(byte  register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICPU cpu)
        {
            cpu.Halted = true;
        }
    }
}