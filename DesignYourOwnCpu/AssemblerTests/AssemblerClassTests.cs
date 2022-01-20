using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Assembler;
using Assembler.Instructions;
using Assembler.LineSources;
using Assembler.Symbols;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Shared;

namespace AssemblerTests;

/// <summary>
///     Not the best class name but avoid conflicts with the name space / project
/// </summary>
[ExcludeFromCodeCoverage]
public class AssemblerClassTests
{
    private Mock<ICodeGenerator> codeGeneratorMock;
    private Mock<ILineSource> lineSourceMock;
    private Mock<IParser> parserMock;

    [SetUp]
    public void SetUp()
    {
        parserMock = new Mock<IParser>();
        codeGeneratorMock = new Mock<ICodeGenerator>();
        lineSourceMock = new Mock<ILineSource>();
    }

    [Test]
    public void Ctor_whenInvokeWithMissingParserDependency_ShouldThrowArgumentNullException()
    {
        parserMock = null;
        Assert.Throws<ArgumentNullException>(() => CreateSut());
    }

    [Test]
    public void Ctor_whenInvokeWithMissingCodeGeneratorDependency_ShouldThrowArgumentNullException()
    {
        codeGeneratorMock = null;
        Assert.Throws<ArgumentNullException>(() => CreateSut());
    }

    [Test]
    public void Ctor_WhenCorrectDependenciesPassed_ShouldInitializeCodeGeneratorProperty()
    {
        var sut = CreateSut();
        sut.CodeGenerator.Should().Be(codeGeneratorMock.Object);
    }

    [Test]
    public void Ctor_WhenCorrectDependenciesPassed_ShouldInitializeRamProperty()
    {
        var ram = new Mock<IRandomAccessMemory>();
        codeGeneratorMock.SetupGet(c => c.Ram).Returns(ram.Object);
        var sut = CreateSut();
        sut.Ram.Should().Be(codeGeneratorMock.Object.Ram);
    }

    [Test]
    public void Ctor_WhenCorrectDependenciesPassed_ShouldInitializeSymbolTableProperty()
    {
        var symbolTable = new Mock<ISymbolTable>();
        codeGeneratorMock.SetupGet(c => c.SymbolTable).Returns(symbolTable.Object);
        var sut = CreateSut();
        sut.SymbolTable.Should().Be(codeGeneratorMock.Object.SymbolTable);
    }

    [Test]
    public void Assemble_WhenInvoked_ShouldPArseAndGenerateCode()
    {
        var instructions = new List<IAssemblerInstruction>();
        parserMock.SetupGet(p => p.Instructions).Returns(instructions);

        var sut = CreateSut();
        sut.Assemble(lineSourceMock.Object);

        parserMock.Verify(pm => pm.ParseAllLines(lineSourceMock.Object), Times.Once);
        codeGeneratorMock.Verify(cg => cg.GenerateCode(instructions), Times.Once);
    }


    private Assembler.Assembler CreateSut()
    {
        // TODO: Refactor me
        return new Assembler.Assembler(parserMock?.Object, codeGeneratorMock?.Object, null, null, null);
    }
}