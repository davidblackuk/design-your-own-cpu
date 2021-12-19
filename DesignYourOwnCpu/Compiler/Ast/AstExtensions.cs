using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using Compiler.Ast.Nodes;
using Pastel;

namespace Compiler.Ast;

internal static class AstExtensions
{

    
    public static void Display(this IAbstractSyntaxTree tree)
    {
        Display(tree.Root, "", true);
        Console.WriteLine();
        
    }

    private static void Display(AstNode node, string indent, bool last)
    {
        WriteTreeStructure(indent);
        if (last)
        {
            WriteTreeStructure(" └─");
            indent += "   ";
        }
        else
        {
            WriteTreeStructure(" ├─");
            indent += " │ ";
        }
        
        switch (node)
        {
            case BlockNode block:
                WriteLineReservedWord(" BLOCK");
                for (var index = 0; index < block.Nodes.Count; index++)
                {
                    AstNode child = block.Nodes[index];
                    Display(child, indent, index == block.Nodes.Count - 1);
                }

                break;
            case AssignmentNode assign:
                WriteLineReservedWord(" :=");
                Display(assign.Identifier, indent, false);
                Display(assign.Expression, indent, true);
                break;
            case ConstantNode constant:
                Console.WriteLine($" {constant.Value}");
                break;
            case IdentifierNode identifier:
                Console.WriteLine($" {identifier.Value}");
                break;
            case ReadNode:
                WriteLineFunction(" READ");
                break;

            case WhileNode whileNode:
                WriteLineReservedWord(" WHILE");
                Display(whileNode.Comparison, indent, false);
                Display(whileNode.Statements, indent, true);
                break;
            case PairNode pair:
                Console.WriteLine(" " + pair.Operator);
                Display(pair.Left, indent, false);
                Display(pair.Right, indent, true);
                break;
            case ExpressionNode expression:
                Console.WriteLine(" " + expression.Operator);
                Display(expression.Left, indent, false);
                Display(expression.Right, indent, true);
                break;
            case IfNode ifNode:
                WriteLineReservedWord(" IF");
                Display(ifNode.Comparison, indent, false);
                Display(ifNode.Statements, indent, true);
                break;
            case WriteNode writeNode:
                WriteLineFunction(" WRITE");
                Display(writeNode.Expression, indent, true);
                break;

            default:
                Console.WriteLine("ARRGH WHATS THIS!!  > " + node.GetType().Name);
                break;
        }
    }

    private static void WriteLineReservedWord(string text)
    {
        Console.WriteLine(text.Pastel(Color.Goldenrod));
    }

    private static void WriteLineFunction(string text)
    {
        Console.WriteLine(text.Pastel(Color.Aqua));
    }
    
    private static void WriteTreeStructure(string text)
    {
        Console.Write(text.Pastel(Color.Plum));
    }
}