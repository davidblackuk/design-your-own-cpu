using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Compiler.LexicalAnalysis;
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
            services.AddSingleton<IKeywordLexemeTypeMap, KeywordLexemeTypeMap>();
        }
    }
}