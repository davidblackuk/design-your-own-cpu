using System.Collections.Generic;
using System.Linq;
using Assembler.Exceptions;
using Shared;

namespace Assembler.Instructions.PsuedoInstructions
{
    public class DefineMessageInstruction: AssemblerInstruction, IAssemblerInstruction
    {
        public const string InstructionName = "defm";
        private string rawMessage;
        private readonly List<byte> bytes = new List<byte>();
        
        /// <summary>
        /// Size of space defined in bytes
        /// </summary>
        public override ushort Size => (ushort)bytes.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public override void Parse(string source)
        {
          

            var startIndex = source.IndexOf('"');
            var endIndex = source.LastIndexOf('"');

            if (startIndex == -1 || endIndex == -1 || startIndex == endIndex)
            {
                throw new AssemblerException("Malformed message definition: Messages must be enclosed in quotes");
            }

            rawMessage = source.Substring(startIndex + 1, (endIndex - startIndex) - 1);
            foreach (var character in rawMessage)
            {
                bytes.Add(AsciiMapper.ConvertCharToByte(character));
            }
            bytes.Add(0);
        }

        public override void WriteBytes(IRandomAccessMemory ram, ushort address)
        {
            for (ushort i = 0; i < bytes.Count; i++)
            {
                ram[(ushort)(address + i)] = bytes[i];
            }
        }

        public override string BytesString()
        {
            return ".. .. .. ..";
        }
        
        public override string ToString()
        {
            return $"defm \"{rawMessage}\" ({rawMessage.Length} chars, {Size} bytes)";
        }
    }}