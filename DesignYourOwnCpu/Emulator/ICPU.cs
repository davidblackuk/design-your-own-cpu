namespace Emulator
{
    public interface ICPU
    {
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