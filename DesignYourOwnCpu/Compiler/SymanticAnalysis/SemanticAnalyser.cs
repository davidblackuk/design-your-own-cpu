using System;
using Compiler.Ast;
using Compiler.Ast.Nodes;
using Compiler.Exceptions;
using Compiler.SyntacticAnalysis;
using Microsoft.Extensions.Logging;

namespace Compiler.SymanticAnalysis;

/// <inheritdoc />
internal class SemanticAnalyser : ISemanticAnalyser
{
    private readonly IAbstractSyntaxTree ast;
    private readonly ISymbolTable symbolTable;
    private readonly ILogger<SemanticAnalyser> logger;

    public SemanticAnalyser(IAbstractSyntaxTree ast, ISymbolTable symbolTable, ILogger<SemanticAnalyser> logger)
    {
        this.ast = ast;
        this.symbolTable = symbolTable;
        this.logger = logger;
    }

    public void Scan()
    {
        Scan(ast.Root);
    }
    
    public void Scan(AstNode node)
    {
        
        switch (node)
        {
            case BlockNode block:
                for (var index = 0; index < block.Nodes.Count; index++)
                {
                    AstNode child = block.Nodes[index];
                    Scan(child);
                }
                break;
            
            case AssignmentNode assign:
                CheckIdentifierExists(assign.Identifier);
                break;
            case IdentifierNode identifier:
                CheckIdentifierExists(identifier);
                break;
            case ConstantNode:
                // can't be an identifier and no recursion
                break;
            case ReadNode:
                //  can't be an identifier and no recursion
                break;
            case WhileNode whileNode:
                Scan(whileNode.Comparison);
                Scan(whileNode.Statements);
                break;
            case PairNode pair:
                Scan(pair.Left);
                Scan(pair.Right);
                break;
            case ExpressionNode expression:
                Scan(expression.Left);
                Scan(expression.Right);
                break;
            case IfNode ifNode:
                Scan(ifNode.Comparison);
                Scan(ifNode.Statements);
                break;
            case WriteNode writeNode:
                Scan(writeNode.Expression);
                break;
            default:
                logger.LogError("Semantic analysis: unknown node type: {NodeType}", node.GetType().Name);
                break;
        }
    }

    private void CheckIdentifierExists(IdentifierNode idNode)
    {
        if (!symbolTable.Exists(idNode.Value))
        {
            logger.LogError("Use of undeclared identifier {Identifier}", idNode.Value);
        }
    }
}