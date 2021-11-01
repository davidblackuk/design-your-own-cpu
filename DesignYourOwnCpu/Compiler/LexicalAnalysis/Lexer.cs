using System;
using System.Diagnostics.CodeAnalysis;
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

        public Lexer(IInputStream inputStream, IKeywordLexemeTypeMap lexemeMapper)
        {
            this.inputStream = inputStream;
            this.lexemeMapper = lexemeMapper;
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
            
            return new Lexeme(LexemeType.Unknown);
        }

        private Lexeme RecognizeOperator(char current)
        {
            if (".,();".Contains(current))
            {
                return new Lexeme(lexemeMapper.MapsToLexeme(""+current));
            }
            if ("+-*/".Contains(current))
            {
                switch (current)
                {
                    case '+': return new Lexeme(LexemeType.AddOp, OperationType.Addition);
                    case '-': return new Lexeme(LexemeType.AddOp, OperationType.Subtraction);
                    case '/': return new Lexeme(LexemeType.MulOp, OperationType.Division);
                    default:  return new Lexeme(LexemeType.MulOp, OperationType.Multiplication);
                }
            }

            if (current == ':')
            {
                if (inputStream.Peek() == '=')
                {
                    inputStream.Next();
                    return new Lexeme(LexemeType.Assign);
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
                    return new Lexeme(LexemeType.RelOp, OperationType.LessThanOrEquals);
                }
                if (next == '>')
                {
                    inputStream.Next();
                    return new Lexeme(LexemeType.RelOp, OperationType.NotEqual);
                }
                return new Lexeme(LexemeType.RelOp, OperationType.LessThan);
            }

            if (current == '=')
            {
                return new Lexeme(LexemeType.RelOp, OperationType.Equal);
            }
            
            // this has to be true
            if (current == '>')
            {
                char next = inputStream.Peek();
                if (next == '=')
                {
                    inputStream.Next();
                    return new Lexeme(LexemeType.RelOp, OperationType.GreaterThanOrEquals);
                }
                return new Lexeme(LexemeType.RelOp, OperationType.GreaterThan);
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
                return new Lexeme(type);
            }

            return new Lexeme(LexemeType.Identifier, identifier);

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
                return new Lexeme(LexemeType.Constant, value);
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