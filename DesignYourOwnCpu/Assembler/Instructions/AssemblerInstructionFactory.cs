using System.Data;
using Assembler.Exceptions;

namespace Assembler.Instructions
{
    public class AssemblerInstructionFactory : IAssemblerInstructionFactory
    {
        public IAssemblerInstruction Create(string name)
        {
            switch (name)
            {
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
                case BrachGreaterThanInstruction.InstructionName:
                    return new BrachGreaterThanInstruction();
                case BranchAlwaysInstruction.InstructionName:
                    return new BranchAlwaysInstruction();
                
                case AddInstruction.InstructionName:
                    return new AddInstruction();
                case SubtractInstruction.InstructionName:
                    return new SubtractInstruction();
                
                case ReturnInstruction.InstructionName:
                    return new ReturnInstruction();
                case CallInstruction.InstructionName:
                    return new CallInstruction();
                case PushInstruction.InstructionName:
                    return new PushInstruction();
                case PopInstruction.InstructionName:
                    return new PopInstruction();
                
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