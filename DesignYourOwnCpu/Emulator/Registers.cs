namespace Emulator
{
    public class Registers : IRegisters
    {
        private ushort[] registers { get; } = new ushort[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0xFF};
        
        public ushort this[byte index]
        {
            get => registers[index];
            set => registers[index] = value;
        }

        /// <summary>
        /// points to the next instruction to execute
        /// </summary>
        public ushort ProgramCounter
        {
            get => registers[8];
            set => registers[8] = value;
        }
        
        /// <summary>
        /// not doing stacks yet, but ...
        /// </summary>
        public ushort StackPointer    {
            get => registers[9];
            set => registers[9] = value;
        }

    }
}