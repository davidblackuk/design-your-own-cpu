using System;
using Assembler.Exceptions;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.Instructions
{
    public class InstructionTests
    {
        [Test]
        [TestCase("")]
        [TestCase("A")]
        [TestCase("A,B,C")]
        public void Operands_WhenCalledWithIncorrectNumberOfParts_ShouldThrowAssemblerException(string line)
        {
            var sut = CreateSut();
            Assert.Throws<AssemblerException>(() => sut.Parse(line));
        }
        
        [Test]
        [TestCase("r1,34", "r1", "34")]
        [TestCase("  r1,    34   ", "r1", "34")]
        public void Operands_WhenCalledWithTwoParts_ShouldReturnTrimmedParts(string line, string expectedLeft, string expectedRight)
        {
            var sut = CreateSut();
            sut.Parse(line);
            sut.Left.Should().Be(expectedLeft);
            sut.Right.Should().Be(expectedRight);
        }

        [Test]
        [TestCase("r1", true)]
        [TestCase("R1", true)]
        [TestCase("47", false)]
        [TestCase("(47)", false)]
        public void IsRegister_WhenCalled_ProcessesCorrectlyAndCaseInsensitively(string text, bool isRegister)
        {
            var sut = CreateSut();
            var res = sut.TestIsRegister(text);
            res.Should().Be(isRegister);
        }
        
        [Test]
        [TestCase("r1", false)]
        [TestCase("R1", false)]
        [TestCase("47", false)]
        [TestCase("(47)", true)]
        public void IsAddress_WhenCalled_ProcessesCorrectlyAndCaseInsensitively(string text, bool isAddress)
        {
            var sut = CreateSut();
            var res = sut.TestIsAddress(text);
            res.Should().Be(isAddress);
        }

        [Test]
        [TestCase("0x5678", 0x56, 0x78)]
        [TestCase("04567", 0x09, 0x77)]
        [TestCase("9876", 0x26, 0x94)]
        public void ParseValue_whenInvoked_shouldParseInTheCorrectBase(string text, byte expectedHigh, byte expectedLow)
        {
            var sut = CreateSut();
            sut.TestParseValue(text);
            sut.ByteHigh.Should().Be(expectedHigh);
            sut.ByteLow.Should().Be(expectedLow);
        }
        
        [Test]
        [TestCase("(0x5678)", 0x56, 0x78)]
        [TestCase("(04567)", 0x09, 0x77)]
        [TestCase("(9876)", 0x26, 0x94)]
        public void ParseAddress_whenInvoked_shouldParseInTheCorrectBase(string text, byte expectedHigh, byte expectedLow)
        {
            var sut = CreateSut();
            sut.TestParseAddress(text);
            sut.ByteHigh.Should().Be(expectedHigh);
            sut.ByteLow.Should().Be(expectedLow);
        }

        [Test]
        [TestCase("0xff55")]
        [TestCase("(0xff55)")]
        [TestCase("raster")]
        [TestCase("r11")]
        public void ParseRegister_WhenInvokedWithIllegalRegister_ShouldThrowAssemblerException(string text)
        {
            var sut = CreateSut();
            Assert.Throws<AssemblerException>(() => sut.TestParseRegister(text));
        }

        private SpyInstruction CreateSut()
        {
            return new SpyInstruction();
        }

        public class SpyInstruction : Instruction
        {

            public string Left { get; set; }
            public string Right { get; set; }

            public bool TestIsRegister(string text) => IsRegister(text);
            public bool TestIsAddress(string text) => IsAddress(text);
            public void TestParseValue(string text) => ParseValue(text);
            public void TestParseAddress(string text) => ParseAddress(text);
            public void TestParseRegister(string text) => ParseRegister(text);
            
            public void Parse(string source)
            {
                var parts = base.GetOperands("xor",source);
                Left = parts.left;
                Right = parts.right;
            }
        }
    }
}