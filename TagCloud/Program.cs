using Autofac;
using CommandLine;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TagCloud.Tests")]
namespace TagCloud
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<CommandLineOptions>(args);

            parserResult.WithParsed(options =>
            {
                Run(DIContainer.ConfigureContainer(options));
            });

            parserResult.WithNotParsed(errors =>
            {
                Console.WriteLine("Ошибка парсинга аргументов:");
                foreach (var error in errors)
                    Console.WriteLine(error.ToString());
            });
        }

        private static void Run(IContainer container)
        {
            using var scope = container.BeginLifetimeScope();
            var program = scope.Resolve<ProgramExecutor>();
            program.Execute();
        }
    }
}
