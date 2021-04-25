using Assembler.LineSources;

namespace Assembler
{
    public interface IParser
    {
        void ParseAllLines(ILineSource lineSource);
    }
}