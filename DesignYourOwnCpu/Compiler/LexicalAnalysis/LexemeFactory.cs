namespace Compiler.LexicalAnalysis
{
    internal interface ILexemeFactory
    {
        Lexeme Create(LexemeType type, object value = null);
    }

    internal class LexemeFactory : ILexemeFactory
    {
        private readonly IInputStream inputStream;

        public LexemeFactory(IInputStream inputStream)
        {
            this.inputStream = inputStream;
        }

        public Lexeme Create(LexemeType type, object value = null)
        {
            return new Lexeme(type, value, inputStream.LineNumber);
        }
    }
}