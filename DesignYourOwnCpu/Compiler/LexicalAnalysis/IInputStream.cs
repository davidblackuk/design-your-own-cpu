namespace Compiler.LexicalAnalysis;

internal interface IInputStream
{
    /// <summary>
    /// Returns the next character in the input stream, with out advancing the current position
    /// </summary>
    char Peek();

    /// <summary>
    /// Gets the next character in the input stream and advances the input stream one character.
    /// </summary>
    char Next();

    /// <summary>
    /// Gets the current line number for error reporting
    /// </summary>
    int LineNumber { get; }

    /// <summary>
    /// Gets the current column number for error reporting
    /// </summary>
    int ColumnNumber { get; }
}