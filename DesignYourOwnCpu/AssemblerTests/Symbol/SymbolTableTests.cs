using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Assembler.Exceptions;
using Assembler.Symbols;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Symbol;

[ExcludeFromCodeCoverage]
public class SymbolTableTests
{
    [Test]
    public void ReferenceSymbol_WhenSymbolIsCreated_ShouldNotHaveAddress()
    {
        var sut = CreateSut();
        sut.ReferenceSymbol("label");
        var symbol = sut.GetSymbol("label");
        symbol.Address.HasValue.Should().BeFalse();
    }

    [Test]
    public void ReferenceSymbol_WhenCalledWithTheSameName_ShouldNotThrow()
    {
        var sut = CreateSut();
        sut.ReferenceSymbol("label");
        sut.ReferenceSymbol("label");
    }

    [Test]
    public void DefineSymbol_WhenANewSymbolIsCreated_ShouldStoreAddress()
    {
        var sut = CreateSut();
        sut.DefineSymbol("label", 3456);
        var symbol = sut.GetSymbol("label");
        symbol.Address.Value.Should().Be(3456);
    }

    [Test]
    public void DefineSymbol_WhenASymbolIsDefinedMultipleTimes_ShouldThrowDuplicateSymbolException()
    {
        var sut = CreateSut();
        sut.DefineSymbol("label", 3456);
        Assert.Throws<AssemblerException>(() => sut.DefineSymbol("label", 9089));
    }

    [Test] 
    public void SymbolNames_WhenRead_ShouldBeInAlphabeticalOrder()
    {
        var sut = CreateSut();
        sut.DefineSymbol("z", 3456);
        sut.DefineSymbol("a", 3456);
        sut.DefineSymbol("g", 3456);
        sut.SymbolNames.Should().BeInAscendingOrder();
    }

    [Test] 
    public void SymbolTable_whenInitialized_ShouldContainTheSystemSymbols()
    {
        var sut = CreateSut();
        var symbols = sut.SymbolNames.ToArray();
        foreach (var expectedSymbol in InternalSymbols.SystemDefinedSymbols.Keys)
        {
            symbols.Should().Contain(expectedSymbol);
        }
    }

        
    [Test]
    public void GetSymbol_WhenInvokedForANonExistentSymbol_ShouldThrowUndefinedSymbolException()
    {
        var sut = CreateSut();
        Assert.Throws<AssemblerException>(() => sut.GetSymbol("label"));
    }

    private SymbolTable CreateSut()
    {
        return new SymbolTable();
    }
}