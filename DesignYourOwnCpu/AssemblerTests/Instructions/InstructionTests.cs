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
        [TestCase("r")]
        public void ParseRegister_WhenInvokedWithIllegalRegister_ShouldThrowAssemblerException(string text)
        {
            var sut = CreateSut();
            Assert.Throws<AssemblerException>(() => sut.TestParseRegister(text));
        }

        [Test]
        public void ParseRegister_WhenInvokedWithAValidRegister_ShouldReturnTHeRagisterNumber()
        {
            var sut = CreateSut();
            sut.TestParseRegister("r3").Should().Be(3);
        }
        
        
        
        [Test]
        [TestCase("hello", true)]
        [TestCase("  hello  ", true)]
        [TestCase(" hello", true)]
        [TestCase("hello ", true)]
        [TestCase("r2", false)]
        [TestCase(" r2 " , false)]
        [TestCase(" 0x3456 " , false)]
        [TestCase(" 03456 " , false)]
        [TestCase("" , false)]
        [TestCase(" 3456 " , false)]
        public void IsLabel_WhenCalled_ShouldHandle(string text, bool expectedIsLabel)
        {
            var sut = CreateSut();
            sut.TestIsLabel(text).Should().Be(expectedIsLabel);
        }
        
        [Test]
        public void ResolveSymbol_WhenCalledWithConstant_ShouldCorrectlyParseInstruction()
        {
            var sut = CreateSut();
            sut.ResolveSymbol(0xFE55);
            sut.ByteHigh.Should().Be(0xFE);
            sut.ByteLow.Should().Be(0x55);
        }

        [Test]
        public void RecordSymbolForResolution_WhenCalled_ShouldStoreTheSymbol()
        {
            var expected = "sym";
            var sut = CreateSut();
            sut.TestRecordSymbolForResolution(expected);
            sut.Symbol.Should().Be(expected);
        }
        [Test]
        public void RecordSymbolForResolution_WhenCalled_ShouldStoreTheSymbolLowered()
        {
            var expected = "symbol";
            var sut = CreateSut();
            sut.TestRecordSymbolForResolution(expected.ToUpperInvariant());
            sut.Symbol.Should().Be(expected);
        }

        [Test]
        public void RequresSymbolResolution_WhenSymbolIsSet_ShouldSetRequiresResolution()
        {
            var expected = "sym";
            var sut = CreateSut();
            sut.TestRecordSymbolForResolution(expected);
            sut.RequresSymbolResolution.Should().BeTrue();
        }
        
        [Test]
        public void RequresSymbolResolution_WhenNoSymbolPresent_ShouldBeFalse()
        {
            var sut = CreateSut();
            sut.RequresSymbolResolution.Should().BeFalse();
        }

        [Test]
        public void BytesString_WhenCalled_ShouldOutputInOrderAndHex()
        {
            var sut = CreateSut();
            sut.SetBytes(0x12, 0x34, 0x56, 0x78);
            sut.BytesString().Should().Be("12 34 56 78");
        } 
        
        [Test]
        public void Size_whenRead_ShouldBeFour()
        {
            var sut = CreateSut();
            sut.Size.Should().Be(4);
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
            public byte TestParseRegister(string text) => ParseRegister(text);

            public void TestResolveSymbol(ushort value) => ResolveSymbol(value);
            
            public bool TestIsLabel(string text) => IsLabel(text);

            public void SetBytes(byte opcode, byte register, byte dataHigh, byte dataLow)
            {
                OpCode = opcode;
                Register = register;
                ByteHigh = dataHigh;
                ByteLow = dataLow;

            }
            

            public void TestRecordSymbolForResolution(string symbol) => RecordSymbolForResolution(symbol); 
            
            public void Parse(string source)
            {
                var parts = base.GetOperands("xor",source);
                Left = parts.left;
                Right = parts.right;
            }
        }
    }
}