using Compiler.LexicalAnalysis;
using FluentAssertions;
using NUnit.Framework;

namespace CompilerTests.LexicalAnalysis
{
    public class LexemeTests
    {

        [Test]
        public void Ctor_WhenCalled_ShouldInitializeAllFields()
        {
            LexemeType expectedType = LexemeType.Do;
            object expectedValue = "Hello";
            int expectedLine = 9;

            var sut = CreateSut(expectedType, expectedValue, expectedLine);

            sut.Type.Should().Be(expectedType);
            sut.Value.Should().BeSameAs(expectedValue);
            sut.LineNumber.Should().Be(expectedLine);

        }


        private Lexeme CreateSut(LexemeType type, object value, int line)
        {
            return new Lexeme(type, value, line);
        }
        
    }
}