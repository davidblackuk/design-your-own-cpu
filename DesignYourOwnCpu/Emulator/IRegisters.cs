namespace Emulator
{
    public interface IRegisters
    {
        ushort this[ushort address] { get; set; }

        /// <summary>
        /// points to the next instruction to execute
        /// </summary>
        ushort ProgramCounter { get; set; }

        /// <summary>
        /// not doing stacks yet, but ...
        /// </summary>
        ushort StackPointer { get; set; }
    }
}