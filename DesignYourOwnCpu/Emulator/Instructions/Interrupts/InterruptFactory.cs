using System;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace Emulator.Instructions.Interrupts;

public class InterruptFactory : IInterruptFactory
{
    private readonly IServiceProvider services;

    public InterruptFactory(IServiceProvider services)
    {
        this.services = services;
    }

    public IInterrupt Create(ushort vector)
    {
        switch (vector)
        {
            case InternalSymbols.WriteStringInterrupt:
                return new WriteStringInterrupt();
            case InternalSymbols.ReadWordInterrupt:
                return new ReadWordInterrupt(services.GetService<INumberParser>());
            case InternalSymbols.WriteWordInterrupt:
                return new WriteWordInterrupt();
            default:
                throw new EmulatorException($"Unknown interrupt vector {vector}");
        }
    }
}