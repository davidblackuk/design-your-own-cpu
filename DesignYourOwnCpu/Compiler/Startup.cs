using System.Diagnostics.CodeAnalysis;
using Compiler.Ast;
using Compiler.LexicalAnalysis;
using Compiler.SyntacticAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Compiler
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

        internal string SourceFile => Configuration["input"];
        
        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IInputStream>(s => new InputStream(SourceFile));
            services.AddSingleton<ILexer, Lexer>();
            services.AddSingleton<ISyntaxAnalyser, SyntaxAnalyser>();
            services.AddSingleton<ILexemeFactory, LexemeFactory>();
            services.AddSingleton<IKeywordLexemeTypeMap, KeywordLexemeTypeMap>();
            services.AddSingleton<ISymbolTable, SymbolTable>();
            services.AddSingleton<IAbstractSyntaxTree, AbstractSyntaxTree>();
        }
    }
}