using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class PopInstructionTests : EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInMemory()
        {
            RegistersMock.SetupGet(r => r.StackPointer).Returns(0xFFFC);
            MemoryMock.Setup(m => m.GetWord(0xFFFC)).Returns(1234);

            var sut = CreateSut(3);
            sut.Execute(CpuMock.Object);

            RegistersMock.VerifySet(r => r[3] = 1234);
        }

        private PopInstruction CreateSut(byte register)
        {
            return new PopInstruction(register, 0, 0);
        }
    }
}