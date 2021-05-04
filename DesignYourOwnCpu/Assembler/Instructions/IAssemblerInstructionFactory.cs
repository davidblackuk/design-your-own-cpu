namespace Assembler.Instructions
{
    public interface IAssemblerInstructionFactory
    {
        IAssemblerInstruction Create(string name);
    }
}