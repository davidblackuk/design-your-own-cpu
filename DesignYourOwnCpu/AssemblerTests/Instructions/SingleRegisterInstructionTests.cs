using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class SingleRegisterInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode()
        {
            var sut = CreateSut();
            sut.OpCode.Should().Be(OpCodes.Push);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void Parse_WhenInvoked_ShouldStoreTheRegisterCorrectly(int expectedRegister)
        {
            var sut = CreateSut();

            sut.Parse($"r{expectedRegister}");
            sut.Register.Should().Be((byte) expectedRegister);
            sut.RequresSymbolResolution.Should().BeFalse();
        }

        private SingleRegisterInstruction CreateSut()
        {
            return new("push", OpCodes.Push);
        }
    }
}