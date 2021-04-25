using Assembler.Exceptions;
using Assembler.Symbols;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.Symbol
{
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
}