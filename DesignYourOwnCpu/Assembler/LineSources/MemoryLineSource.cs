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
            foreach (var line in lines) yield return line;
        }
    }
}