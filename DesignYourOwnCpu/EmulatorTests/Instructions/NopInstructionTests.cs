using System.Diagnostics.CodeAnalysis;
using Emulator;
using Emulator.Instructions;
using Moq;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class NopInstructionTests
    {
        [Test]
        public void Execute_WhenInvoked_ShouldDoNothing()
        {
            var sut = CreateSut();
            var cpuMock = new Mock<ICpu>();

            // no memory set or gegisters so pass is no null ref exceptions
            sut.Execute(cpuMock.Object);
        }

        private NopInstruction CreateSut()
        {
            return new NopInstruction(0, 0, 0);
        }
    }
}