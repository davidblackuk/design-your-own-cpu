using System;
using System.Collections.Generic;

namespace Assembler.LineSources
{
    public class MemoryLineSource : ILineSource
    {
        private readonly string[] lines;

        public MemoryLineSource(string text)
        {
            lines = text.Split(Environment.NewLine.ToCharArray());
        }

        public IEnumerable<string> Lines()
        {
            foreach (var line in lines)
            {
                ProcessedLines += 1;
                yield return line;
            }
        }
        
        /// <summary>
        /// Gets the count of processed lines
        /// </summary>
        public int ProcessedLines { get; private set;  }


    }
}