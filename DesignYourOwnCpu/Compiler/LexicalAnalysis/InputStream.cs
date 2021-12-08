using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Compiler.Exceptions;

namespace Compiler.LexicalAnalysis
{
    [ExcludeFromCodeCoverage]  // we don't do integration tests
    internal class InputStream : IInputStream
    {
        private readonly string inputFilename;
        private readonly Lazy<StreamReader> lazyStreamReader;

        private string currentLine;
        private bool endOfFile;
        
        public InputStream(string inputFilename)
        {
            this.inputFilename = inputFilename;
            lazyStreamReader = new Lazy<StreamReader>(OpenInputStream);
        }

        /// <summary>
        /// Open the file or we die trying.
        /// </summary>
        private StreamReader OpenInputStream()
        {
            try
            {
                return File.OpenText(inputFilename);
            }
            catch (Exception)
            {
                throw new CompilerException($"Could not open input file: {inputFilename}");
            } 
        }

        /// <summary>
        /// Returns the next character in the input stream, with out advancing the current position
        /// </summary>
        public char Peek()
        {
            if (IsNewFile || IsEndOfLine)
            {
                ReadLine();
            }

            return currentLine[ColumnNumber];
        }

        /// <summary>
        /// Gets the next character in the input stream and advances the input stream one character.
        /// </summary>
        public char Next()
        {
            var res  = Peek();
            ColumnNumber += 1;
            return res;
        }
        
        /// <summary>
        /// Gets the current line number for error reporting
        /// </summary>        
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets the current column number for error reporting
        /// </summary>
        public int ColumnNumber { get; set; }

        private void ReadLine()
        {
            currentLine = lazyStreamReader.Value.ReadLine();
            endOfFile = (currentLine == null);
            if (endOfFile)
            {
                throw new CompilerException("Unexpected end of file", LineNumber, ColumnNumber);
            }

            // pad both sides space so that a new line in source becomes a lexeme break;
            currentLine = " " + currentLine + " ";
            LineNumber += 1;
            ColumnNumber = 0;
        }

        /// <summary>
        /// Are we in the initial starting position
        /// </summary>
        private bool IsNewFile => (currentLine == null && !endOfFile);
        
        /// <summary>
        /// Are we at the end of the line and need to read the next?
        /// </summary>
        private bool IsEndOfLine => (ColumnNumber == currentLine.Length);
    }
}