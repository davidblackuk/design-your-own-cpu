using System.Diagnostics.CodeAnalysis;
using Compiler.Exceptions;
using Compiler.LexicalAnalysis;
using FluentAssertions;
using NUnit.Framework;

namespace CompilerTests.LexicalAnalysis
{
    [ExcludeFromCodeCoverage]
    public class LexerTests
    {

        [Test]
        [TestCase(".", LexemeType.Dot, null)]
        [TestCase(",", LexemeType.Comma, null)]
        [TestCase(";", LexemeType.Semicolon, null)]
        [TestCase("(", LexemeType.LBracket, null)]
        [TestCase(")", LexemeType.RBracket, null)]
        
        [TestCase("+", LexemeType.AddOp, OperationType.Addition)]
        [TestCase("-", LexemeType.AddOp, OperationType.Subtraction)]
        [TestCase("*", LexemeType.MulOp, OperationType.Multiplication)]
        [TestCase("/", LexemeType.MulOp, OperationType.Division)]
        
        [TestCase(":=", LexemeType.Assign, null)]
        
        [TestCase("begin ", LexemeType.Begin, null)]
        [TestCase("end ", LexemeType.End, null)]
        [TestCase("read ", LexemeType.Read, null)]
        [TestCase("write ", LexemeType.Write, null)]
        [TestCase("if ", LexemeType.If, null)]
        [TestCase("then ", LexemeType.Then, null)]
        [TestCase("while ", LexemeType.While, null)]
        [TestCase("do ", LexemeType.Do, null)]
        [TestCase("var ", LexemeType.Var, null)]
        [TestCase("velocity ", LexemeType.Identifier, "velocity")]
        [TestCase("1234 ", LexemeType.Constant, 1234)]

        [TestCase("< ", LexemeType.RelOp, OperationType.LessThan)]
        [TestCase("> ", LexemeType.RelOp, OperationType.GreaterThan)]
        [TestCase("<> ", LexemeType.RelOp, OperationType.NotEqual)]
        [TestCase("= ", LexemeType.RelOp, OperationType.Equal)]
        [TestCase("<= ", LexemeType.RelOp, OperationType.LessThanOrEquals)]
        [TestCase(">= ", LexemeType.RelOp, OperationType.GreaterThanOrEquals)]

        
        public void GetLexeme_WhenCalledWithValidInput_ShouldReturnTheCorrectLexeme(string line, LexemeType expectedType, object expectedValue)
        {
            var sut = CreateSut(line);
            var lexeme = sut.GetLexeme();
            lexeme.Type.Should().Be(expectedType);
            lexeme.Value.Should().BeEquivalentTo(expectedValue);
        }

        [Test]
        [TestCase("begin ", LexemeType.Begin, null)]
        [TestCase("BEGIN ", LexemeType.Begin, null)]
        [TestCase("bEgIn ", LexemeType.Begin, null)]
        [TestCase("VELocitY ", LexemeType.Identifier, "velocity")]
        [TestCase("velocity ", LexemeType.Identifier, "velocity")]
        public void GetLexeme_WhenCalledWithValidInput_ShouldIgnoreCase(string line, LexemeType expectedType, object expectedValue)
        {
            var sut = CreateSut(line);
            var lexeme = sut.GetLexeme();
            lexeme.Type.Should().Be(expectedType);
            lexeme.Value.Should().BeEquivalentTo(expectedValue);
        }

        
        [Test]
        [TestCase("     var {comment}   ", LexemeType.Var, null)]
        [TestCase("var {comment} ", LexemeType.Var, null)]
        [TestCase(" { comment} var ", LexemeType.Var, null)]
        public void GetLexeme_WhenCalledWithValidInput_ShouldIgnoreCommentsAndWhiteSpace(string line, LexemeType expectedType, object expectedValue)
        {
            var sut = CreateSut(line);
            var lexeme = sut.GetLexeme();
            lexeme.Type.Should().Be(expectedType);
            lexeme.Value.Should().BeEquivalentTo(expectedValue);
        }
        
        
        [Test]
        public void GetLexeme_WhenCalledWithAnInvalidAssignment_ShouldThrowACompilerException()
        {
            var sut = CreateSut(":0");
            Assert.Throws<CompilerException>(() => sut.GetLexeme());
        }
        
        [Test]
        public void GetLexeme_WhenCalledWithANumericConstantThatOverflows16Bits_ShouldThrowACompilerException()
        {
            var sut = CreateSut("12345678 ");
            Assert.Throws<CompilerException>(() => sut.GetLexeme());
        }

        private Lexer CreateSut(string line)
        {
            return new Lexer(new InputStreamMock(line), new KeywordLexemeTypeMap());
        }
    }
}