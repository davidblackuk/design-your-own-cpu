namespace Assembler.Instructions
{
    public interface IInstructionNameParser
    {
        /// <summary>
        ///     Parses out the instruction name and the remaining text of the instruction
        /// </summary>
        /// <param name="line">The input line of assembly</param>
        /// <returns>A tuple containing the instruction (lowercased and trimmed) and the remainder (trimmed)</returns>
        (string instruction, string remainder) Parse(string line);
    }
}