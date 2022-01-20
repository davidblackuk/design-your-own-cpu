using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using Assembler.Exceptions;
using Assembler.Extensions;
using Assembler.Instructions;
using Assembler.LineSources;
using Assembler.Symbols;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pastel;
using Shared;

namespace Assembler;

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
                services.AddSingleton<ISymbolTable, SymbolTable>();
                services.AddSingleton<IInstructionNameParser, InstructionNameParser>();
                services.AddSingleton<IAssemblerInstructionFactory, AssemblerInstructionFactory>();
                services.AddSingleton<IRandomAccessMemory, RandomAccessMemory>();
                services.AddSingleton<ICodeGenerator, CodeGenerator>();
                services.AddSingleton<IParser, Parser>();
                services.AddSingleton<IAssemblerConfig, AssemblerConfig>();

                services.AddHostedService<Assembler>();
            });
    }
}