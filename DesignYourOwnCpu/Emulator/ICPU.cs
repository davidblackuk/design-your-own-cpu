using Shared;

namespace Emulator
{
    public interface ICpu
    {
        public IRegisters Registers { get; }

        public IRandomAccessMemory Memory { get; }

        public IFlags Flags { get; set; }

        /// <summary>
        ///     Executes till a halt instruction is hit
        /// </summary>
        void Run();
    }
}