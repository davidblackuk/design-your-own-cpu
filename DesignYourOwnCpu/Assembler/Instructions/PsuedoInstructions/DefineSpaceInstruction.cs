using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions.PsuedoInstructions
{
    /// <summary>
    ///     Reserves a chunk of memory for storage etc. Only accepts a single size argument
    /// </summary>
    public class DefineSpaceInstruction : AssemblerInstruction, IAssemblerInstruction
    {
        public const string InstructionName = "defs";

        /// <summary>
        ///     Size of space defined in bytes
        /// </summary>
        public override ushort Size => (ushort)((ByteHigh << 8) | ByteLow);

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        public override void Parse(string source)
        {
            // TODO: Check if it is a symbol first when we can define constants
            ParseValue(source);
        }

        public override void WriteBytes(IRandomAccessMemory ram, ushort address)
        {
            // TODO: decision - zero the bytes or leave as is?
        }

        [ExcludeFromCodeCoverage]
        public override string BytesString()
        {
            return ".. .. .. ..";
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"defs 0x{Size:X4}";
        }
    }
}