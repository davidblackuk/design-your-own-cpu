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

        [Test]
        public void SubtractOperator_WhenUsedWithASingleType_ShouldRemoveIt()
        {
            var sut = CreateSut(LexemeType.Do, LexemeType.Assign, LexemeType.Comma);
            var res = sut - LexemeType.Assign;
            res.Contains(LexemeType.Assign).Should().BeFalse();
            res.Contains(LexemeType.Do).Should().BeTrue();
            res.Contains(LexemeType.Comma).Should().BeTrue();
        }

        [Test]
        public void SubtractOperator_WhenUsedWithALexemeSet_ShouldRemoveIt()
        {
            var sut = CreateSut(LexemeType.Do, LexemeType.Assign, LexemeType.Comma);
            var res = sut - LexemeSet.From(LexemeType.Assign, LexemeType.Comma);
            res.Contains(LexemeType.Do).Should().BeTrue();
            res.Contains(LexemeType.Assign).Should().BeFalse();
            res.Contains(LexemeType.Comma).Should().BeFalse();
        }
        
        
        private LexemeSet CreateSut(params LexemeType[] args)
        {
            return LexemeSet.From(args);
        }
    }
}
