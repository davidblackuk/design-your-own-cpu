using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class LoadRegisterFromMemoryInstructionTests : EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInTheRegister()
        {
            // LD R4, R2
            byte targetRegister = 2;
            ushort expectedValue = 0x3456;
            ushort expectedAddress = 0x0034;

            MemoryMock.Setup(m => m.GetWord(expectedAddress)).Returns(expectedValue);

            var sut = CreateSut(targetRegister, expectedAddress);
            sut.Execute(CpuMock.Object);
            RegistersMock.VerifySet(r => r[targetRegister] = expectedValue);
        }

        private LoadRegisterFromMemoryInstruction CreateSut(byte register, ushort expectedAddress)
        {
            return new LoadRegisterFromMemoryInstruction(register, HighByte(expectedAddress), LowByte(expectedAddress));
        }
    }
}