using System.Collections.Generic;

namespace Compiler.LexicalAnalysis
{
    internal interface IKeywordLexemeTypeMap
    {
        LexemeType MapsToLexeme(string from);
    }

    internal class KeywordLexemeTypeMap : IKeywordLexemeTypeMap
    {
        private Dictionary<string, LexemeType> map = new()
        {
            ["."] = LexemeType.Dot,
            [","] = LexemeType.Comma,
            [":="] = LexemeType.Assign,
            [";"] = LexemeType.Semicolon,
            ["("] = LexemeType.LBracket,
            [")"] = LexemeType.RBracket,
            ["+"] = LexemeType.AddOp,
            ["-"] = LexemeType.AddOp,
            ["*"] = LexemeType.MulOp,
            ["/"] = LexemeType.MulOp,
            ["<"] = LexemeType.RelOp,
            ["<="] = LexemeType.RelOp,
            [">"] = LexemeType.RelOp,
            [">="] = LexemeType.RelOp,
            
            ["begin"] = LexemeType.Begin,
            ["end"] = LexemeType.End,
            ["read"] = LexemeType.Read,
            ["write"] = LexemeType.Write,
            ["if"] = LexemeType.If,
            ["then"] = LexemeType.Then,
            ["while"] = LexemeType.While,
            ["do"] = LexemeType.Do,
            ["var"] = LexemeType.Var,
            ["end"] = LexemeType.End,
            
        };

        public LexemeType MapsToLexeme(string from)
        {
            if (map.ContainsKey(from))
            {
                return map[from];
            }
            return LexemeType.Unknown;
        }
        
    }
}