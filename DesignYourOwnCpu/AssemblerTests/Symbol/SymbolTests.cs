using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.Symbol
{
    [ExcludeFromCodeCoverage]
    public class SymbolTests
    {
        [Test]
        public void Ctor_WhenInvoked_SholudSetCorrectValues()
        {
            var sut = CreateSut("hello", 234);
            sut.Name.Should().Be("hello");
            sut.Address.Should().Be(234);
        }

        [Test]
        public void ToString_WhenInvoked_ShouShouldDisplayCorrectly()
        {
            var sut = CreateSut("hello", 0x1234);
            var res = sut.ToString();
            res.Should().Be("0x1234 hello");
        }
        
        
        private Assembler.Symbols.Symbol CreateSut(string name, ushort value)
        {
            return new Assembler.Symbols.Symbol(name, value);
        }
    }
}