using Shared;

namespace Assembler.Instructions
{
    /// <summary>
    ///     An instruction allowing the user to call a software interrupt and enter the kernel/rom to
    ///     perform  system operations like writing and reading strings and integers
    /// </summary>
    public class SoftwareInterruptInstruction : SingleValueInstruction
    {
        public const string InstructionName = "swi";

        public SoftwareInterruptInstruction() : base(InstructionName, OpCodes.Swi)
        {
        }
    }
}