using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Emulator.Instructions;
using Emulator.Instructions.Interrupts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;

namespace Emulator;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        CreateHostBuilder(args)
           .Build()
           .Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((hostingContext, config) =>
           {
               config.AddJsonFile($"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}appsettings.json", optional: false);
               config.AddEnvironmentVariables();

               if (args != null)
               {
                   config.AddCommandLine(args);
               }
           })
           .ConfigureServices((hostContext, services) =>
           {
               services.AddLogging();
               services.AddSingleton(hostContext.Configuration);
               services.AddSingleton<ICpu, Cpu>();
               services.AddSingleton<IRandomAccessMemory, RandomAccessMemory>();
               services.AddSingleton<IRegisters, Registers>();
               services.AddSingleton<IEmulatorInstructionFactory, EmulatorInstructionFactory>();
               services.AddSingleton<IFlags, Flags>();
               services.AddSingleton<IInterruptFactory, InterruptFactory>();
               services.AddSingleton<INumberParser, NumberParser>();
               services.AddSingleton<IFileOperations, FileOperations>();
               services.AddHostedService<Emulator>();
           });
    }  
}