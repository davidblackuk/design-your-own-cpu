using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using FluentAssertions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class EmulatorInstructionTests
    {
        [Test]
        [TestCase(0x00, 0x00, 0x0000)]
        [TestCase(0x00, 0xF0, 0x00F0)]
        [TestCase(0xF0, 0x00, 0xF000)]
        [TestCase(0x12, 0x34, 0x1234)]
        public void ValueWhenAccessed_ShouldReturnCorrectValue(byte high, byte low, int expectedValue)
        {
            var sut = CreateSut(0x01, 0x01, high, low);
            sut.TestValue.Should().Be((ushort)expectedValue);
        }

        private SpyEmulatorInstruction CreateSut(byte opcode, byte register, byte high, byte low)
        {
            return new SpyEmulatorInstruction(opcode, register, high, low);
        }

        private class SpyEmulatorInstruction : EmulatorInstruction
        {
            public SpyEmulatorInstruction(byte opcode, byte register, byte high, byte low) : base(opcode, register,
                high, low)
            {
            }

            public ushort TestValue => Value;
        }
    }
}