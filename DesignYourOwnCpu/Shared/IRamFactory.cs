namespace Shared
{
    public interface IRamFactory
    {
        RandomAccessMemory Create(string path = null);
    }
}