using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using Moq;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class PushInstructionTests : EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInMemory()
        {
            byte targetRegister = 6;
            ushort expectedValue = 0x3456;
            ushort expectedStackPointer = 0xFFFF;

            RegistersMock.SetupGet(r => r.StackPointer).Returns(expectedStackPointer);
            RegistersMock.SetupGet(r => r[targetRegister]).Returns(expectedValue);

            var sut = CreateSut(targetRegister);
            sut.Execute(CpuMock.Object);
            RegistersMock.VerifySet(r => r.StackPointer = (ushort) (expectedStackPointer - 4), Times.Once());
            MemoryMock.Verify(m => m.SetWord(It.IsAny<ushort>(), expectedValue));
        }

        private PushInstruction CreateSut(byte register)
        {
            return new(register, 0, 0);
        }
    }
}