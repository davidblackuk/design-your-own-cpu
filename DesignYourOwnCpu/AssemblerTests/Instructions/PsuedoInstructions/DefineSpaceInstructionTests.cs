using System;
using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions.PsuedoInstructions;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions.PsuedoInstructions;

[ExcludeFromCodeCoverage]
public class DefineSpaceInstructionTests
{
    [Test]
    public void Parse_WhenCalled_ShouldThrowErrorForNnnNumericLengthValues()
    {
        var sut = CreateSut();
        Assert.Throws<FormatException>(() => sut.Parse("Hello"));
    }


    [Test]
    public void Parse_WhenCalled_ShouldCorrectlyParseInstruction()
    {
        var sut = CreateSut();
        sut.Parse("0x1267");
        sut.ByteHigh.Should().Be(0x12);
        sut.ByteLow.Should().Be(0x67);
    }

    [Test]
    public void Parse_WhenCalled_ShouldCorrectlySetTheSize()
    {
        var sut = CreateSut();
        sut.Parse("0x1267");
        sut.Size.Should().Be(0x1267);
    }
        
    [Test]
    public void WriteBytes_WhenCalled_ShouldZeroOutTHeRamAtTHeSpecifiedLocation()
    {
        const ushort startAddress = 0x123;
        var ramMock = new Mock<IRandomAccessMemory>();
        var sut = CreateSut();
        sut.Parse("0x4");
        sut.WriteBytes(ramMock.Object, startAddress);
            
        ramMock.VerifySet(r => r[startAddress] = 0);
        ramMock.VerifySet(r => r[startAddress + 1] = 0);
        ramMock.VerifySet(r => r[startAddress + 2] = 0);
        ramMock.VerifySet(r => r[startAddress + 3] = 0);
    }


    private DefineSpaceInstruction CreateSut()
    {
        return new DefineSpaceInstruction();
    }
}