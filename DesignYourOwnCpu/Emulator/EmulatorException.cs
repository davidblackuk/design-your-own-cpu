using System;

namespace Emulator;

public class EmulatorException : Exception
{
    public EmulatorException(string message) : base(message)
    {
    }
}