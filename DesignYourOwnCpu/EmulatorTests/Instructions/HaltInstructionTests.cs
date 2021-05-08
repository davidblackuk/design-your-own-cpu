using Emulator;
using Emulator.Instructions;
using Moq;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    public class HaltInstructionTests
    {
        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInTheRegister()
        {
            var sut = CreateSut();
            Mock<ICPU> cpuMock = new Mock<ICPU>();
            sut.Execute(cpuMock.Object);

            cpuMock.VerifySet(cpu => cpu.Halted = true, Times.Once());
        }

        private HaltInstruction CreateSut()
        {
            return new HaltInstruction(0, 0, 0);
        }
    }
}