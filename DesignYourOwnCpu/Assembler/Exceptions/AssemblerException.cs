using System;

namespace Assembler.Exceptions
{
    public class AssemblerException : Exception
    {
        public AssemblerException(string name) : base(name)
        {
        }
    }
}