using System;
using System.Collections.Generic;

namespace Assembler.LineSources
{
    public class MemoryLineSource : ILineSource
    {
        private readonly string[] lines;
        private string currentLine;
        public MemoryLineSource(string text)
        {
            lines = text.Split(Environment.NewLine.ToCharArray());
        }

        public IEnumerable<string> Lines()
        {
            foreach (var line in lines)
            {
                ProcessedLines += 1;
                currentLine = line;
                yield return currentLine;
            }
        }

        public ILineSource ChainTo(ILineSource downStreamSource)
        {
            throw new NotImplementedException();
        }

        public int ProcessedLines { get; private set;  }

        public string CurrentRawLine => currentLine;


    }
}