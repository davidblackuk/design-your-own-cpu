using System.Diagnostics.CodeAnalysis;

namespace Compiler.LexicalAnalysis
{
    internal class Lexeme
    {
        public object Value { get; }

        public int LineNumber { get; }

        public LexemeType Type { get; }
        
        public Lexeme(LexemeType type, object value, int lineNumber)
        {
            Type = type;
            LineNumber = lineNumber;
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
                default:
                    return $"[{Type}]";
            }            
        }
    }
}