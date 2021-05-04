using Shared;

namespace Assembler.Instructions
{
    public interface IAssemblerInstruction: IInstruction
    {

        /// <summary>
        /// Does this instruction require a symbol to be resolved for it to be output
        /// </summary>
        public bool RequresSymbolResolution { get; }

        /// <summary>
        /// For symbols this contains the name that needs to be resolved
        /// </summary>
        string Symbol { get; }

        /// <summary>
        /// Stores a word value in the instruction
        /// </summary>
        /// <param name="value"></param>
        void StoreData(ushort value);
        
        /// <summary>
        /// Parse the instruction and set the values for Opcode, register, high and low bytes (if necessary)
        /// </summary>
        /// <param name="source"></param>
        void Parse(string source);

        /// <summary>
        /// Returns the data associated with in the instruction as 4 space separated, two digit hex values,
        /// in the order Opcode / Register / High byte / Low byte
        /// </summary>
        string BytesString();
    }
}