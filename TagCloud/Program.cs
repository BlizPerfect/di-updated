using Autofac;
using CommandLine;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TagCloud.Tests")]
namespace TagCloud
{
    internal class Program
    {
        private static IContainer container;

        static void Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<CommandLineOptions>(args);

            parserResult.WithParsed(options =>
            {
                container = DIContainer.ConfigureContainer(options);
                Run(options.ResultFormat);
            });

            parserResult.WithNotParsed(errors =>
            {
                Console.WriteLine("Ошибка парсинга аргументов:");
                foreach (var error in errors)
                    Console.WriteLine(error.ToString());
            });
        }

        private static void Run(string resultFormat)
        {
            using var scope = container.BeginLifetimeScope();
            var program = scope.Resolve<ProgramExecutor>();
            program.Execute(resultFormat);
        }
    }
}
