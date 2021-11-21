using System.Collections.Generic;

namespace Assembler.LineSources
{
    public interface ILineSource
    {
        IEnumerable<string> Lines();

        /// <summary>
        /// THe number for lines processed (current line number)
        /// </summary>
        int ProcessedLines { get; }

        /// <summary>
        /// The current line being processed
        /// </summary>
        string CurrentRawLine { get; }

        /// <summary>
        /// Chains this line source to a downsstream implementation
        /// </summary>
        /// <param name="downStreamSource">The line source to chain to</param>
        /// <returns>THe down stream source</returns>
        ILineSource ChainTo(ILineSource downStreamSource);
    }
}