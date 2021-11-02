using System.Collections.Generic;

namespace Assembler.LineSources
{
    public interface ILineSource
    {
        IEnumerable<string> Lines();

        int ProcessedLines { get; }
    
    }
}