using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.Instructions;

[ExcludeFromCodeCoverage]
public class StoreLowInstructionTests
{
    [Test]
    [TestCase("r4, (0x1234)", 0x11, 4, 0x12, 0x34)]
    [TestCase("r5, (r3)", 0x14, 5, 0x0, 0x3)]
    public void Parse_whenCalled_ShouldCorrectlyParseInstruction(
        string line, byte opcode, byte register, byte dataHigh, byte dataLow
    )
    {
        var sut = CreateSut();
        sut.Parse(line);
        sut.Register.Should().Be(register);
        sut.OpCode.Should().Be(opcode);
        sut.ByteHigh.Should().Be(dataHigh);
        sut.ByteLow.Should().Be(dataLow);
    }


    private StoreLowInstruction CreateSut()
    {
        return new StoreLowInstruction();
    }
}