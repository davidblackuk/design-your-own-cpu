namespace Assembler.Instructions
{
    public interface IInstruction
    {
        /// <summary>
        /// The opcode for this instruction
        /// </summary>
        byte OpCode { get; }
        
        /// <summary>
        /// Primary register number for this instruction if one is needed
        /// </summary>
        byte Register { get; }
        
        /// <summary>
        /// High byte of the address or value we are using (if required)
        /// </summary>
        byte ByteHigh { get; }
        
        /// <summary>
        /// Low byte of the address or value we are using (if required)
        /// </summary>
        byte ByteLow { get; }

        /// <summary>
        /// The size of the instruction in bytes (currently always 4 bytes)
        /// </summary>
        public ushort Size { get;  }

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