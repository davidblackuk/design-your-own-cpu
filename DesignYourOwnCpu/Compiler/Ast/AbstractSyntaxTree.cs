using System;
using System.Drawing;
using Pastel;

namespace Compiler.Ast
{
    internal class AbstractSyntaxTree : IAbstractSyntaxTree
    {
        public BlockNode Root { get; set; }

        public void Dump()
        {
            Dump(Root, 0);
        }

        private void Dump(AstNode node, int level)
        {
            switch (node)
            {
                case BlockNode block:
                    WriteLineLevel(level, "BEGIN");
                    foreach (AstNode child in block.Nodes)
                    {
                        Dump(child, level + 1);
                    }
                    WriteLineLevel(level, "END");
                    break;
                case AssignmentNode assign:
                    WriteLineLevel(level + 1, $"{assign.Identifier}");
                    WriteLineLevel(level, ":=");
                    Dump(assign.Expression, level );
                    break;
                case PairNode pair:
                    Dump(pair.Left, level + 1);
                    WriteLineLevel(level+1, Map(pair.Operator));
                    Dump(pair.Right, level + 1);
                    break;
                case ConstantNode constant:
                    WriteLineLevel(level+1, constant.Value.ToString());
                    break;
                case IdentifierNode identifier:
                    WriteLineLevel(level+1, identifier.Value);
                    break;
                case ReadNode:
                    WriteLineLevel(level+1, "READ()");
                    break;
                case IfNode ifNode:
                    Console.WriteLine();
                    Dump(ifNode.Comparison, level+1);
                    WriteLineLevel(level, "IF");
                    Dump(ifNode.Statements, level +1);
                    break;
                case WhileNode whileNode:
                    Console.WriteLine();
                    Dump(whileNode.Comparison, level+1);
                    WriteLineLevel(level, "WHILE");
                    Dump(whileNode.Statements, level +1);
                    break;
                case WriteNode writeNode:
                    Console.WriteLine();
                    WriteLineLevel(level, "WRITE");
                    Dump(writeNode.Expression, level + 1);
                    break;
                
                default:
                    Console.WriteLine("ARRGH WHATS THIS!!  > " + node.GetType().Name);
                    break;
                
            }
            
        }

        private Color[] Colors = new[] { Color.Aqua, Color.Brown, Color.Gold, Color.Fuchsia, Color.Orange };
        
        private void WriteLineLevel(int level, string message)
        {
                Console.Write(" ".PadLeft(level*2, ' '));
            
            
            Console.WriteLine(""+message.Pastel(Colors[level % Colors.Length]));
        }

        private string Map(string from)
        {
            switch (from)
            {
                case "Addition": return "+";
                case "Subtraction": return "-";
                case "Multiplication": return "*";
                case "Division": return "/";
                default: return from;
            }
        }
        
    }
}