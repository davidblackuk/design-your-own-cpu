namespace Shared
{
    public class Instruction : IInstruction
    {
        /// <summary>
        ///     The op code of the instruction
        /// </summary>
        public byte OpCode { get; protected set; }

        /// <summary>
        ///     The register r0..7
        /// </summary>
        public byte Register { get; protected set; }

        /// <summary>
        ///     High byte of the data associated with the instruction
        /// </summary>
        public byte ByteHigh { get; protected set; }

        /// <summary>
        ///     Low byte of the data associated with the instruction
        /// </summary>
        public byte ByteLow { get; protected set; }

        /// <summary>
        ///     All instructions are size 4 in this architecture, but storage psuedo-instructions like
        ///     defs, defw and defb produce variable size data (These instructions are also never executed)
        /// </summary>
        public virtual ushort Size { get; } = 4;
    }
}