namespace Assembler.Instructions
{
    public interface IInstruction
    {
        byte OpCode { get; }
        byte Register { get; }
        byte ByteHigh { get; }
        byte ByteLow { get; }
        

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
        
        
        void Parse(string source);

        string BytesString();
    }
}