using System.Data;
using Assembler.Exceptions;

namespace Assembler.Instructions
{
    public class InstructionFactory : IInstructionFactory
    {
        public IInstruction Create(string name)
        {
            switch (name)
            {
                case NopInstruction.InstructionName:
                    return new NopInstruction(); 
                case HaltInstruction.InstructionName:
                    return new HaltInstruction();
                default:
                    throw new AssemblerException("Unknown instruction: " + name);
            }
        }
    }
}