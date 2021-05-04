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
                case CompareInstruction.InstructionName:
                    return new CompareInstruction();
                case BranchEqualInstruction.InstructionName:
                    return new BranchEqualInstruction();
                case BranchLessThanInstruction.InstructionName:
                    return new BranchLessThanInstruction();
                case BranchGreaterThanInstruction.InstructionName:
                    return new BranchGreaterThanInstruction();
                case BranchAlwaysInstruction.InstructionName:
                    return new BranchAlwaysInstruction();
                case AddInstruction.InstructionName:
                    return new AddInstruction();
                case SubtractInstruction.InstructionName:
                    return new SubtractInstruction();
                default:
                    throw new AssemblerException("Unknown instruction: " + name);
            }
        }
    }
}