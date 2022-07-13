using System;
using System.IO;
using System.Linq;
using Utility.CommandLine;

namespace GitlabReports
{
    public static class Program
    {
        /// <summary>
        ///     Displays help text and exits.
        /// </summary>
        [Argument('h', "help", "Displays help text and exits.")]
        private static bool ShowHelpText { get; set; }

        /// <summary>
        ///     Path of configuration file.
        /// </summary>
        [Argument('i', "input", "Path of JSON file.")]
        private static string InputFile { get; set; }

        /// <summary>
        ///     Path of configuration file.
        /// </summary>
        [Argument('o', "output", "Path of PDF file.")]
        private static string OutputFile { get; set; }

        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            Arguments.Populate();

            if (ShowHelpText || args.Length == 0)
            {
                ShowHelp();
                return;
            }

            if (string.IsNullOrEmpty(InputFile))
            {
                throw new ArgumentException($"'{nameof(InputFile)}' cannot be null or empty.", nameof(InputFile));
            }

            if(string.IsNullOrEmpty(OutputFile))
            {
                OutputFile = Path.ChangeExtension(InputFile, ".pdf");
            }

            var report = ReportGenerator.Import(InputFile);
            ReportGenerator.Export(report, OutputFile);
        }

        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"ERROR:{((Exception)e.ExceptionObject).Message}");
            Environment.Exit(1);
        }

        /// <summary>
        ///     Show help for arguments.
        /// </summary>
        private static void ShowHelp()
        {
            var helpAttributes = Arguments.GetArgumentInfo(typeof(Program));

            Console.WriteLine("Short\t   Long   \tFunction");
            Console.WriteLine("-----\t----------\t--------");

            foreach (var item in helpAttributes)
            {
                Console.WriteLine($"-{item.ShortName}\t--{item.LongName,-10}\t{item.HelpText}");
            }
        }
    }
}
