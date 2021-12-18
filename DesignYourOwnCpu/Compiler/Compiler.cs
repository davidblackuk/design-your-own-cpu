using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Compiler.Exceptions;
using Compiler.SyntacticAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pastel;

namespace Compiler;

internal class Compiler : IHostedService
{
    private readonly IHostApplicationLifetime applicationLifetime;
    private readonly ISyntaxAnalyser syntaxAnalyser;
    private readonly IConfiguration config;
    private ILogger<Compiler> logger;

    public Compiler(IHostApplicationLifetime applicationLifetime, 
        ISyntaxAnalyser syntaxAnalyser,
        IConfiguration config, ILogger<Compiler> logger)
    {
        this.applicationLifetime = applicationLifetime;
        this.syntaxAnalyser = syntaxAnalyser;
        this.config = config;
        this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var inputFile = config["input"];
        if (inputFile == null) Usage();
            
        logger.LogInformation("start async");
            
        logger.LogInformation($"Input file: {inputFile}");

        try
        {
            syntaxAnalyser.Scan();
        }
        catch (CompilerException e)
        {
            logger.LogError(e.Message);
        }
            
        applicationLifetime.StopApplication();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("stop async");
        return Task.CompletedTask;
    }
        
    private void Usage()
    {
        Console.WriteLine();
        Console.WriteLine("Emulator Usage:");
        Console.WriteLine("    dotnet run  -p <path to project file> --input <path for the bin file>");
        Console.WriteLine();
        applicationLifetime.StopApplication();
    }
        
}