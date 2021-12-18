using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Compiler.Ast;
using Compiler.LexicalAnalysis;
using Compiler.SyntacticAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Compiler
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args)
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
                    services.AddSingleton<IInputStream>(_ => new InputStream(hostContext.Configuration["input"]));
                    services.AddSingleton<ILexer, Lexer>();
                    services.AddSingleton<ISyntaxAnalyser, SyntaxAnalyser>();
                    services.AddSingleton<ILexemeFactory, LexemeFactory>();
                    services.AddSingleton<IKeywordLexemeTypeMap, KeywordLexemeTypeMap>();
                    services.AddSingleton<ISymbolTable, SymbolTable>();
                    services.AddSingleton<IAbstractSyntaxTree, AbstractSyntaxTree>();

                    services.AddHostedService<Compiler>();
                });
        }
    }
}
