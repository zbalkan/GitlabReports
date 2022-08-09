using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using System.IO;
using Spectre.Console.Cli;
using System;
using System.Threading;

namespace GitlabReports
{
    public sealed class GenerateReportCommand : Command<GenerateReportCommand.Settings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            var output = settings.OutputFile;
            if (string.IsNullOrEmpty(settings.OutputFile))
            {
                output = Path.ChangeExtension(settings.InputFile, ".pdf");
            }

            AnsiConsole.Status()
                .AutoRefresh(true)
                .Spinner(Spinner.Known.BouncingBar)
                .SpinnerStyle(Style.Parse("green bold"))
                .Start("Processing...",
                    ctx =>
                    {
                        try
                        {
                            ctx.Status("Importing JSON file...");
                            var report = ReportGenerator.Import(settings.InputFile);
                            Thread.Sleep(1000);
                            ctx.Status("Generating PDF...");
                            ReportGenerator.Export(report, output);
                            Thread.Sleep(1000);
                        }
                        catch (Exception ex)
                        {
                            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                            Environment.Exit(1);
                        }
                    }
                );

            AnsiConsole.Write(new TextPath($"Report saved to: {Path.GetFullPath(output)}"));

            return 0;
        }

        public sealed class Settings : CommandSettings
        {
            /// <summary>
            ///     Path of JSON file.
            /// </summary>
            [Description("Path of JSON file.")]
            [CommandOption("-i|--input")]
            public string InputFile { get; init; }

            /// <summary>
            ///     Path of PDF file.
            /// </summary>
            [Description("Path of PDF file.")]
            [CommandOption("-o|--output")]
            public string OutputFile { get; init; }
        }
    }
}
