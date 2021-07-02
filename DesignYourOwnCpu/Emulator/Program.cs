using System;
using System.Diagnostics.CodeAnalysis;
using Emulator.Extensions;
using Microsoft.Extensions.DependencyInjection;

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

            // Get Service and call method
            var cpu = serviceProvider.GetService<ICPU>();
            cpu.Run();

            Console.WriteLine();
            cpu.Registers.ToConsole();
            Console.WriteLine();
            cpu.Memory.ToConsole(0, 128);
            Console.WriteLine();
        }
    }
}