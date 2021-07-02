using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class BranchAlwaysInstructionTests : EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenFlagTrue_ShouldSetProgramCounterToValue()
        {
            ushort expectedProgramCounter = 0x3141;
            var sut = CreateSut(expectedProgramCounter);
            sut.Execute(CpuMock.Object);
            RegistersMock.VerifySet(reg => reg.ProgramCounter = expectedProgramCounter);
        }

        private BranchAlwaysInstruction CreateSut(ushort expectedProgramCounter)
        {
            return new(0, HighByte(expectedProgramCounter), LowByte(expectedProgramCounter));
        }
    }
}