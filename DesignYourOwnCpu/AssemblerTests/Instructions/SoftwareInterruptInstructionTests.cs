using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions;

[ExcludeFromCodeCoverage]
public class SoftwareInterruptInstructionTests
{
    [Test]
    public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode()
    {
        var sut = CreateSut();
        sut.OpCode.Should().Be(OpCodes.Swi);
    }

    private SoftwareInterruptInstruction CreateSut()
    {
        return new SoftwareInterruptInstruction();
    }
}