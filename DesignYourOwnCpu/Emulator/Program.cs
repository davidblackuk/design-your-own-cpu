using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Emulator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Pastel;
using Shared;

namespace Emulator
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static IServiceProvider ServiceProvider { get; set; }
        private static IEmulatorConfig Configuration;

        private static void Main(string[] args)
        {
            ServiceProvider = GetServiceProvider(args);
            Configuration = GetConfiguration();

            try
            {
                Emulate();
            }
            catch (EmulatorException e)
            {
                Console.WriteLine($"Emulator error: {e.Message}\n".Pastel(Color.Tomato));
            }
        }

        /// <summary>
        /// Loads the binary image and emulates it until a HALT
        /// instruction in encountered or an exception ocurrs
        /// </summary>
        private static void Emulate()
        {
            var cpu = ServiceProvider.GetService<ICPU>();
            cpu.Memory.Load(Configuration.BinaryFilename);

            cpu.Run();

            if (!Configuration.QuietOutput)
            {
                cpu.Registers.ToConsole();
                cpu.Memory.ToConsole(0, 128);
            }
        }

        /// <summary>
        /// Gets the configuration for the app
        /// </summary>
        /// <returns></returns>
        private static IEmulatorConfig GetConfiguration()
        {
            var config = ServiceProvider.GetService<IEmulatorConfig>();

            if (config.BinaryFilename == null)
            {
                Usage();
                Environment.Exit(0);
            }
            return config;
        }

        /// <summary>
        /// Gets the DI container after asking Startup to wire the dependancies
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IServiceProvider GetServiceProvider(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            var startup = new Startup(args);
            startup.ConfigureServices(services);
            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Output usage information to the console
        /// </summary>
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