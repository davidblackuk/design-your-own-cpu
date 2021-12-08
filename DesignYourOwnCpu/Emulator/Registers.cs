namespace Emulator
{
    public class Registers : IRegisters
    {
        private const ushort ProgramCounterDefaultValue = 0;
        private const ushort StackPointerDefaultValue = 0xFFFF;

        private readonly ushort[] registers  =
            { 0, 0, 0, 0, 0, 0, 0, 0, ProgramCounterDefaultValue, StackPointerDefaultValue };

        public ushort this[byte index]
        {
            get => registers[index];
            set => registers[index] = value;
        }

        /// <summary>
        ///     points to the next instruction to execute
        /// </summary>
        public ushort ProgramCounter
        {
            get => registers[8];
            set => registers[8] = value;
        }

        /// <summary>
        ///     not doing stacks yet, but ...
        /// </summary>
        public ushort StackPointer
        {
            get => registers[9];
            set => registers[9] = value;
        }
    }
}