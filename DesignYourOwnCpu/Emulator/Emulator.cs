using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pastel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shared;
using Emulator.Extensions;

namespace Emulator;

[ExcludeFromCodeCoverage]
internal class Emulator: IHostedService
{
    private readonly IHostApplicationLifetime applicationLifetime;
    private readonly ICpu cpu;
    private readonly IConfiguration config;
    private readonly ILogger<Emulator> logger;

    public Emulator(IHostApplicationLifetime applicationLifetime,
        ICpu cpu,
        IConfiguration config, 
        ILogger<Emulator> logger)
    {
        this.applicationLifetime = applicationLifetime;
        this.cpu = cpu;
        this.config = config;
        this.logger = logger;
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        var inputFile = config["input"];
        if (inputFile == null)
        {
            Usage();
        }
        logger.LogInformation("start async");

        logger.LogInformation($"Input file: {inputFile}");

        try
        {
           

            // Load the app image at address zero
            cpu.Memory.Load(inputFile);

            // Run the CPU
            cpu.Run();

            Console.WriteLine();
            cpu.Registers.ToConsole();
            Console.WriteLine();
            cpu.Memory.ToConsole(0, 128);
            Console.WriteLine();
        }
        catch (EmulatorException emulatorException)
        {
            Console.WriteLine($"Emulator error: {emulatorException.Message}\n".Pastel(Color.Tomato));
        }
        catch (Exception emulatorException)
        {
            logger.LogError("Unexpected exception {exception}", emulatorException.Message);
        }


        applicationLifetime.StopApplication();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("stop async");
        return Task.CompletedTask;
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
