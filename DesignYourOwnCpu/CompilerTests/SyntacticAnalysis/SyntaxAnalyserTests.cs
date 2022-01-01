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
        // var id1, id2.
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.Var))
            .Returns(new Lexeme(LexemeType.Identifier, "id1"))
            .Returns(new Lexeme(LexemeType.Comma))
            .Returns(new Lexeme(LexemeType.Identifier, "id2"))
            .Returns(new Lexeme(LexemeType.Dot))
            ;
        var sut = CreateSut();
        sut.Scan();
        
        symbolTableMock.Verify(st => st.Declare("id1"));
        symbolTableMock.Verify(st => st.Declare("id2"));
    }
 
    [Test]
    public void Assignment_WhenProvidedConstantValue_ShouldStoreIdentifierAndValue()
    {
        // id1 := 22.
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.Identifier, "id1"))
            .Returns(new Lexeme(LexemeType.Assign))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)22))
            .Returns(new Lexeme(LexemeType.Dot))
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
    public void Assignment_WhenReadFunctionUsed_ShouldStoreReadNodeReference()
    {
        // id1 := 22.
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.Identifier, "id1"))
            .Returns(new Lexeme(LexemeType.Assign))
            .Returns(new Lexeme(LexemeType.Read))
            .Returns(new Lexeme(LexemeType.Dot))
            ;
        var sut = CreateSut();
        sut.Scan();
        ast.Root.As<BlockNode>().Nodes.First().Should().BeOfType<AssignmentNode>();
        
        var assignmentNode = ast.Root.As<BlockNode>().Nodes.First().As<AssignmentNode>();
        assignmentNode.Identifier.Value.Should().Be("id1");
        assignmentNode.Expression.Should().BeOfType<ReadNode>();
    }
    
    [Test]
    public void Expressions_WhenParsed_ShouldHandleMultipleTerms()
    {
        // id1 := 12 + 43 * 7.  { Should be same as 12 + ( 43 * 7) }
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.Identifier, "id1"))
            .Returns(new Lexeme(LexemeType.Assign))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)12))
            .Returns(new Lexeme(LexemeType.AddOp, "+"))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)43))
            .Returns(new Lexeme(LexemeType.MulOp, "*"))
                        
            .Returns(new Lexeme(LexemeType.Constant, (ushort)7))
            .Returns(new Lexeme(LexemeType.Dot))
            ;
        var sut = CreateSut();
        sut.Scan();

        
        var assignmentNode = ast.Root.As<BlockNode>().Nodes.First().As<AssignmentNode>();
        var firstExpression = assignmentNode.Expression.As<ExpressionNode>();
        firstExpression.Operator.Should().Be("+");
        firstExpression.Left.As<ConstantNode>().Value.Should().Be((ushort)12);

        var secondExpression = firstExpression.Right.As<PairNode>();
        secondExpression.Operator.Should().Be("*");
        secondExpression.Left.As<ConstantNode>().Value.Should().Be((ushort)43);
        secondExpression.Right.As<ConstantNode>().Value.Should().Be((ushort)7);

    }
    
    [Test]
    public void Expressions_WhenWithParenthesis_ShouldOverrideBodmas()
    {
        // id1 := (12 + 43) * 7. { overrides order of evaluation }
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.Identifier, "id1"))
            .Returns(new Lexeme(LexemeType.Assign))
            .Returns(new Lexeme(LexemeType.LBracket))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)12))
            .Returns(new Lexeme(LexemeType.AddOp, "+"))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)43))
            .Returns(new Lexeme(LexemeType.RBracket))

            .Returns(new Lexeme(LexemeType.MulOp, "*"))
                        
            .Returns(new Lexeme(LexemeType.Constant, (ushort)7))
            .Returns(new Lexeme(LexemeType.Dot))
            ;
        var sut = CreateSut();
        sut.Scan();

        
        var assignmentNode = ast.Root.As<BlockNode>().Nodes.First().As<AssignmentNode>();
        var firstExpression = assignmentNode.Expression.As<PairNode>();
        firstExpression.Operator.Should().Be("*");


        var secondExpression = firstExpression.Left.As<ExpressionNode>();
        secondExpression.Operator.Should().Be("+");
        secondExpression.Left.As<ConstantNode>().Value.Should().Be((ushort)12);
        secondExpression.Right.As<ConstantNode>().Value.Should().Be((ushort)43);
        
        firstExpression.Right.As<ConstantNode>().Value.Should().Be((ushort)7);
        
    }
    
    
    [Test]
    public void Write_WhenUsedWithConstantValue_ShouldStoreValue()
    {
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.Write))
            .Returns(new Lexeme(LexemeType.LBracket))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)22))
            .Returns(new Lexeme(LexemeType.RBracket))
            .Returns(new Lexeme(LexemeType.Dot))
            ;
        var sut = CreateSut();
        sut.Scan();
        ast.Root.As<BlockNode>().Nodes.First().Should().BeOfType<WriteNode>();
        var writeNode = ast.Root.As<BlockNode>().Nodes.First().As<WriteNode>();
        writeNode.Expression.As<ConstantNode>().Value.Should().Be(22);
    }
    
    [Test]
    public void If_WhenScanned_ShouldParseAndStoreTheComparisonAndStatements()
    {
        // if (id1 > 22) then begin end.
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.If))
            .Returns(new Lexeme(LexemeType.LBracket))
            .Returns(new Lexeme(LexemeType.Identifier, "id1", 0))
            .Returns(new Lexeme(LexemeType.RelOp, ">", 0))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)22, 0))
            .Returns(new Lexeme(LexemeType.RBracket))
            .Returns(new Lexeme(LexemeType.Then))
            .Returns(new Lexeme(LexemeType.Begin))

            .Returns(new Lexeme(LexemeType.Write))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)22))
            
            .Returns(new Lexeme(LexemeType.End))
            .Returns(new Lexeme(LexemeType.Dot))
            ;
        var sut = CreateSut();
        sut.Scan();
        
        ast.Root.As<BlockNode>().Nodes.First().Should().BeOfType<IfNode>();
        var ifNode = ast.Root.As<BlockNode>().Nodes.First().As<IfNode>();
        ifNode.Comparison.Operator.Should().Be(">");
        ifNode.Comparison.Left.Should().BeOfType<IdentifierNode>();
        ifNode.Comparison.Left.As<IdentifierNode>().Value.Should().Be("id1");
        ifNode.Comparison.Right.Should().BeOfType<ConstantNode>();
        ifNode.Comparison.Right.As<ConstantNode>().Value.Should().Be((ushort)22);

        ifNode.Statements.Should().BeOfType<BlockNode>();
        ifNode.Statements.As<BlockNode>().Nodes.First().Should().BeOfType<WriteNode>();

    }
    
    [Test]
    public void While_WhenScanned_ShouldParseAndStoreTheComparisonAndStatements()
    {
        // if (id1 > 22) then begin end.
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.While))
            .Returns(new Lexeme(LexemeType.LBracket))
            .Returns(new Lexeme(LexemeType.Identifier, "id1", 0))
            .Returns(new Lexeme(LexemeType.RelOp, ">", 0))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)22, 0))
            .Returns(new Lexeme(LexemeType.RBracket))
            .Returns(new Lexeme(LexemeType.Then))
            .Returns(new Lexeme(LexemeType.Begin))

            .Returns(new Lexeme(LexemeType.Write))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)22))
            
            .Returns(new Lexeme(LexemeType.End))
            .Returns(new Lexeme(LexemeType.Dot))
            ;
        var sut = CreateSut();
        sut.Scan();
        
        ast.Root.As<BlockNode>().Nodes.First().Should().BeOfType<WhileNode>();
        var whileNode = ast.Root.As<BlockNode>().Nodes.First().As<WhileNode>();
        whileNode.Comparison.Operator.Should().Be(">");
        whileNode.Comparison.Left.Should().BeOfType<IdentifierNode>();
        whileNode.Comparison.Left.As<IdentifierNode>().Value.Should().Be("id1");
        whileNode.Comparison.Right.Should().BeOfType<ConstantNode>();
        whileNode.Comparison.Right.As<ConstantNode>().Value.Should().Be((ushort)22);

        whileNode.Statements.Should().BeOfType<BlockNode>();
        whileNode.Statements.As<BlockNode>().Nodes.First().Should().BeOfType<WriteNode>();

    }
    
    [Test]
    public void Block_WhenScanned_ShouldParseAndStoreMultipleInstructions()
    {
        // if (id1 > 22) then begin end.
        lexerMock.SetupSequence(s => s.GetLexeme())
            .Returns(new Lexeme(LexemeType.Begin))

            .Returns(new Lexeme(LexemeType.Write))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)22))
            .Returns(new Lexeme(LexemeType.Semicolon))

            .Returns(new Lexeme(LexemeType.Write))
            .Returns(new Lexeme(LexemeType.Constant, (ushort)34))

            .Returns(new Lexeme(LexemeType.End))
            .Returns(new Lexeme(LexemeType.Dot))
            ;
        var sut = CreateSut();
        sut.Scan();

        var block = ast.Root.As<BlockNode>();
        block.Nodes.First().Should().BeOfType<WriteNode>();
        block.Nodes.First().As<WriteNode>().Expression.As<ConstantNode>().Value.Should().Be(22);
        block.Nodes.Skip(1).Take(1).First().Should().BeOfType<WriteNode>();
        block.Nodes.Skip(1).Take(1).First().As<WriteNode>().Expression.As<ConstantNode>().Value.Should().Be(34);

    }
    

    private SyntaxAnalyser CreateSut()
    {
        return new(lexerMock?.Object, ast, symbolTableMock?.Object, loggerMock?.Object);
    }
}