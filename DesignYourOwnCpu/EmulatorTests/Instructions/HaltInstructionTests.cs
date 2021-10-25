using System.Diagnostics.CodeAnalysis;
using Emulator;
using Emulator.Instructions;
using Moq;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class HaltInstructionTests
    {
        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInTheRegister()
        {
            var sut = CreateSut();
            var cpuMock = new Mock<ICPU>();
            var flagsMock = new Mock<IFlags>();
            cpuMock.SetupGet(c => c.Flags).Returns(flagsMock.Object);
            sut.Execute(cpuMock.Object);

            flagsMock.VerifySet(f => f.Halted = true, Times.Once());
        }

        private HaltInstruction CreateSut()
        {
            return new HaltInstruction(0, 0, 0);
        }
    }
}