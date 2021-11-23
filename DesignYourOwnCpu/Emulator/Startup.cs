using System;
using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using Emulator.Instructions.Interrupts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace Emulator
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        internal IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddLogging();
            services.AddSingleton(Configuration);
            services.AddSingleton<ICPU, CPU>();
            services.AddSingleton<IRandomAccessMemory, RandomAccessMemory>();
            services.AddSingleton<IRegisters, Registers>();
            services.AddSingleton<IEmulatorInstructionFactory, EmulatorInstructionFactory>();
            services.AddSingleton<IFlags, Flags>();
            services.AddSingleton<IInterruptFactory, InterruptFactory>();
            services.AddSingleton<INumberParser, NumberParser>();
            services.AddSingleton<IFileOperations, FileOperations>();
            services.AddSingleton<IEmulatorConfig, EmulatorConfig>();
        }
    }
}