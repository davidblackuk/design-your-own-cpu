using System.Diagnostics.CodeAnalysis;

namespace Assembler.Instructions
{
    /// <summary>
    /// Base class for single register instructions like push, pop, rotr, rotl etc
    /// </summary>
    public class SingleRegisterInstruction : AssemblerInstruction, IAssemblerInstruction
    {
        private readonly string instructionName;

        
        /// <summary>
        /// Constructs a new instance of a branch instruction. This is a base class that is
        /// used to handle all common tasks processing the instruction (ie most of it)
        /// </summary>
        /// <param name="instructionName">The instruction name (beq, bra etc)</param>
        /// <param name="opCode">Op code for this instruction</param>
        public SingleRegisterInstruction(string instructionName, byte opCode)
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
            Register = ParseRegister(source);
        }
        
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"{instructionName} r{Register}";
                    
        }
    }
}