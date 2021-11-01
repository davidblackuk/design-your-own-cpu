using System.Diagnostics.CodeAnalysis;
using Compiler.LexicalAnalysis;

namespace CompilerTests.LexicalAnalysis
{
    [ExcludeFromCodeCoverage]
    public class InputStreamMock: IInputStream
    {
        private readonly string source;
        private int current = 0;
        
        public InputStreamMock(string source)
        {
            this.source = source;
        }


        public char Peek()
        {
            return source[current];
        }

        public char Next()
        {
            var res = Peek();
            current += 1;
            return res;
        }

        public int LineNumber => 1;

        public int ColumnNumber => current;
    }
}