namespace Compiler.LexicalAnalysis;

internal interface IKeywordLexemeTypeMap
{
    LexemeType MapsToLexeme(string from);
}