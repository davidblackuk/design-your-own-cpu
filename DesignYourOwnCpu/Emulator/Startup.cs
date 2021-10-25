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

        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var binaryToExecute = Configuration["input"];

            if (binaryToExecute == null) Usage();

            IRamFactory ramFactory = new RamFactory();
            var memory = ramFactory.Create(binaryToExecute);
            services.AddSingleton<IRandomAccessMemory>(memory);

            services.AddLogging();
            services.AddSingleton(Configuration);
            services.AddSingleton<ICPU, CPU>();
            services.AddSingleton<IRegisters, Registers>();
            services.AddSingleton<IEmulatorInstructionFactory, EmulatorInstructionFactory>();
            services.AddSingleton<IFlags, Flags>();
            services.AddSingleton<IRamFactory, RamFactory>();
            services.AddSingleton<IInterruptFactory, InterruptFactory>();
            services.AddSingleton<INumberParser, NumberParser>();
            services.AddSingleton<IFileOperations, FileOperations>();
        }

        private void Usage()
        {
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("    dotnet run  -p <path to project file> --input <path for the bin file>");
            Console.WriteLine();
            Environment.Exit(0);
        }
    }
}