using System.Collections.Generic;

namespace Assembler.LineSources
{
    public class WhitespaceRemovalLineSource : ILineSource
    {
        private ILineSource source;

        public WhitespaceRemovalLineSource()
        {
        }

        public IEnumerable<string> Lines()
        {
            foreach (var line in source.Lines())
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    yield return line;
                }
            }
        }

        public ILineSource ChainTo(ILineSource downStreamSource)
        {
            source = downStreamSource;
            return downStreamSource;
        }

        /// <summary>
        /// Gets the count of processed lines, we delegate to the down stream source
        /// </summary>
        public int ProcessedLines => source.ProcessedLines;

        /// <summary>
        /// Gets the current raw line (we delegate to the down stream source
        /// </summary>
        public string CurrentRawLine => source.CurrentRawLine;

    }
}