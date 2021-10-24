using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using Emulator.Instructions.Interrupts;
using Moq;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class SoftwareInterruptInstructionTests: EmulatorUnitTest
    {
        private Mock<IInterruptFactory> interruptFactoryMock;
        
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            interruptFactoryMock = new Mock<IInterruptFactory>();
        }

        [Test]
        public void Execute_WhenInvoked_ShouldGetTheCorrectInstructionFromTheFactory()
        {
            ushort expectedValue = 0x2376;
            var interruptMock = new Mock<IInterrupt>();
            interruptFactoryMock.Setup(ifm => ifm.Create(expectedValue)).Returns(interruptMock.Object);
            
            var sut = CreateSut(expectedValue);
            
            sut.Execute(CpuMock.Object);
            interruptFactoryMock.Verify(im => im.Create(expectedValue));
        }
        
        [Test]
        public void Execute_WhenInvoked_ShouldExecuteTHeInterruptOnTheCpu()
        {
            ushort expectedValue = 0x2376;
            var interruptMock = new Mock<IInterrupt>();
            interruptFactoryMock.Setup(ifm => ifm.Create(expectedValue)).Returns(interruptMock.Object);
            
            var sut = CreateSut(expectedValue);
            
            sut.Execute(CpuMock.Object);
            interruptMock.Verify(im => im.Execute(CpuMock.Object));
        }

        private SoftwareInterruptInstruction CreateSut(ushort value)
        {
            return new SoftwareInterruptInstruction(interruptFactoryMock?.Object, 0, HighByte(value), LowByte(value));
        }
    }
}