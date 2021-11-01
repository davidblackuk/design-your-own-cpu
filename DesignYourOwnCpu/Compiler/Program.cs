using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Compiler.Exceptions;
using Compiler.LexicalAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Pastel;

namespace Compiler
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            var startup = new Startup(args);
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            if (startup.SourceFile == null) Usage();
            
            try
            {
                var x = serviceProvider.GetService<ILexer>();
                while (true)
                {
                    var res = x.GetLexeme();
                    if (res != null)
                    {
                        Console.Write(res);
                    }
                }
            }
            catch (CompilerException e)
            {
                Console.WriteLine($"\nEmulator error: {e.Message}\n".Pastel(Color.Tomato));
            }
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
}
