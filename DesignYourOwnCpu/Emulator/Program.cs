using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Emulator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Pastel;

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

            try
            {
                // Get Service and call method
                var cpu = serviceProvider.GetService<ICPU>();
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
    }
}