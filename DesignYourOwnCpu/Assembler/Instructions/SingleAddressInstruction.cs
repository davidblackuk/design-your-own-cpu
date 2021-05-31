using System.Diagnostics.CodeAnalysis;

namespace Assembler.Instructions
{
    /// <summary>
    /// An instruction that takes as it's operand a single address (via either a constant or a label),
    /// examples are branches, calls etc
    /// </summary>
    public class SingleAddressInstruction : AssemblerInstruction, IAssemblerInstruction
    {
        private readonly string instructionName;

        
        /// <summary>
        /// Constructs a new instance of a branch instruction. This is a base class that is
        /// used to handle all common tasks processing the instruction (ie most of it)
        /// </summary>
        /// <param name="instructionName">The instruction name (beq, bra etc)</param>
        /// <param name="opCode">Op code for this instruction</param>
        public SingleAddressInstruction(string instructionName, byte opCode)
        {
            this.instructionName = instructionName;
            this.OpCode = opCode;
        }

        /// <summary>
        /// Parses the branch instruction and 
        /// </summary>
        /// <param name="source"></param>
        public void Parse(string source)
        {
            if (IsLabel(source))
            {
                RecordSymbolForResolution(source);
            }
            else
            {
                ParseValue(source);
            }
        }
        
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            string address = $"0x{ByteHigh:X2}{ByteLow:X2}";
            string postfix = RequresSymbolResolution ? $" => {Symbol}" : "";
            return $"{instructionName} {address}{postfix}";
                    
        }
    }
}