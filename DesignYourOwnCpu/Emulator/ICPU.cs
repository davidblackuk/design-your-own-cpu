using Shared;

namespace Emulator
{
    public interface ICPU
    {
        public IRegisters Registers { get;  }

        public IRandomAccessMemory Memory { get; }
        
        /// <summary>
        /// Is the CPU halted?
        /// </summary>
        bool Halted { get; set; }

        /// <summary>
        /// Executes till a halt instruction is hit
        /// </summary>
        void Run();
    }
}