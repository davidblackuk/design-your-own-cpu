using Compiler.LexicalAnalysis;
using Compiler.SyntacticAnalysis;
using FluentAssertions;
using NUnit.Framework;

namespace CompilerTests.SyntacticAnalysis
{
    internal class LexemeSetTests
    {
        [Test]
        public void Set_WhenCreatedWithNoItems_ShouldReturnEmptyAsTrue()
        {
            var sut = CreateSut();
            sut.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void Set_WhenCreatedWithItems_ShouldReturnEmptyAsFalse()
        {
            var sut = CreateSut(LexemeType.Do);
            sut.IsEmpty.Should().BeFalse();
        }


        private LexemeSet CreateSut(params LexemeType[] args)
        {
            return LexemeSet.From(args);
        }
    }
}
