using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions;

[ExcludeFromCodeCoverage]
public class NopInstructionTests
{
    [Test]
    public void Parse_WhenInvoked_ShouldSetValuesCorrectly()
    {
        var sut = CreateSut();
        sut.Parse("");
        sut.OpCode.Should().Be(OpCodes.Nop);
        sut.Register.Should().Be(0);
        sut.ByteHigh.Should().Be(0);
        sut.ByteLow.Should().Be(0);
    }

    private NopInstruction CreateSut()
    {
        return new NopInstruction();
    }
}