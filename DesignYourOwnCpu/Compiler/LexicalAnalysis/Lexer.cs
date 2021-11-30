using System;
using System.Text;
using Compiler.Exceptions;

namespace Compiler.LexicalAnalysis
{
    internal interface ILexer
    {
        Lexeme GetLexeme();
    }

    internal class Lexer : ILexer
    {
       
        
        private readonly IInputStream inputStream;
        private readonly IKeywordLexemeTypeMap lexemeMapper;
        private readonly ILexemeFactory lexemeFactory;

        public Lexer(IInputStream inputStream, IKeywordLexemeTypeMap lexemeMapper, ILexemeFactory lexemeFactory)
        {
            this.inputStream = inputStream;
            this.lexemeMapper = lexemeMapper;
            this.lexemeFactory = lexemeFactory;
        }
        
        public Lexeme GetLexeme()
        {
            char current;
            
            // strip white space and comments
            SkipWhiteSpaceAndComments();

            current = inputStream.Next();
            if (Char.IsDigit(current))
            {
                return RecognizeNumericConstant(current);
            }
            
            else if (Char.IsLetter(current))
            {
                return RecognizeIdentifierOrKeyWord(current);
            }
            else if (IsSymbol(current))
            {
                return RecognizeOperator(current);
            }
            
            return lexemeFactory.Create(LexemeType.Unknown);
        }

        private Lexeme RecognizeOperator(char current)
        {
            if (".,();".Contains(current))
            {
                return lexemeFactory.Create(lexemeMapper.MapsToLexeme(""+current));
            }
            if ("+-*/".Contains(current))
            {
                switch (current)
                {
                    case '+': return lexemeFactory.Create(LexemeType.AddOp, OperationType.Addition);
                    case '-': return lexemeFactory.Create(LexemeType.AddOp, OperationType.Subtraction);
                    case '/': return lexemeFactory.Create(LexemeType.MulOp, OperationType.Division);
                    default:  return lexemeFactory.Create(LexemeType.MulOp, OperationType.Multiplication);
                }
            }

            if (current == ':')
            {
                if (inputStream.Peek() == '=')
                {
                    inputStream.Next();
                    return lexemeFactory.Create(LexemeType.Assign);
                }
                else
                {
                    throw new CompilerException(": with out matching =");
                }
            }

            if (current == '<')
            {
                char next = inputStream.Peek();
                if (next == '=')
                {
                    inputStream.Next();
                    return lexemeFactory.Create(LexemeType.RelOp, OperationType.LessThanOrEquals);
                }
                if (next == '>')
                {
                    inputStream.Next();
                    return lexemeFactory.Create(LexemeType.RelOp, OperationType.NotEqual);
                }
                return lexemeFactory.Create(LexemeType.RelOp, OperationType.LessThan);
            }

            if (current == '=')
            {
                return lexemeFactory.Create(LexemeType.RelOp, OperationType.Equal);
            }
            
            // this has to be true
            if (current == '>')
            {
                char next = inputStream.Peek();
                if (next == '=')
                {
                    inputStream.Next();
                    return lexemeFactory.Create(LexemeType.RelOp, OperationType.GreaterThanOrEquals);
                }
                return lexemeFactory.Create(LexemeType.RelOp, OperationType.GreaterThan);
            }
            
            // we really can't get here
            return null;
        }

        private Lexeme RecognizeIdentifierOrKeyWord(char current)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(current);
            while (Char.IsLetter(inputStream.Peek()))
            {
                builder.Append(inputStream.Next());
            }

            var identifier = builder.ToString().ToLowerInvariant();

            var type = lexemeMapper.MapsToLexeme(identifier);
            if (type != LexemeType.Unknown)
            {
                return lexemeFactory.Create(type);
            }

            return lexemeFactory.Create(LexemeType.Identifier, identifier);

        }

        /// <summary>
        /// Read a numeric constant from the stream
        /// </summary>
        /// <param name="current">the initial digit of the constant</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Lexeme RecognizeNumericConstant(char current)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(current);
            while (Char.IsDigit(inputStream.Peek()))
            {
                builder.Append(inputStream.Next());
            }

            if (UInt16.TryParse(builder.ToString(), out ushort value))
            {
                return lexemeFactory.Create(LexemeType.Constant, value);
            }
            else
            {
                throw new CompilerException($"Could not parse constant {builder}", inputStream.LineNumber, inputStream.ColumnNumber);
            }
        }

        private void SkipWhiteSpaceAndComments()
        {
            while (" \t{".Contains(inputStream.Peek()))
            {
                char current = inputStream.Next();
                if (current == '{')
                {
                    do
                    {
                        current = inputStream.Next();
                    } while (current != '}');
                }
            }
        }

        private bool IsSymbol(char candidate)
        {
            return ".,;:()+-*/<>=".Contains(candidate);
        }

        
    }
}