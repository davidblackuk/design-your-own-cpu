using System;
using System.Collections.Generic;
using System.IO;

namespace Assembler.LineSources
{
    public class FileLineSource : ILineSource
    {
        private readonly string inputFile;

        public FileLineSource(string inputFile)
        {
            this.inputFile = inputFile ?? throw new ArgumentNullException(nameof(inputFile));
        }

        public IEnumerable<string> Lines()
        {
            foreach (var line in File.ReadLines(inputFile))
                // check if include file and insert new file?
                yield return line;
        }
    }
}