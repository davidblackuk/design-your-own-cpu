using System.Collections.Generic;

namespace Assembler.LineSources
{
    public class WhitespaceRemovalLineSource : ILineSource
    {
        private readonly ILineSource source;

        public WhitespaceRemovalLineSource(ILineSource source)
        {
            this.source = source;
        }

        public IEnumerable<string> Lines()
        {
            foreach (var line in source.Lines())
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    ProcessedLines += 1;                
                    yield return line;
                }
            }
        }
        
        /// <summary>
        /// Gets the count of processed lines
        /// </summary>
        public int ProcessedLines { get; private set;  }

    }
}