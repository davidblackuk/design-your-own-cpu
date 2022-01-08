using System;

namespace Compiler.Exceptions;

public class CompilerException: Exception
{
    public CompilerException(string message)
        : base(message)
    {
            
    }
        
    public CompilerException(string message, int lineNumber, int columnNumber)
        : base($"{message }, line: {lineNumber}, column: {columnNumber}")
    {
            
    }
}