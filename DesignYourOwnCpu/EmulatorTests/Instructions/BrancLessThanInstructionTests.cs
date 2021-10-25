using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using Moq;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class BrancLessThanInstructionTests : EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenFlagTrue_ShouldSetProgramCounterToValue()
        {
            ushort expectedProgramCounter = 0x3141;
            FlagsMock.SetupGet(f => f.LessThan).Returns(true);
            var sut = CreateSut(expectedProgramCounter);
            sut.Execute(CpuMock.Object);
            RegistersMock.VerifySet(reg => reg.ProgramCounter = expectedProgramCounter);
        }

        [Test]
        public void Execute_WhenFlagFalse_ShouldNotSetProgramCounter()
        {
            ushort expectedProgramCounter = 0x3141;
            FlagsMock.SetupGet(f => f.LessThan).Returns(false);
            var sut = CreateSut(expectedProgramCounter);
            sut.Execute(CpuMock.Object);
            RegistersMock.VerifySet(reg => reg.ProgramCounter = expectedProgramCounter, Times.Never);
        }


        private BranchLessThanInstruction CreateSut(ushort expectedProgramCounter)
        {
            return new BranchLessThanInstruction(0, HighByte(expectedProgramCounter), LowByte(expectedProgramCounter));
        }
    }
}