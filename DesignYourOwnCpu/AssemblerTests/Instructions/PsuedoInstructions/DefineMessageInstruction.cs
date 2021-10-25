using System.Diagnostics.CodeAnalysis;
using Assembler.Exceptions;
using Assembler.Instructions.PsuedoInstructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions.PsuedoInstructions
{
    [ExcludeFromCodeCoverage]
    public class DefineMessageInstructionTests
    {
        [Test]
        [TestCase("Hello", new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F })]
        [TestCase("\\\"Hello", new byte[] { MockAsciiMapper.Quote, 0x48, 0x65, 0x6C, 0x6C, 0x6F })]
        [TestCase("Hel\\tlo", new byte[] { 0x48, 0x65, 0x6C, MockAsciiMapper.Tab, 0x6C, 0x6F })]
        [TestCase("Hel\\\\lo", new byte[] { 0x48, 0x65, 0x6C, MockAsciiMapper.Slash, 0x6C, 0x6F })]
        [TestCase("Hello\\0", new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x00 })]
        [TestCase("Hello\\n", new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, MockAsciiMapper.NewLine })]
        [TestCase("Hel\\nlo", new byte[] { 0x48, 0x65, 0x6C, MockAsciiMapper.NewLine, 0x6C, 0x6F })]
        public void Parse_WhenInvoked_ShouldCorrectlyParseStringToBytes(string original, byte[] mascii)
        {
            var sut = CreateSut();
            sut.Parse($"\"{original}\"");
            sut.bytes.Count.Should().Be(mascii.Length);
            sut.Size.Should().Be((ushort)mascii.Length);
            sut.bytes.Should().ContainInOrder(mascii);
        }

        [Test]
        public void Parse_WhenInvokedWithTerminalSlash_ShouldThrow()
        {
            var sut = CreateSut();
            Assert.Throws<AssemblerException>(() => { sut.Parse("\"I am an error\\\""); });
        }

        [Test]
        public void Parse_WhenInvokedWithUnknownSequence_ShouldThrow()
        {
            var sut = CreateSut();
            Assert.Throws<AssemblerException>(() => { sut.Parse("\"I, \\y, am an error\""); });
        }

        private DefineMessageInstruction CreateSut()
        {
            return new DefineMessageInstruction();
        }
    }
}