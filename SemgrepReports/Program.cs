using SemgrepReports.Models;
using System;
using System.Linq;
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
            Arguments.Populate();

            if (ShowHelpText)
            {
                ShowHelp();
                return;
            }

            var report = ReportGenerator.Import(InputFile);
            ReportGenerator.Export(report, OutputFile);
        }

        /// <summary>
        ///     Show help for arguments.
        /// </summary>
        private static void ShowHelp()
        {
            var helpAttributes = Arguments.GetArgumentInfo(typeof(Program));

            var maxLen = helpAttributes.Select(a => a.Property.PropertyType.ToColloquialString()).OrderByDescending(s => s.Length).FirstOrDefault().Length;

            Console.WriteLine($"Short\tLong\t\t{"Type".PadRight(maxLen)}\tFunction");
            Console.WriteLine($"-----\t----\t\t{"----".PadRight(maxLen)}\t--------");

            foreach (var item in helpAttributes)
            {
                var result = item.ShortName + "\t" + item.LongName + "\t\t" + item.Property.PropertyType.ToColloquialString().PadRight(maxLen) + "\t" + item.HelpText;
                Console.WriteLine(result);
            }
        }

        /// <summary>
        ///     Returns a "pretty" string representation of the provided Type; specifically, corrects the naming of generic Types
        ///     and appends the type parameters for the type to the name as it appears in the code editor.
        /// </summary>
        /// <param name="type">The type for which the colloquial name should be created.</param>
        /// <returns>A "pretty" string representation of the provided Type.</returns>
        private static string ToColloquialString(this Type type) => (!type.IsGenericType ? type.Name : type.Name.Split('`')[0] + "<" + string.Join(", ", type.GetGenericArguments().Select(a => a.ToColloquialString())) + ">");

    }
}
