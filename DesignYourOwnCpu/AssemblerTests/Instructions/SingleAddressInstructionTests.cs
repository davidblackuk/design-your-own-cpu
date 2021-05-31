using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions
{
    public class SingleAddressInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode()
        {
            var sut = CreateSut();
            sut.OpCode.Should().Be(OpCodes.BranchLessThan);
        }

        [Test]
        public void Parse_WhenItIsALabel_ShouldStoreTheSymbolCorrectly()
        {
            var sut = CreateSut();
            var expected = "loop1";
            sut.Parse(expected);
            sut.Symbol.Should().Be(expected);
            sut.RequresSymbolResolution.Should().BeTrue();
        }

        [Test]
        public void Parse_WhenItIsAValue_ShouldNotMarkAsRequiresSymbolResolution()
        {
            var sut = CreateSut();
            sut.Parse("0xFFEE");
            sut.RequresSymbolResolution.Should().BeFalse();
        }

        [Test]
        public void Parse_WhenItIsAValue_ShouldStoreTheValueCorrectly()
        {
            var sut = CreateSut();
            sut.Parse("0xFFEE");
            sut.ByteHigh.Should().Be(0xFF);
            sut.ByteLow.Should().Be(0xEE);
        }


        private SingleAddressInstruction CreateSut() => new SingleAddressInstruction("blt", OpCodes.BranchLessThan);
    }
}