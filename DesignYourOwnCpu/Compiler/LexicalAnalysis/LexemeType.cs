namespace Compiler.LexicalAnalysis;

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