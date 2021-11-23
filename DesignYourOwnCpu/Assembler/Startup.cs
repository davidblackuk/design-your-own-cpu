using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using Assembler.LineSources;
using Assembler.Symbols;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace Assembler
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

        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton(Configuration);
            services.AddSingleton<ISymbolTable, SymbolTable>();
            services.AddSingleton<IInstructionNameParser, InstructionNameParser>();
            services.AddSingleton<IAssemblerInstructionFactory, AssemblerInstructionFactory>();
            services.AddSingleton<IRandomAccessMemory, RandomAccessMemory>();
            services.AddSingleton<ICodeGenerator, CodeGenerator>();
            services.AddSingleton<IParser, Parser>();
            services.AddSingleton<IAssemblerConfig, AssemblerConfig>();
            services.AddSingleton<IAssembler, Assembler>();

            services.AddSingleton<FileLineSource>();
            services.AddSingleton<CommentStrippingLineSource>();
            services.AddSingleton<WhitespaceRemovalLineSource>();

        }
    }
}