using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions
{
    public class HaltInstruction : IInstruction
    {
        public const string InstructionName = "halt";

        public byte OpCode => OpCodes.Halt;
        public byte Register => 0x00;
        public byte ByteHigh => 0x00;
        public byte ByteLow => 0x00;

        public void Parse(string source)
        {
            // nothing to parse as this has no operands
        }
        
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"{OpCode:X2} {Register:X2} {ByteHigh:X2} {ByteLow:X2}    # {InstructionName}";
        }
    }
}