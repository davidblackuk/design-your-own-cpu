using Emulator;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
namespace EmulatorTests
{
    public class RegistersTests
    {

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void Register_WhenLegalAndSet_ShouldBeStoredCorrectly(int register)
        {
            byte ExpectedValue = 0x38;
            var sut = CreateSut();
            sut[(byte)register] = ExpectedValue;
            sut[(byte) register].Should().Be(ExpectedValue);
            for (byte i = 0; i < 8; i++)
            {
                if (i != register)
                {
                    sut[i].Should().Be(0);
                }
            }

        }

        [Test]
        public void ProgramCounter_WhenSet_ShouldPersist()
        {
            var sut = CreateSut();
            ushort expectedValue = 0x4467;
            sut.ProgramCounter = expectedValue;
            sut.ProgramCounter.Should().Be(expectedValue);
        }
        
        [Test]
        public void StackPointer_WhenSet_ShouldPersist()
        {
            var sut = CreateSut();
            ushort expectedValue = 0x4467;
            sut.StackPointer = expectedValue;
            sut.StackPointer.Should().Be(expectedValue);
        }

    private Registers CreateSut()
        {
            return new Registers();
        }
    }
}