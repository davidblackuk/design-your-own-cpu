using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.Symbol
{
    [ExcludeFromCodeCoverage]
    public class SymbolTests
    {
        [Test]
        public void Ctor_whenInvoked_ShoutSetCorrectValues()
        {
            var sut = CreateSut("hello", 234);
            sut.Name.Should().Be("hello");
            sut.Address.Should().Be(234);
        }

        private Assembler.Symbols.Symbol CreateSut(string name, ushort value)
        {
            return new Assembler.Symbols.Symbol(name, value);
        }
    }
}