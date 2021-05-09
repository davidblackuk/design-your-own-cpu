﻿using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    public class BranchAlwaysInstructionTests: EmulatorUnitTest
    {

        [Test]
        public void Execute_WhenFlagTrue_ShouldSetProgramCounterToValue()
        {
            ushort expectedProgramCounter = 0x3141;
            BranchAlwaysInstruction sut = CreateSut(expectedProgramCounter);
            sut.Execute(CpuMock.Object);
            RegistersMock.VerifySet(reg => reg.ProgramCounter = expectedProgramCounter);
        }
        
        private BranchAlwaysInstruction CreateSut( ushort expectedProgramCounter)
        {
            return new BranchAlwaysInstruction(0, HighByte(expectedProgramCounter), LowByte(expectedProgramCounter));
        }
        
    }
}