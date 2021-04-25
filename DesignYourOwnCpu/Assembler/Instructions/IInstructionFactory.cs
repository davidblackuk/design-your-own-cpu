namespace Assembler.Instructions
{
    public interface IInstructionFactory
    {
        IInstruction Create(string name);
    }
}