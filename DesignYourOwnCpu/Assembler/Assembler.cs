using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Assembler.Exceptions;
using Assembler.Extensions;
using Assembler.LineSources;
using Assembler.Symbols;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pastel;
using Shared;

namespace Assembler;

public class Assembler : IAssembler, IHostedService
{
    private readonly IParser parser;
    private readonly IHostApplicationLifetime applicationLifetime;
    private readonly IAssemblerConfig config;
    private readonly ILogger<Assembler> logger;

    public Assembler(IParser parser, ICodeGenerator codeGenerator,
        IHostApplicationLifetime applicationLifetime, 
        IAssemblerConfig config,
      ILogger<Assembler> logger)
    {
        this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
        this.applicationLifetime = applicationLifetime;
        this.config = config;
        this.logger = logger;
        CodeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
    }

    public ICodeGenerator CodeGenerator { get; }
    public IRandomAccessMemory Ram => CodeGenerator.Ram;

    public ISymbolTable SymbolTable => CodeGenerator.SymbolTable;

    public void Assemble(ILineSource lineSource)
    {
        parser.ParseAllLines(lineSource);
        CodeGenerator.GenerateCode(parser.Instructions);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (config.SourceFilename == null)
        {
            Usage();
        }

        logger.LogInformation("start async");
            
        if (!config.QuietOutput)
        {
            config.ToConsole();
        }
        
        FileLineSource rawFileSource = new FileLineSource(config.SourceFilename);
        ILineSource lineSource =
            new CommentStrippingLineSource(
                new WhitespaceRemovalLineSource(
                    rawFileSource));

        
        try
        {
            var start = DateTime.Now;

            parser.ParseAllLines(lineSource);
            CodeGenerator.GenerateCode(parser.Instructions);
            Ram.Save(config.BinaryFilename);
            SymbolTable.Save(config.SymbolFilename);
                
            if (!config.QuietOutput)
            {
                Console.WriteLine("\nSymbols\n");
                SymbolTable.ToConsole();
            }
                
            Console.WriteLine($"\nAssembled {lineSource.ProcessedLines} lines in {(DateTime.Now - start).TotalMilliseconds} (ms)\n");
        }
        catch (AssemblerException e)
        {
            ShowError(rawFileSource, "Assembler", e.Message);
        }
        catch (Exception otherException)
        {
            ShowError(rawFileSource, "Internal", otherException.Message);
        }
            
        applicationLifetime.StopApplication();
        return Task.CompletedTask;
        
        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("stop async");
        return Task.CompletedTask;
    }
    
    private static void ShowError(FileLineSource fileSource, string type, string message)
    {
        Console.Write($"{type} error: ".Pastel(Color.Tomato));
        Console.WriteLine(message);
        Console.Write($"\nLine {fileSource.ProcessedLines}: ");
        Console.WriteLine($"{fileSource.CurrentLine}\n".Pastel(Color.Teal));
    }
    
    private static void Usage()
    {
        Console.WriteLine();
        Console.WriteLine("Usage:");
        Console.WriteLine("    dotnet run  -p <path to project file> --input <path for the file to assemble>");
        Console.WriteLine();
        Environment.Exit(0);
    }
}