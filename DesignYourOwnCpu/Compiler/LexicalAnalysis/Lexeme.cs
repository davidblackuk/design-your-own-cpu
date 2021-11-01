using System.Diagnostics.CodeAnalysis;

namespace Compiler.LexicalAnalysis
{
    public enum LexemeType
    {
        Dot,
        Constant,
        Identifier,
        Comma,
        Assign,
        Semicolon,
        LBracket,
        RBracket,
        AddOp,
        MulOp,
        RelOp,
        Begin,
        Read,
        Write,
        If,
        Then,
        While,
        Do,
        Var,
        End,
        Unknown
    }

    public enum OperationType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEquals,
        LessThanOrEquals
    }
    
    
    internal class Lexeme
    {
        public object Value { get; }

        public LexemeType Type { get; }

        public Lexeme(LexemeType type)
        {
            Type = type;
        }

        public Lexeme(LexemeType type, object value): this(type)
        {
            Value = value;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            switch (Type)
            {
                case LexemeType.Constant:
                case LexemeType.Identifier:
                case LexemeType.AddOp:
                case LexemeType.MulOp:
                    return $"[{Type} = {Value}]";
                    break;
                default:
                    return $"[{Type}]";
            }            
        }
    }
}