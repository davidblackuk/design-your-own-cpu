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
                case LoadInstruction.InstructionName:
                    return new LoadInstruction();
                case StoreInstruction.InstructionName:
                    return new StoreInstruction();
                case StoreHiInstruction.InstructionName:
                    return new StoreHiInstruction();
                case StoreLowInstruction.InstructionName:
                    return new StoreLowInstruction();
                default:
                    throw new AssemblerException("Unknown instruction: " + name);
            }
        }
    }
}