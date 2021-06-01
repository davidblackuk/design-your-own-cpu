namespace Shared
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

    }

    public class Instruction : IInstruction
    {
        /// <summary>
        /// The op code of the instruction
        /// </summary>
        public byte OpCode { get; protected set; }
        
        /// <summary>
        /// The rsgister r0..7
        /// </summary>
        public byte Register { get; protected set; }
        
        /// <summary>
        /// High byte of the data associated with the instruction 
        /// </summary>
        public byte ByteHigh { get; protected set; }
        
        /// <summary>
        /// Low byte of the data associated with the instruction
        /// </summary>
        public byte ByteLow { get; protected set; }
        
        /// <summary>
        /// All instructions are size 4 in this architecture, but storage psuedo-instructions like
        /// defs, defw and defb produce variable size data (These instructions are also never executed) 
        /// </summary>
        public virtual ushort Size { get; } = 4;
    }
}