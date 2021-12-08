using System.Diagnostics.CodeAnalysis;
using Assembler.Exceptions;
using Assembler.Instructions.PsuedoInstructions;
using FluentAssertions;
using Moq;
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
            sut.Bytes.Count.Should().Be(mascii.Length);
            sut.Size.Should().Be((ushort)mascii.Length);
            sut.Bytes.Should().ContainInOrder(mascii);
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

        [Test]
        public void Parse_WhenInvokedWithUnquotedText_ShouldThrow()
        {
            var sut = CreateSut();
            Assert.Throws<AssemblerException>(() => { sut.Parse("hello"); });
        }

        
        [Test]
        public void WriteBytes_WhenInvoked_ShouldStoreTheStringCorrectly()
        {
            var sut = CreateSut();
            sut.Parse("\"hello\"");

            Mock<IRandomAccessMemory> ramMock = new();
            
            sut.WriteBytes(ramMock.Object, 22);
            ramMock.VerifySet(s => s[22] = MockAsciiMapper.ConvertCharToByte('h'));
            ramMock.VerifySet(s => s[23] = MockAsciiMapper.ConvertCharToByte('e'));
            ramMock.VerifySet(s => s[24] = MockAsciiMapper.ConvertCharToByte('l'));
            ramMock.VerifySet(s => s[25] = MockAsciiMapper.ConvertCharToByte('l'));
            ramMock.VerifySet(s => s[26] = MockAsciiMapper.ConvertCharToByte('o'));
        }

        private DefineMessageInstruction CreateSut()
        {
            return new DefineMessageInstruction();
        }
    }
}