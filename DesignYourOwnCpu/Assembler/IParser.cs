using System.Collections.Generic;
using Assembler.Instructions;
using Assembler.LineSources;

namespace Assembler
{
    public interface IParser
    {
        /// <summary>
        ///     Instructions generated for the assembly pass
        /// </summary>
        List<IAssemblerInstruction> Instructions { get; }

        /// <summary>
        ///     PArse all of the lines and create the instructions
        /// </summary>
        /// <param name="lineSource"></param>
        void ParseAllLines(ILineSource lineSource);
    }
}