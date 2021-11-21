using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Assembler.LineSources
{
    [ExcludeFromCodeCoverage] // no integration tests
    public class FileLineSource : ILineSource
    {
        private readonly string inputFile;
        private string currentLine = String.Empty;
        
        public FileLineSource(IAssemblerConfig config)
        {
            this.inputFile = config.SourceFilename;
        }

        public IEnumerable<string> Lines()
        {
            foreach (var line in File.ReadLines(inputFile))
            {
                ProcessedLines += 1;
                currentLine = line;
                // check if include file and insert new file?
                yield return line;
            }
        }

        public ILineSource ChainTo(ILineSource downStreamSource)
        {
            // we are the end of the chain
            return downStreamSource;
        }

        /// <summary>
        /// Gets the count of processed lines
        /// </summary>
        public int ProcessedLines { get; private set;  }

        public string CurrentRawLine => currentLine;

    }
}