using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Assembler.Exceptions;
using Shared;

namespace Assembler.Instructions.PsuedoInstructions
{
    public class DefineMessageInstruction : AssemblerInstruction
    {
        public const string InstructionName = "defm";
        internal readonly List<byte> Bytes = new();
        private string rawMessage;

        /// <summary>
        ///     Size of space defined in bytes
        /// </summary>
        public override ushort Size => (ushort)Bytes.Count;

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        public override void Parse(string source)
        {
            var isEscaped = false;

            StripEnclosingQuotes(source);
            foreach (var character in rawMessage)
            {
                if (character == '\\')
                {
                    isEscaped = HandleStartEscapeSymbol(isEscaped, character);
                }
                else if (isEscaped)
                {
                    OutputEscapedCharacter(character);
                    isEscaped = false;
                }
                else
                {
                    Bytes.Add(MockAsciiMapper.ConvertCharToByte(character));
                }
            }

            // if we got here then we have a \ at the very end of the string
            if (isEscaped) throw new AssemblerException("Unterminated escape sequence at line end");
        }

        public override void WriteBytes(IRandomAccessMemory ram, ushort address)
        {
            for (ushort i = 0; i < Bytes.Count; i++) ram[(ushort)(address + i)] = Bytes[i];
        }

        [ExcludeFromCodeCoverage]
        public override string BytesString()
        {
            return ".. .. .. ..";
        }

        private void StripEnclosingQuotes(string source)
        {
            var startIndex = source.IndexOf('"');
            var endIndex = source.LastIndexOf('"');

            if (startIndex == -1 || endIndex == -1 || startIndex == endIndex)
                throw new AssemblerException("Malformed message definition: Messages must be enclosed in quotes");

            rawMessage = source.Substring(startIndex + 1, endIndex - startIndex - 1);
        }

        private void OutputEscapedCharacter(char character)
        {
            switch (character)
            {
                case '0':
                    Bytes.Add(0);
                    break;
                case 'n':
                    Bytes.Add(MockAsciiMapper.NewLine);
                    break;
                case 't':
                    Bytes.Add(MockAsciiMapper.Tab);
                    break;
                case '\"':
                case '\\':
                    Bytes.Add(MockAsciiMapper.ConvertCharToByte(character));
                    break;
                default:
                    throw new AssemblerException($"Unknown escape sequence \\{character}");
            }
        }

        private bool HandleStartEscapeSymbol(bool isEscaped, char character)
        {
            if (isEscaped) Bytes.Add(MockAsciiMapper.ConvertCharToByte(character));
            return !isEscaped;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"defm \"{rawMessage}\" ({rawMessage.Length} chars";
        }
    }
}