using System;
using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions.PsuedoInstructions;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class DefineSpaceInstructionTests
    {
        [Test]
        public void Parse_whenCalled_ShouldThrowErrorForNnnNumericLengthValues()
        {
            var sut = CreateSut();
            Assert.Throws<FormatException>(() => sut.Parse("Hello"));
        }


        [Test]
        public void Parse_whenCalled_ShouldCorrectlyParseInstruction()
        {
            var sut = CreateSut();
            sut.Parse("0x1267");
            sut.ByteHigh.Should().Be(0x12);
            sut.ByteLow.Should().Be(0x67);
        }

        [Test]
        public void Parse_whenCalled_ShouldCorrectlySetTheSize()
        {
            var sut = CreateSut();
            sut.Parse("0x1267");
            sut.Size.Should().Be(0x1267);
        }


        private DefineSpaceInstruction CreateSut()
        {
            return new DefineSpaceInstruction();
        }
    }
}