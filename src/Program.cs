using Spectre.Console.Cli;

namespace GitlabReports
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandApp<GenerateReportCommand>();
            app.Configure(c =>
            {
                c.AddExample(new[] {"-i", "report.json"});
                c.SetApplicationName("sr");
            });
            return app.Run(args);
        }
    }
}