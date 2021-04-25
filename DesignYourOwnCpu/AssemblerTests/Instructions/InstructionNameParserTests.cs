using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.Instructions
{
    public class InstructionNameParserTests
    {

        [Test]
        [TestCase("HALT","halt", "")]
        [TestCase("  NOP ","nop", "")]
        [TestCase("  ld     R1, 0    ","ld", "R1, 0")]
        [TestCase("BLt lAbel","blt", "lAbel")]
        public void GetInstruction_WhenInvokedWithValidLine_ShouldReturnCorrectInstruction(string line, string expectedInstruction, string expectedRemainder)
        {
            var sut = CreateSut();
            var res = sut.Parse(line);
            res.instruction.Should().Be(expectedInstruction);
            res.remainder.Should().Be(expectedRemainder);
        }

        private InstructionNameParser CreateSut()
        {
            return new InstructionNameParser();
        }
    }
}