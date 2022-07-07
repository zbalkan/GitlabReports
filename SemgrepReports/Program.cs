using System;
using Utility.CommandLine;

namespace SemgrepReports
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
        [Argument('i', "input", "Path of json file.")]
        private static string InputFile { get; set; }

        /// <summary>
        ///     Path of configuration file.
        /// </summary>
        [Argument('o', "output", "Path of pdf file.")]
        private static string OutputFile { get; set; }

        public static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            Arguments.Populate();

            if (ShowHelpText)
            {
                ShowHelp();
                return;
            }

            var report = ReportGenerator.Import(InputFile);
            ReportGenerator.Export(report, OutputFile);
        }

        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
            Environment.Exit(0);
        }

        /// <summary>
        ///     Show help for arguments.
        /// </summary>
        private static void ShowHelp()
        {
            var helpAttributes = Arguments.GetArgumentInfo(typeof(Program));

            Console.WriteLine("Short\tLong\tFunction");
            Console.WriteLine("-----\t----\t--------");

            foreach (var item in helpAttributes)
            {
                var result = item.ShortName + "\t" + item.LongName + "\t" + item.HelpText;
                Console.WriteLine(result);
            }
        }
    }
}
