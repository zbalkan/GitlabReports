using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using System.IO;
using Spectre.Console.Cli;
using System;
using System.Threading;
// ReSharper disable ClassNeverInstantiated.Global

namespace GitlabReports
{
    public sealed class GenerateReportCommand : Command<GenerateReportCommand.Settings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            if (string.IsNullOrEmpty(settings.OutputFile))
            {
                settings.OutputFile = Path.ChangeExtension(settings.InputFile, ".pdf");
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
                            ReportGenerator.Export(report, settings.OutputFile);
                            Thread.Sleep(1000);
                        }
                        catch (Exception ex)
                        {
                            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                            Environment.Exit(1);
                        }
                    }
                );

            AnsiConsole.Write(new TextPath($"Report saved to: {settings.OutputFile}"));

            return 0;
        }

        public sealed class Settings : CommandSettings
        {
            private string _inputFile;
            private string _outputFile;

            /// <summary>
            ///     Path of JSON file.
            /// </summary>
            [Description("Path of JSON file.")]
            [CommandOption("-i|--input")]
            public string InputFile
            {
                get => _inputFile;
                set => _inputFile = Path.GetFullPath(value);
            }

            /// <summary>
            ///     Path of PDF file.
            /// </summary>
            [Description("Path of PDF file.")]
            [CommandOption("-o|--output")]
            public string OutputFile
            {
                get => _outputFile;
                set => _outputFile = Path.GetFullPath(value);
            }
        }
    }
}
