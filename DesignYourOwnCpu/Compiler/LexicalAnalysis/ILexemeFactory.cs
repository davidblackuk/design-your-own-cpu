namespace Compiler.LexicalAnalysis
{
    internal interface ILexemeFactory
    {
        Lexeme Create(LexemeType type, object value = null);
    }
}