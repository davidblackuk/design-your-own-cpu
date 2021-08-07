using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            var binaryToExecute = Configuration["input"];

            if (binaryToExecute == null)
            {
                Usage();
            }
            
        }

        private void Usage()
        {
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine($"    dotnet run  -p <path to project file> --input <path for the bin file>");
            Console.WriteLine();
            Environment.Exit(0);
        }
    }