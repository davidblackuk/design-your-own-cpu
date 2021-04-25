using System;
using System.Collections.Generic;

namespace Assembler.LineSources
{
    public class WhitespaceRemovalLineSource: ILineSource
    {
        private readonly ILineSource source;

        public WhitespaceRemovalLineSource(ILineSource source)
        {
            this.source = source;
        }

        public IEnumerable<string> Lines()
        {
            foreach (string line in source.Lines())
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    yield return line;
                }
            }
        }
    }
}