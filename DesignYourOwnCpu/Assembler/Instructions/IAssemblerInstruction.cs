using Shared;

namespace Assembler.Instructions
{
    public interface IAssemblerInstruction : IInstruction
    {
        /// <summary>
        ///     Does this instruction require a symbol to be resolved for it to be output
        /// </summary>
        public bool RequresSymbolResolution { get; }

        /// <summary>
        ///     For symbols this contains the name that needs to be resolved
        /// </summary>
        string Symbol { get; }

        /// <summary>
        ///     Stores a word value in the instruction
        /// </summary>
        /// <param name="value"></param>
        void StoreData(ushort value);

        /// <summary>
        ///     Parse the instruction and set the values for Opcode, register, high and low bytes (if necessary)
        /// </summary>
        /// <param name="source"></param>
        void Parse(string source);

        /// <summary>
        ///     Returns the data associated with in the instruction as space separated, two digit hex values,
        ///     (for most instructions in the order Opcode / Register / High byte / Low byte)
        /// </summary>
        string BytesString();

        /// <summary>
        ///     Write the instruction bytes to memory, at the specified address, this will write IInstruction.Size bytes
        ///     <para>
        ///         Note all executable instructions write 4 bytes, but psuedo instructions like defs, defb or defw will write
        ///         variable amounts of data
        ///     </para>
        /// </summary>
        /// <param name="ram">The ram to write to</param>
        /// <param name="address">the address to write data to</param>
        public void WriteBytes(IRandomAccessMemory ram, ushort address);
    }
}