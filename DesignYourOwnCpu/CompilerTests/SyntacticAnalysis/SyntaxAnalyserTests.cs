using System.Linq;
using Compiler.Ast;
using Compiler.Ast.Nodes;
using Compiler.LexicalAnalysis;
using Compiler.SyntacticAnalysis;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CompilerTests.SyntacticAnalysis;

public class SyntaxAnalyserTests
{
    private Mock<ILexer> lexerMock; 
    private AbstractSyntaxTree ast;
    private Mock<ISymbolTable> symbolTableMock; 
    private Mock<ILogger<SyntaxAnalyser>> loggerMock;

    [SetUp]
    public void SetUp()
    {
        lexerMock = new Mock<ILexer>();
        ast = new AbstractSyntaxTree();
        symbolTableMock = new Mock<ISymbolTable>();
        loggerMock = new Mock<ILogger<SyntaxAnalyser>>();
    }

    [Test]
    public void Identifiers_WhenDeclared_ShouldBeAddedToTheSymbolTable()
    {
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.Var, null, 0))
            .Returns(new Lexeme(LexemeType.Identifier, "id1", 0))
            .Returns(new Lexeme(LexemeType.Comma, null, 0))
            .Returns(new Lexeme(LexemeType.Identifier, "id2", 0))
            .Returns(new Lexeme(LexemeType.Semicolon, null, 0))
            .Returns(new Lexeme(LexemeType.Begin, null, 0))
            .Returns(new Lexeme(LexemeType.End, null, 0))
            .Returns(new Lexeme(LexemeType.Dot, null, 0))
            ;
        var sut = CreateSut();
        sut.Scan();
        
        symbolTableMock.Verify(st => st.Declare("id1"));
        symbolTableMock.Verify(st => st.Declare("id2"));
    }
 
    [Test]
    public void Assignment_WhenProvidedConstantValue_ShouldStoreIdentifierAndValue()
    {
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.Begin, null, 0))
            .Returns(new Lexeme(LexemeType.Identifier, "id1", 0))
            .Returns(new Lexeme(LexemeType.Assign, null, 0))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)22, 0))
            .Returns(new Lexeme(LexemeType.Semicolon, null, 0))
            .Returns(new Lexeme(LexemeType.End, null, 0))
            .Returns(new Lexeme(LexemeType.Dot, null, 0))
            ;
        var sut = CreateSut();
        sut.Scan();
        ast.Root.As<BlockNode>().Nodes.First().Should().BeOfType<AssignmentNode>();
        
        var assignmentNode = ast.Root.As<BlockNode>().Nodes.First().As<AssignmentNode>();
        assignmentNode.Identifier.Value.Should().Be("id1");
        assignmentNode.Expression.Should().BeOfType<ConstantNode>();
        assignmentNode.Expression.As<ConstantNode>().Value.Should().Be(22);
        

    }
    
    
    [Test]
    public void Write_WhenUsedWithConstantValue_ShouldStoreValue()
    {
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.Begin, null, 0))
            .Returns(new Lexeme(LexemeType.Write, "id1", 0))
            .Returns(new Lexeme(LexemeType.LBracket, "id1", 0))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)22, 0))
            .Returns(new Lexeme(LexemeType.RBracket, null, 0))
            .Returns(new Lexeme(LexemeType.Semicolon, null, 0))
            .Returns(new Lexeme(LexemeType.End, null, 0))
            .Returns(new Lexeme(LexemeType.Dot, null, 0))
            ;
        var sut = CreateSut();
        sut.Scan();
        ast.Root.As<BlockNode>().Nodes.First().Should().BeOfType<WriteNode>();
        var writeNode = ast.Root.As<BlockNode>().Nodes.First().As<WriteNode>();
        writeNode.Expression.As<ConstantNode>().Value.Should().Be(22);
    }
    

    private SyntaxAnalyser CreateSut()
    {
        return new(lexerMock?.Object, ast, symbolTableMock?.Object, loggerMock?.Object);
    }
}