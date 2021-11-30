using System;
using Emulator;
using Emulator.Instructions.Interrupts;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Shared;

namespace EmulatorTests.Instructions.Interrupts
{
    public class InterruptFactoryTests
    {
        private Mock<IServiceProvider> serviceProviderMock;

        [SetUp]
        public void SetUp()
        {
            serviceProviderMock = new Mock<IServiceProvider>();
        }
        
        [Test]
        [TestCase(InternalSymbols.ReadWordInterrupt, typeof(ReadWordInterrupt))]
        [TestCase(InternalSymbols.WriteStringInterrupt, typeof(WriteStringInterrupt))]
        [TestCase(InternalSymbols.WriteWordInterrupt, typeof(WriteWordInterrupt))]
        public void Create_WhenCalledWithAKnownInstructionName_ShouldReturnCorrectType(
            ushort vector, Type expectedType)
        {
            InterruptFactory sut = CreateSut();
            var instruction = sut.Create(vector);
            instruction.GetType().Name.Should().Be(expectedType.Name);
        }

        [Test]
        public void Create_WhenCalledWithAUnknownInstructionName_ShouldThrow()
        {
            InterruptFactory sut = CreateSut();
            Assert.Throws<EmulatorException>(() =>  sut.Create(999));
        }
        
        
        
        private InterruptFactory CreateSut()
        {
            return new InterruptFactory(serviceProviderMock?.Object);
        }
    }
}