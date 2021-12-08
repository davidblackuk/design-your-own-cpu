using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Emulator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Pastel;
using Shared;

namespace Emulator
{
    [ExcludeFromCodeCoverageAttribute]
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            var startup = new Startup(args);
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            
            var binaryToExecute = startup.Configuration["input"];
            if (binaryToExecute == null) Usage();
            
            try
            {
                // Get Service and call method
                var cpu = serviceProvider.GetService<ICpu>();
                cpu.Memory.Load(binaryToExecute);
                
                cpu.Run();

                Console.WriteLine();
                cpu.Registers.ToConsole();
                Console.WriteLine();
                cpu.Memory.ToConsole(0, 128);
                Console.WriteLine();
            }
            catch (EmulatorException e)
            {
                Console.WriteLine($"Emulator error: {e.Message}\n".Pastel(Color.Tomato));
            }
        }
        
        private static void Usage()
        {
            Console.WriteLine();
            Console.WriteLine("Emulator Usage:");
            Console.WriteLine("    dotnet run  -p <path to project file> --input <path for the bin file>");
            Console.WriteLine();
            Environment.Exit(0);
        }
    }
}