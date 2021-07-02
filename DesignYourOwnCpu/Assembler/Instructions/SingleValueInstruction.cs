using System.Diagnostics.CodeAnalysis;

namespace Assembler.Instructions
{
    /// <summary>
    ///     An instruction that takes as it's operand a single 16bit value (address or data) (via either a constant or a
    ///     label),
    ///     examples are branches, calls, interrupts etc
    /// </summary>
    public class SingleValueInstruction : AssemblerInstruction, IAssemblerInstruction
    {
        private readonly string instructionName;


        /// <summary>
        ///     Constructs a new instance of a branch instruction. This is a base class that is
        ///     used to handle all common tasks processing the instruction (ie most of it)
        /// </summary>
        /// <param name="instructionName">The instruction name (beq, bra etc)</param>
        /// <param name="opCode">Op code for this instruction</param>
        public SingleValueInstruction(string instructionName, byte opCode)
        {
            this.instructionName = instructionName;
            OpCode = opCode;
        }

        /// <summary>
        ///     Parses the branch instruction and
        /// </summary>
        /// <param name="source"></param>
        public override void Parse(string source)
        {
            ParseConstantValueOrSymbol(source);
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            var address = $"0x{ByteHigh:X2}{ByteLow:X2}";
            var postfix = RequresSymbolResolution ? $" => {Symbol}" : "";
            return $"{instructionName} {address}{postfix}";
        }
    }
}