namespace Assembler.Instructions
{
    public interface IInstruction
    {
        public byte OpCode { get; }
        public byte Register { get; }
        public byte ByteHigh { get; }
        public byte ByteLow { get; }
        
        public void Parse(string source);
    }
}