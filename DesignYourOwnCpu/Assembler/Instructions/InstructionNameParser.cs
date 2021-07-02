using System;

namespace Assembler.Instructions
{
    /// <summary>
    ///     Parses a line and strips out the instruction name
    /// </summary>
    public class InstructionNameParser : IInstructionNameParser
    {
        /// <summary>
        ///     Parses out the instruction name and the remaining text of the instruction
        /// </summary>
        /// <param name="line">The input line of assembly</param>
        /// <returns>A tuple containing the instruction (lowercased and trimmed) and the remainder (trimmed)</returns>
        public (string instruction, string remainder) Parse(string line)
        {
            var instruction = string.Empty;
            var remainder = string.Empty;

            line = line.Trim();
            var idx = line.IndexOf(" ", StringComparison.Ordinal);
            if (idx == -1)
            {
                instruction = line;
            }
            else
            {
                instruction = line.Substring(0, idx).Trim();
                remainder = line.Substring(idx).Trim();
            }

            return (instruction: instruction.ToLowerInvariant(), remainder);
        }
    }
}