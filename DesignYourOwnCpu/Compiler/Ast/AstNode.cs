using System.Collections.Generic;

namespace Compiler.Ast
{
    internal class AstNode
    {
        public List<AstNode> Nodes = new List<AstNode>();

        public T Add<T>(T node) where T : AstNode
        {
            Nodes.Add(node);
            return node;
        }
    }

    internal class ProgramNode : AstNode
    {
        
    }
    
    internal class AssignmentNode : AstNode
    {
        public string Identifier { get; set; }
        public AstNode Expression = new ExpressionNode();
       
    }
    
    internal class WriteNode : AstNode
    {
        public AstNode Expression { get; set; }
    }

    internal class IfNode : AstNode
    {
        public PairNode Comparison { get; set; }
        public BlockNode Statements { get; } = new();

    } 
    
    internal class WhileNode : AstNode
    {
        public PairNode Comparison { get; set; }
        public BlockNode Statements { get; } = new();
    } 
    
    internal class ComparisonNode : AstNode
    {
        public string Operation { get; set;  }
        public ExpressionNode Left { get; set; }
        public ExpressionNode Right { get; set; }
    } 
    
    internal class ExpressionNode : AstNode
    {
        // operator
        private ExpressionNode Left { get; set; }
        private ExpressionNode Right { get; set; }
    }

    internal class BlockNode : AstNode
    {
        
    }

    internal class ReadNode : AstNode
    {
        
    }
    
    internal class ConstantNode : AstNode
    {
        private readonly ushort value;

        public ConstantNode(ushort value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
    
    internal class IdentifierNode : AstNode
    {
        private readonly string value;

        public IdentifierNode(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
    internal class PairNode : AstNode
    {
        public string Operator { get; set; }

        public AstNode Left { get; set; }
        public AstNode Right { get; set; }

        public override string ToString()
        {
            return $"{Operator}";
        }
    }
}